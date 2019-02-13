using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        FSMLooping l = new FSMLooping();
        Console.WriteLine("Current State = " + l.CurrentState);
        Console.WriteLine("Transition.todX90: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX90));
        Console.WriteLine("Transition.todX180: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX180));
        Console.WriteLine("Transition.todX270: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX270));
        Console.WriteLine("Transition.todX360: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX360));
        Console.WriteLine("Transition.Reset: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.Reset));

        FSMARoll a = new FSMARoll();
        Console.WriteLine("Current State = " + a.CurrentState);
        Console.WriteLine("Transition.todZ90: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ90));
        Console.WriteLine("Transition.todZ180: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ180));
        Console.WriteLine("Transition.todZ270: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ270));
        Console.WriteLine("Transition.todZ360: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ360));
        Console.WriteLine("Transition.Reset: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.Reset));
    }
}