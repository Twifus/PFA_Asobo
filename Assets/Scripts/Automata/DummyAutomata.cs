using System.Collections;
using System.Collections.Generic;

/*
 * Classe de test de AutomataDetector
 */

public class DummyAutomata : IFigureAutomata {
    public void resetStates() {}
    public figure_id getFigureId() {return figure_id.LOOP;}
    public string getName() {return "";}
    public bool isValid() {return true;}
    public int getCurrentState() {return 0;}
    public int getNumberOfState() {return 3;}
    public int calculateState(IFlyingObject plane) {return 0;}
}