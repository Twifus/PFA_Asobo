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