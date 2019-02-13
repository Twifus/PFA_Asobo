using System;
using System.Collections.Generic;


public class LoopingAutomata : IFigureAutomata {
    private FSMLooping _myAuto;
    public LoopingAutomata (){
        _myAuto = new FSMLooping();
    }
    

    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    public void resetStates() {
        _myAuto.CurrentState = (int) LoopingState.Start;
    }
    //renvoie l'id de la figure représentée par FigureId
    public figure_id getFigureId() {
        return figure_id.LOOP;
    }
    //affiche le nom de la figure que l'automate gère (debug)
    public string getName() {
        return "Looping";
    }
    //renvoie si l'automate est sur un état final ou pas
    public bool isValid() {
        return (_myAuto.CurrentState == (int) LoopingState.Looping);
    }
    //renvoie l'id de l'état actuel (debug)
    public int getCurrentState() {
        return _myAuto.CurrentState;
    }
    //renvoie le nombre d'états de l'automate (debug)
    public int getNumberOfState() {
        return ((int)LoopingState.Looping)+1;
    }
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1
    public int calculateState(Coordinate newPos) {
        if (isValid()) return 1;
        int idDegrees = (int) newPos.xangle / 90;
        int state = getCurrentState();
        if (state < (int) LoopingState.dX270) {
            if (idDegrees == state || idDegrees == state + 1) {
                _myAuto.MoveNext(state);
            }
            else {
                resetStates();
                return -1;
            }
        }
        else {
            if (idDegrees == 0 || idDegrees >= 4) {
                _myAuto.MoveNext(4);
            }
            else {
                resetStates();
                return -1;
            }
        }
        if (isValid()) return 1;
        return 0;
    }
}