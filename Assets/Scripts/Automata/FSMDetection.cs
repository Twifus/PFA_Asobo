using System;
using System.Collections.Generic; 



    public class FSMDetection
    {
        public class StateTransition //Représente un état et une de ses transitions.
        {
            readonly int CurrentState;
            readonly int Transition;

        public StateTransition(int currentState, int transitionfsm)
            {
                CurrentState = currentState;
                Transition = transitionfsm;
            }


        public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Transition.GetHashCode();
            }
            
            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.CurrentState == other.CurrentState && this.Transition == other.Transition;
            }
        }

        public Dictionary<StateTransition, int> DicoTransitions; 
        public int CurrentState; //{ get; private set; }


        public int GetNext(int TargetTransition)
        {
            StateTransition Stransition = new StateTransition(CurrentState, TargetTransition);
            int nextState;
            if (!DicoTransitions.TryGetValue(Stransition, out nextState)){
                return CurrentState;
            }
            return nextState;
        }

        public int MoveNext(int transition)
        {
            CurrentState = GetNext(transition);
            return CurrentState;
        }
    }




