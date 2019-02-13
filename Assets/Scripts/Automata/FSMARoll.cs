using System;
using System.Collections.Generic;

    public enum ARollState{
        Start,
        dZ90,
        dZ180,
        dZ270,
        ARoll

    }

    public enum ARollTransition{

        Reset,
        todZ90,
        todZ180,
        todZ270,
        todZ360
    }
    
    

public class FSMARoll : FSMDetection {

    public FSMARoll(){
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
}