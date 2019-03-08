using System;
using System.Collections.Generic;

public class LoopingAutomata : FSMDetection, IFigureAutomata {
    //private FSMLooping _myAuto;
    private int _finalState;

    //renvoie une valeur convertie du pitch entre 0 et 360 
    static private double getTrueAngle(double pitch, double yaw) {
        if (yaw > 0)
            return (pitch + 360)%360;
        return 180 - pitch;
    }

    public LoopingAutomata (int n = 4){
        //_myAuto = new FSMLooping();
        CurrentState = 0;
        _finalState = n;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>();
        for (int i = 0; i < n; i++) {
            StateTransition t = new StateTransition(i, i+1); //transition vers l'etat suivant
            DicoTransitions.Add(t, i+1);
            t = new StateTransition(i, 0); //transition vers l'etat de depart
            DicoTransitions.Add(t, 0);
            t = new StateTransition(i, i); //transition vers l'etat courant (boucle)
            DicoTransitions.Add(t, i);
        }
    }
    

    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    public void resetStates() {
        CurrentState = 0;
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
        return (CurrentState == _finalState);
    }
    //renvoie l'id de l'état actuel (debug)
    public int getCurrentState() {
        return CurrentState;
    }
    //renvoie le nombre d'états de l'automate (debug)
    public int getNumberOfState() {
        return _finalState + 1;
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
                MoveNext(idDegrees); 
            }
            else {
                resetStates();
                return -1;
            }
        }
        else {
            if (idDegrees == 0 || idDegrees >= 4) {
                MoveNext(4);
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