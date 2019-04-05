using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Permet d'implémenter une série de figures à effectuer dans l'ordre
/// </summary>
/// ///<remarks> Nous n'utilisons pas ce fichier dans notre code, mais c'est une piste pour pouvoir créer des figures à la suite d'autres
///Les fonctions ont le principe que pour les autres figures, mais prend en compte le fait qu'il y a plusieurs figures
/// </remarks>
public abstract class AbstractSequentialFigureAutomata : IFigureAutomata {
    //liste des figures à réaliser
    protected List<IFigureAutomata> _listFigures;
    //représente l'id de la figure étudiée actuellement
    protected int _currentFigureIndex;

    //renvoie la figure actuellement calculée
    public IFigureAutomata getCurrentAuto() {
        return _listFigures[_currentFigureIndex];
    }

    public abstract figure_id getFigureId();

    public abstract string getName();

    public void resetStates() {
        foreach(IFigureAutomata auto in _listFigures)
            auto.resetStates();
        _currentFigureIndex = 0;
    }
    public bool isValid() {
        return (_currentFigureIndex == _listFigures.Count); 
    }
    //renvoie l'id de l'état actuel
    public int getCurrentState() {
        if (isValid())
            return _listFigures.Count;
        return _currentFigureIndex;
    }
    //renvoie le nombre d'états de l'automate
    public int getNumberOfState() {
        return _listFigures.Count;
    }
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    public int calculateState(IFlyingObject plane) {
        if (isValid()) 
            return 1;
        int intState = getCurrentAuto().calculateState(plane);
        if (intState == -1) {
            resetStates();
            return -1;
        }
        else if (intState == 1) {
            _currentFigureIndex++;
            if(isValid())
                return 1;
        }
        return 0;
    }
}