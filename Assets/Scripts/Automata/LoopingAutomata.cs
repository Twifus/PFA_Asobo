using System;
using System.Collections.Generic;

public class LoopingAutomata : FSMDetection, IFigureAutomata {
    //private FSMLooping _myAuto;
    private int _finalState;
    private bool _yawState = true;

    //renvoie une valeur convertie du pitch entre 0 et 360 
    private double getTrueAngle(double pitch, double yaw) {
        if(_yawState) {//yaw defaut > 0
            if (yaw > 0)
                return (pitch + 360)%360;
            return 180 + pitch;
            }
        else {
            if (yaw < 0)
                return (pitch + 360)%360;
            return 180 + pitch;
        }
    }

    public LoopingAutomata (int n = 4) {
        CurrentState = 0;
        _finalState = n;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>();
        for (int i = 0; i < n; i++) {
            StateTransition t = new StateTransition(i, i+1); //transition vers l'etat suivant
            DicoTransitions.Add(t, i+1);
            //t = new StateTransition(i, 0); //transition vers l'etat de depart
            //DicoTransitions.Add(t, 0);
            t = new StateTransition(i, i); //transition vers l'etat courant (boucle)
            DicoTransitions.Add(t, i);
        }
    }
/* =======
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
>>>>>>> bb571059a86d52aad4390efd9416d1613e200bf7*/
    

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
        double yaw = newPos.yangle;
        double pitch = newPos.xangle;
        pitch = getTrueAngle(pitch, yaw);
        //Console.WriteLine("C'est bon les pitchs : " +pitch + "  yawState : " + _yawState);
        int idDegrees = (int) pitch / (360/_finalState);
        int state = getCurrentState();
        //avant dernier etat, penser à verifier si l'angle reboucle
        if(state == _finalState-1 && idDegrees == 0) { 
            MoveNext(_finalState);
            return 1; 
        }
        if (idDegrees == state || idDegrees == state + 1) {
            MoveNext(idDegrees); 
        }
        else {
            resetStates();
            return -1;
        }
        //if (isValid()) return 1;
        if (CurrentState == 0) _yawState = (yaw >= 0);
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