using System;
using System.Collections.Generic;

public enum LoopingState
    {
        Start = 0,
        dX90,
        dX180,
        dX270,
        Looping = 4
    }
public enum LoopingTransition
    {
        Reset = 0,
        todX90,
        todX180,
        todX270,
        todX360 = 4
    }

public class LoopingAutomata : FSMDetection, IFigureAutomata {
    //private FSMLooping _myAuto;
    public LoopingAutomata (){
        //_myAuto = new FSMLooping();
        bool yawState = true;
        int 
        CurrentState = (int) LoopingState.Start;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>{
            {new StateTransition((int) LoopingState.Start,(int) LoopingTransition.todX90), (int) LoopingState.dX90},
            {new StateTransition((int) LoopingState.dX90, (int) LoopingTransition.todX180), (int) LoopingState.dX180},
            {new StateTransition((int) LoopingState.dX180, (int) LoopingTransition.todX270), (int) LoopingState.dX270},
            {new StateTransition((int) LoopingState.dX270, (int) LoopingTransition.todX360), (int) LoopingState.Looping},
            {new StateTransition((int) LoopingState.Looping, (int) LoopingTransition.Reset), (int) LoopingState.Start},
            {new StateTransition((int) LoopingState.dX90, (int) LoopingTransition.Reset), (int) LoopingState.Start},
            {new StateTransition((int) LoopingState.dX180, (int) LoopingTransition.Reset), (int) LoopingState.Start},
            {new StateTransition((int) LoopingState.dX270, (int) LoopingTransition.Reset), (int) LoopingState.Start},
        };
    }
    

    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    public void resetStates() {
        CurrentState = (int) LoopingState.Start;
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
        return (CurrentState == (int) LoopingState.Looping);
    }
    //renvoie l'id de l'état actuel (debug)
    public int getCurrentState() {
        return CurrentState;
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
        int idDegrees = (int) newPos.xangle / (360/_finalState);
        int state = getCurrentState();
        if (state < _finalState-1) {
            if (idDegrees == state || idDegrees == state + 1) {
                MoveNext(idDegrees); 
            }
            else {
                resetStates();
                return -1;
            }
        }
        else {
            if (idDegrees == 0 || idDegrees >= _finalState) {
                MoveNext(_finalState);
            }
            else {
                resetStates();
                return -1;
            }
        }
        if (isValid()) return 1;
        return 0;
    }
    
/* 
    public int calculateState(Coordinate newPos) {
        if (isValid()) return 1;
        int window = 5;
        int state = getCurrentState();
        if (state == 0){
            yawState = true;
        }

        

        if (newPos.xangle > (90-window) || newPos.xangle < 90){
            if (yaw && (state == 0 || state == 1)){
                MoveNext(1);
            } else resetStates();
        } else if (newPos.xangle > (0-window) || newPos.xangle < (0 + window)){
            if (!yaw && (state == 1 || state == 2)){
                MoveNext(2);
            } else resetStates();
        } else if (newPos.xangle > -90 || newPos.xangle < (-90 + window)){
            if (!yaw && (state == 2 || state == 3)){
                MoveNext(3);
            } else resetStates();
        } else if (newPos.xangle > (0-window) || newPos.xangle < (0 + window)){
            if (!yaw && (state == 3 || state == 4)){
                MoveNext(4);
            } else resetStates();
        } 
        
    }

    */
}