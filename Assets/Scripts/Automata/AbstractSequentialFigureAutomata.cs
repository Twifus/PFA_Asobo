using System.Collections;
using System.Collections.Generic;

/*
    Représente une série de figures à effectuer dans l'ordre
 */
public abstract class AbstractSequentialFigureAutomata : IFigureAutomata {
    protected List<IFigureAutomata> _listFigures;
    protected int _currentFigureIndex;
    //reinitialise l'automate de la figure
    public IFigureAutomata getCurrentAuto() {
        return _listFigures[_currentFigureIndex];
    }
    public abstract FigureId getFigureId();
    public abstract string getName();
    public void resetStates() {
        foreach(IFigureAutomata auto in _listFigures)
            auto.resetStates();
        _currentFigureIndex = 0;
    }
    public bool isValid() {
        return (_currentFigureIndex == _listFigures.Count); 
                //&& getCurrentAuto().isValid());;
    }
    //renvoie l'id de l'état actuel
    public int getCurrentState() {
        if (isValid())
            return _listFigures.Count;
        return _currentFigureIndex;
    }
    //renvoie le nombre d'états de l'automate
    public int getNumberOfState() {
        return _listFigures.Count;
    }
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    public int calculateState(Coordinate newPos) {
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