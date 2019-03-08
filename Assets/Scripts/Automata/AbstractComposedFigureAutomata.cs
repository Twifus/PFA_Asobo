using System.Collections;
using System.Collections.Generic;

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
    public abstract figure_id getFigureId();
    public abstract string getName();
    public void resetStates() {
        _mainFigure.resetStates();
        int i = 0;
        foreach(IFigureAutomata auto in _subFigures) {
            auto.resetStates();
            _subFiguresRealised[i++] = false;
        }
    }
    public bool isValid() {
        if(_mainFigure.isValid()) {
            foreach(var b in _subFiguresRealised) {
                if(!b)
                    return false;
            }
            return true;
        }
        else
            return false;
    }
    public int getCurrentState() {
        if (isValid())
            return 1;
        return 0;
    }
    public int getNumberOfState() {return 1;}
    public int calculateState(Coordinate newPos) {
        int mainState = _mainFigure.calculateState(newPos);
        if (mainState == -1)
            resetStates(); //on relance tout
        else { //cas normal
            int i = 0;
            foreach(IFigureAutomata auto in _subFigures) {
                if(auto.calculateState(newPos) == 1)
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