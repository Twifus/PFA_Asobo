using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Représente le gestionaire des automates
/// à utiliser avec FigureManager
 /// </summary>
 /// <remarks> C'est ici qu'il faut ajouter le fichier de la figure correspondate si on veut en ajouter une nouvelle</remarks>
public class AutomataDetector : IFigureDetection {
    private List<IFigureAutomata> _myAutomatas;

    /// <summary>
    /// Constructeur où on ajoute la liste des figures que l'on veut reconnaitre
    /// </summary>
    public AutomataDetector() {
        //creation des auomates à reconnaitre
        _myAutomatas = new List<IFigureAutomata>();
        _myAutomatas.Add(new LoopingAutomata());
        _myAutomatas.Add(new ARollAutomata());
        _myAutomatas.Add(new CEAutomata());
        _myAutomatas.Add(new ARollLeftAutomata());
        _myAutomatas.Add(new CERAutomata());
        _myAutomatas.Add(new CustomAutomata());
    }
    
    /// <summary>
    /// Fonction qui fait appel à calculateState sur tous les automates que l'on a choisi, déclenchant ainsi tourner la reconaissance
    /// </summary>
    public void setPoint(IFlyingObject plane) {
        foreach (IFigureAutomata auto in _myAutomatas)
            auto.calculateState(plane);
    }

    /// <summary>
    /// Fonction qui renvoie un tableau constitué de toutes les figures qui ont été reconnues
    /// </summary>
    public List<Figure> detection() {
        List<Figure> list = new List<Figure>();
        foreach (IFigureAutomata auto in _myAutomatas) {
            if(auto.isValid()) {
                list.Add(new Figure(auto.getFigureId(), 1f));
                auto.resetStates();
            }
        }
        return list;
    }
}