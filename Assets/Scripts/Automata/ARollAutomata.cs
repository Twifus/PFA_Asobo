using System;
using System.Collections.Generic;
using System.Numerics;

    public enum ARollState{
        Start = 0,
        dZ90,
        dZ180,
        dZ270,
        ARoll = 4

    }

    public enum ARollTransition{

        Reset = 0,
        todZ90,
        todZ180,
        todZ270,
        todZ360 = 4
    }
    
    

public class ARollAutomata : FSMDetection, IFigureAutomata {

    public ARollAutomata(){
        CurrentState = (int) ARollState.Start;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>{
            {new StateTransition((int) ARollState.Start, (int) ARollTransition.todZ90), (int) ARollState.dZ90},
                {new StateTransition((int) ARollState.dZ90, (int) ARollTransition.todZ180), (int) ARollState.dZ180},
                {new StateTransition((int) ARollState.dZ180, (int) ARollTransition.todZ270), (int) ARollState.dZ270},
                {new StateTransition((int) ARollState.dZ270, (int) ARollTransition.todZ360), (int) ARollState.ARoll},
                {new StateTransition((int) ARollState.ARoll, (int) ARollTransition.Reset), (int) ARollState.Start},
                {new StateTransition((int) ARollState.dZ90, (int) ARollTransition.Reset), (int) ARollState.Start},
                {new StateTransition((int) ARollState.dZ180, (int) ARollTransition.Reset), (int) ARollState.Start},
                {new StateTransition((int) ARollState.dZ270, (int) ARollTransition.Reset), (int) ARollState.Start},
        };
    }
    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    public void resetStates() {
        CurrentState = (int) ARollState.Start;
    }
    //renvoie l'id de la figure représentée par FigureId
    public figure_id getFigureId() {
        return figure_id.BARREL;
    }
    //affiche le nom de la figure que l'automate gère (debug)
    public string getName() {
        return "Aileron Roll";
    }
    //renvoie si l'automate est sur un état final ou pas
    public bool isValid() {
        return (CurrentState == (int) ARollState.ARoll);
    }
    //renvoie l'id de l'état actuel (debug)
    public int getCurrentState() {
        return CurrentState;
    }
    //renvoie le nombre d'états de l'automate (debug)
    public int getNumberOfState() {
        return ((int)ARollState.ARoll)+1;
    }
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1
    public int calculateState(IFlyingObject plane) {
        /* 
        if (isValid()) {
            resetStates();
            return 1;
        }
        int idDegrees = (int) newPos.zangle / 90;
        int state = getCurrentState();
        if (state < (int) ARollState.dZ270) {
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
        */
        return 0;
    }
}