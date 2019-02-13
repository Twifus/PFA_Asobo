using System.Collections;
using System.Collections.Generic;

//positions en transform, ensemble des points sur lesquels tournent les automates et $1
public struct Coordinate {int x;}
//ensemble des figures : Looping, Barel Roll, Cuban 8, double looping etc...
enum FigureId {A,B,C}
//représente une stat sur une figure : son pourcentage de reconnaissance
public struct FigureDetection {
    FigureId id; 
    int p; 
    FigureDetection(FigureId id, int p) {
        this.id = id;
        this.p = p;
    }
}

/*
    Interface des detecteurs de figures, une classe par algo :
    $1 / automate
 */
public interface IFigureDetection {}

/*
    Interface d'automate par figure :
    chaque figure possède (ou non) un automate
    la classe s'occupe de convertir les positions en transitions
 */
public interface IFigureAutomata {
    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    void resetStates();
    //renvoie l'id de la figure représentée par FigureId
    FigureId getFigureId();
    //affiche le nom de la figure que l'automate gère (debug)
    string getName();
    //renvoie si l'automate est sur un état final ou pas
    bool isValid();
    //renvoie l'id de l'état actuel (debug)
    int getCurrentState();
    //renvoie le nombre d'états de l'automate (debug)
    int getNumberOfState();
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1
    int calculateState(Coordinate newPos);
}

/*
    Représente une figure composée :
    -une figure principale qui doit représenter le début et la fin de l'automate
    -une liste de figures secondaires à effectuer dans n'importe quel ordre
 */
public abstract class AbstractComposedFigureAutomata : IFigureAutomata {
    protected IFigureAutomata _mainFigure;
    protected List<IFigureAutomata> _subFigures;
    protected List<bool> _subFiguresRealised;
    //la fonction ajoute une figure à la sous-liste des figures (appelé dans un constructeur enfant)
    protected void addFigure(IFigureAutomata auto) {
        _subFigures.Add(auto);
        _subFiguresRealised.Add(false);
    }
    void resetStates() {
        _mainFigure.resetStates();
        foreach(IFigureAutomata auto in _subFigures)
            auto.resetStates();
        foreach(bool b in _subFiguresRealised)
            b = false; //à refaire
    }
    bool isValid() {
        if(_mainFigure.isValid()) {
            foreach(bool b in _subFiguresRealised) {
                if(!b)
                    return false;
            }
            return true;
        }
        else
            return false;
    }
    int getCurrentState() {
        if (isValid())
            return 1;
        return 0;
    }
    int getNumberOfState() {return 1;}
    int calculateState(Coordinate newPos) {
        int mainState = _mainFigure.calculateState(newPos);
        if (mainState == -1)
            resetStates(); //on relance tout
        else { //cas normal
            int i = 0;
            foreach(IFigureAutomata auto in _subFigures) {
                if(_subFigures.calculateState(newPos) == 1)
                    _subFiguresRealised[i] = true;
                i++;
            }
        }
        if(mainState == 1 && !isValid()) { //cas où l'automate principal a terminé mais pas les autres
            resetStates();
            return -1;
        }
        return mainState; //renvoie
    }
}

/*
    Représente une série de figure à effectuer dans l'ordre
 */
public abstract class AbstractSequentialFigureAutomata : IFigureAutomata {
    protected List<IFigureAutomata> _listFigures;
    protected int _currentFigureIndex;
    //reinitialise l'automate de la figure
    IFigureAutomata getCurrentAuto() {
        return _listFigures[_currentFigureIndex];
    }
    void resetStates() {
        foreach(IFigureAutomata auto in _listFigures)
            auto.resetStates();
        _currentFigureIndex = 0;
    }
    bool isValid() {
        return (_currentFigureIndex == _listFigures.Count); 
                //&& getCurrentAuto().isValid());;
    }
    //renvoie l'id de l'état actuel
    int getCurrentState() {
        if (isValid())
            return _listFigures.Count;
        return _currentFigureIndex;
    }
    //renvoie le nombre d'états de l'automate
    int getNumberOfState() {
        return _listFigures.Count;
    }
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    int calculateState(Coordinate newPos) {
        if (isValid()) 
            return 1;
        int intState = getCurrentAuto().calculateState(newPos);
        if (intState == -1) {
            resetStates();
            return -1;
        }
        else if (intState == 1) {
            _currentFigureIndex++;
            if(isValid())
                return 1;
        }
        return 0;
    }
}

public class DummyAutomata : IFigureAutomata {
    void resetStates() {}
    FigureId getFigureId() {return FigureId.A;}
    string getName() {return "";}
    bool isValid() {return true;}
    int getCurrentState() {return 0;}
    int getNumberOfState() {return 3;}
    bool calculateState(Coordinate newPos) {return 0;}
}

/*
    Représente le gestionaire des automates
    à utiliser avec FigureManager
 */
public class AutomataDetector : IFigureDetection {
    private List<IFigureAutomata> _myAutomatas;

    public AutomataDetector(int n) {
        for(int i = 0; i < n; i++)
            _myAutomatas.Add(new DummyAutomata());
    }

    public void setPoint(Coordinate point) {
        foreach (IFigureAutomata auto in _myAutomatas)
            auto.calculateState(point);
    }

    public List<FigureDetection> detection() {
        List<FigureDetection> list = new List<FigureDetection>();
        foreach (IFigureAutomata auto in _myAutomatas) {
            if(auto.isValid()) {
                list.Add(new FigureDetection(auto.getFigureId(), 100));
                auto.resetStates();
            }
            else
                list.Add(Figure(auto.getFigureId(), 0));
        }
    }
}