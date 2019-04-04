using System.Collections;
using System.Collections.Generic;

/*
    Représente le gestionaire des automates
    à utiliser avec FigureManager
 */
public class AutomataDetector : IFigureDetection {
    private List<IFigureAutomata> _myAutomatas;

    public AutomataDetector() {
        //creation des auomates à reconnaitre
        _myAutomatas = new List<IFigureAutomata>();
        _myAutomatas.Add(new LoopingAutomata());
        _myAutomatas.Add(new ARollAutomata());
        _myAutomatas.Add(new CEAutomata());
        _myAutomatas.Add(new ARollLeftAutomata());
        _myAutomatas.Add(new CERAutomata());
    }

    /*
     *  Ancienne version, inutile
     */
    public void setPoint(Coordinate coord) {
        ;
    }
    
    public void setPoint(IFlyingObject plane) {
        foreach (IFigureAutomata auto in _myAutomatas)
            auto.calculateState(plane);
    }

    public List<Figure> detection() {
        List<Figure> list = new List<Figure>();
        foreach (IFigureAutomata auto in _myAutomatas) {
            if(auto.isValid()) {
                list.Add(new Figure(auto.getFigureId(), 1f));
                auto.resetStates();
            }
            //else
            //    list.Add(new Figure(auto.getFigureId(), 0));
        }
        return list;
    }
}