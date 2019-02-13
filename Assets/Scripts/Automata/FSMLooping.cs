using System;
using System.Collections.Generic;

    public enum LoopingState
    {
        Start,
        dX90,
        dX180,
        dX270,
        Looping
    }
    public enum LoopingTransition
    {
        Reset,
        todX90,
        todX180,
        todX270,
        todX360
    }

public class FSMLooping : FSMDetection {

    public FSMLooping(){
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
}
/* 
public class LoopAutomata : IFigureAutomata {
    private FSMLooping _myautomata;
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
    int calculateState(Coordinate newPos) {

    }
}*/