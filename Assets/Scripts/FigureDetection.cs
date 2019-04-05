using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface définissant un détecteur de figures
/// </summary>
public interface IFigureDetection {
    /// <summary>
    /// Ajoute les coordonnées courantes d'un IFlyingObject à la trajectoire à traiter
    /// </summary>
    void setPoint(IFlyingObject flyingObject);

    /// <summary>
    /// Lance l'algorithme de détection sur la trajectoire actuelle
    /// </summary>
    /// <returns>
    /// Retourne une liste des figures détectées par l'algorithme.
    /// </returns>
    List<Figure> detection();
}
