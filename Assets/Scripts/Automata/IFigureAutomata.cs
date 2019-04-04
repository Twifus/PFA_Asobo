//using System.Collections;
//using System.Collections.Generic;

/// <summary>
/// Interface d'automate :
/// chaque figure possède(ou non) un automate
/// la classe s'occupe de convertir les positions en transitions
/// </summary>

public interface IFigureAutomata {
    /// <summary>
    /// Réinitialise l'automate de la figure
    /// </summary>
    /// <remarks>
    /// necessaire pour reset les automates terminés ou qui ont échoués
    /// </remarks>
    void resetStates();

    /// <summary>
    /// Renvoie l'id de la figure représentée par FigureId
    /// </summary>
    figure_id getFigureId();

    /// <summary>
    /// Affiche le nom de la figure que l'automate gère (debug)
    /// </summary>
    string getName();

    /// <summary>
    /// Renvoie si l'automate est sur un état final ou non
    /// </summary>
    bool isValid();

    /// <summary>
    /// Renvoie l'id de l'état actuel (debug)
    /// </summary>
    int getCurrentState();

    /// <summary>
    /// Renvoie le nombre d'états de l'automate 
    /// </summary>
    int getNumberOfState();

    /// <summary>
    /// Coeur de l'algorithme : un appel calcule et effectue le changement d'état de l'automate.
    /// </summary>
    /// <returns>
    /// 0 si le nouvel état est intermédiaire
    /// -1 si l'automate recommence à l'état initial
    /// 1 si l'automate est dans un état final 
    /// </returns>
    /// <remarks>
    /// Il faut faire appel à init pour tout initialise, puis faire un appel à isValid pour vérifier que nosu ne sommes pas dans un état final
    /// Ensuite, il faut ajouter les différentes fonctions de vérifications si voulu.
    /// C'est maintenant que l'on crée l'automate : on ajoute dans la tableau figure une séquence de quart de figures (Il faut modifier le nombre d'éats dans le constructeur, paramètre n)
    /// L'appel à process permet de faire els changements eventuels d'états, et enfin il faut revérifier si l'on est dans un état final
    /// </remarks>
    int calculateState(IFlyingObject plane);
}