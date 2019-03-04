using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        LoopingAutomata l = new LoopingAutomata();
        /* 
        Console.WriteLine("Current State = " + l.CurrentState);
        Console.WriteLine("Transition.todX90: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX90));
        Console.WriteLine("Transition.todX180: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX180));
        Console.WriteLine("Transition.todX270: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX270));
        Console.WriteLine("Transition.todX360: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.todX360));
        Console.WriteLine("Transition.Reset: Current State = " +(LoopingState) l.MoveNext((int) LoopingTransition.Reset));
        */
        Coordinate simu = new Coordinate();
        List<int> trajectoryX = new List<int>();
        trajectoryX.Add(120);
        trajectoryX.Add(200);
        trajectoryX.Add(200);
        trajectoryX.Add(60);
        trajectoryX.Add(120);
        trajectoryX.Add(200);
        trajectoryX.Add(300);
        trajectoryX.Add(10);
        trajectoryX.Add(100);

        Console.WriteLine("LOOPING :");
        foreach(int newAngle in trajectoryX){
            simu.xangle = newAngle;
            l.calculateState(simu);
            Console.WriteLine("Current State Looping = " + l.CurrentState);
        }

        
        ARollAutomata a = new ARollAutomata();
        List<int> trajectoryZ = new List<int>();
        trajectoryZ.Add(120);
        trajectoryZ.Add(120);
        trajectoryZ.Add(200);
        trajectoryZ.Add(300);
        trajectoryZ.Add(1);
        trajectoryZ.Add(5);
        trajectoryZ.Add(100);

        Console.WriteLine("AILERON ROLL :");
        foreach(int newAngle in trajectoryZ){
            simu.zangle = newAngle;
            a.calculateState(simu);
            Console.WriteLine("Current State ARoll = " + a.CurrentState);
        }



    /* 
        ARollAutomata a = new ARollAutomata();
        Console.WriteLine("Current State = " + a.CurrentState);
        Console.WriteLine("Transition.todZ90: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ90));
        Console.WriteLine("Transition.todZ180: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ180));
        Console.WriteLine("Transition.todZ270: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ270));
        Console.WriteLine("Transition.todZ360: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.todZ360));
        Console.WriteLine("Transition.Reset: Current State = " +(ARollState) a.MoveNext((int) ARollTransition.Reset));
    */
    }
    
}