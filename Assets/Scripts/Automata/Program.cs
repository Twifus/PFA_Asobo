using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        LoopingAutomata l = new LoopingAutomata();
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
    }
    
}