using System.Collections;
using System.Collections.Generic;

/*
    Représente le gestionaire des automates
    à utiliser avec FigureManager
 */
public class AutomataDetector : IFigureDetection {
    private List<IFigureAutomata> _myAutomatas;

    public AutomataDetector() {
        _myAutomatas = new List<IFigureAutomata>();
        _myAutomatas.Add(new LoopingAutomata());
        _myAutomatas.Add(new ARollAutomata());
        //for (int i = 0; i < n; i++)
        //    _myAutomatas.Add(new DummyAutomata());
    }

    public void setPoint(Coordinate point) {
        foreach (IFigureAutomata auto in _myAutomatas)
            auto.calculateState(point);
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