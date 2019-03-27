using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        /* 
        LoopingAutomata l = new LoopingAutomata();
        Coordinate simu = new Coordinate();

        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);

        simu.xangle = 60;
        simu.yangle = -30;
        l.calculateState(simu);
        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);

        simu.xangle = 70;
        simu.yangle = 30;
        l.calculateState(simu);
        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);

        simu.xangle = -30;
        l.calculateState(simu);
        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);

        simu.xangle = -30;
        simu.yangle = -30;
        l.calculateState(simu);
        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);

        simu.xangle = 30;
        simu.yangle = -30;
        l.calculateState(simu);
        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);
        if (l.isValid()) {
            l.resetStates();
        }

        simu.xangle = 70;
        simu.yangle = 30;
        l.calculateState(simu);
        Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + simu.xangle);
        if (l.isValid()) {
            l.resetStates();
        }
        
        /* 
        Console.WriteLine("LOOPING :");
        foreach(int newAngle in trajectoryX){
            simu.xangle = newAngle;
            l.calculateState(simu);
            Console.WriteLine("Current State Looping = " + l.CurrentState + " with angle " + newAngle);
            if (l.isValid()) {
                l.resetStates();
            }
        }

        
        ARollAutomata a = new ARollAutomata();
        List<int> trajectoryZ = new List<int>();
        trajectoryZ.Add(60);
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
            Console.WriteLine("Current State ARoll = " + a.CurrentState +  " with angle " + newAngle);
            if (a.isValid()) {
                a.resetStates();
            }
        }
        */
    }
    
    
}