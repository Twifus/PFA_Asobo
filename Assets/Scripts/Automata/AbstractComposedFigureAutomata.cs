using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Définit un automate pour figure composée
/// </summary>
/// <remarks> 
/// Une figure est dite "composée" si elle est l'assemblage de :
///    -une figure principale qui doit représenter le début et la fin de l'automate
///    -une liste de figures secondaires à effectuer dans n'importe quel ordre
///
/// La figure totale est considérée valide si :
/// La figure principale est validée ET toutes les figures secondaires ont été validées pendant l'éxécution de la figure principale
/// Nous n'utilisons pas ce fichier dans notre code, mais c'est une piste pour pouvoir créer des figures composées d'autres figures
/// Les fonctions ont le principe que pour les autres figures, mais prend en compte le fait qu'il y a plusieurs figures
/// </remarks>
public abstract class AbstractComposedFigureAutomata : IFigureAutomata {
    protected IFigureAutomata _mainFigure;
    protected List<IFigureAutomata> _subFigures;
    protected List<bool> _subFiguresRealised;

    //la fonction ajoute une figure à la sous-liste des figures (appelé dans un constructeur enfant)
    protected void addFigure(IFigureAutomata auto) {
        _subFigures.Add(auto);
        _subFiguresRealised.Add(false);
    }

    public abstract figure_id getFigureId();
    public abstract string getName();

    public void resetStates() {
        _mainFigure.resetStates();
        int i = 0;
        foreach(IFigureAutomata auto in _subFigures) {
            auto.resetStates();
            _subFiguresRealised[i++] = false;
        }
    }
    public bool isValid() {
        if(_mainFigure.isValid()) {
            foreach(var b in _subFiguresRealised) {
                if(!b)
                    return false;
            }
            return true;
        }
        else
            return false;
    }
    public int getCurrentState() {
        if (isValid())
            return 1;
        return 0;
    }
    public int getNumberOfState() {return 1;}

    public int calculateState(IFlyingObject plane) {
        int mainState = _mainFigure.calculateState(plane);
        if (mainState == -1)
            resetStates(); //on relance tout
        else { //cas normal
            int i = 0;
            foreach(IFigureAutomata auto in _subFigures) {
                if(auto.calculateState(plane) == 1)
                    _subFiguresRealised[i] = true;
                i++;
            }
        }
        //cas où l'automate principal a terminé mais pas les autres : 
        //on recommence car toutes les conditions n'ont pas été respectées
        if (mainState == 1 && !isValid()) { 
            resetStates();
            return -1;
        }
        return mainState;
    }
}