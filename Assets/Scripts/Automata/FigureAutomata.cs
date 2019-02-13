using System.Collections;
using System.Collections.Generic;

//positions en transform, ensemble des points sur lesquels tournent les automates et $1
public struct Coordinate {
    //public int x;
}
//ensemble des figures : Looping, Barel Roll, Cuban 8, double looping etc...
public enum FigureId {A,B,C}
//représente une stat sur une figure : son pourcentage de reconnaissance
public struct FigureDetection {
    FigureId id; 
    int p; 
    public FigureDetection(FigureId id, int p) {
        this.id = id;
        this.p = p;
    }
}

/*
    Interface des detecteurs de figures, une classe par algo :
    $1 / automate
 */
public interface IFigureDetection {}

public class DummyAutomata : IFigureAutomata {
    public void resetStates() {}
    public FigureId getFigureId() {return FigureId.A;}
    public string getName() {return "";}
    public bool isValid() {return true;}
    public int getCurrentState() {return 0;}
    public int getNumberOfState() {return 3;}
    public int calculateState(Coordinate newPos) {return 0;}
}