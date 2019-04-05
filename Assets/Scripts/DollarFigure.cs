using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Conteneur décrivant les courbes composant les figures et utilisées par DollarDetector
/// </summary>
public static class DollarFigure
{
    /// <summary>
    /// Nombre de courbes de référence par figure
    /// </summary>
    public static readonly int curvesPerFigure = 4;

    /// <summary>
    /// Emplacement des fichiers de référence
    /// </summary>
    public static readonly string filePath = "./Assets/Figures/";

    /// <summary>
    /// Colones à extraire des fichiers de référence
    /// </summary>
    /// <remarks>
    /// Le nombre de courbes à extraire doit être égal à curvesPerFigure
    /// </remarks>
    public static readonly int[] columnToPick = { 2, 11, 14, 17 };

    // Curves names for each figure  /!\ size must be equals to curvesPerFigure
    public static readonly string[] loop         = { "BosseHaut", "LigneDroite", "BosseBas", "ZigZag" };
    public static readonly string[] barrelL      = { "LigneDroite", "ZigZag", "BosseBas", "LigneDroite" };
    public static readonly string[] barrelR      = { "LigneDroite", "ZagZig", "BosseBas", "LigneDroite" };
    public static readonly string[] cubanEight   = { "DoubleBosse", "DoubleDemieLigneMontante", "DoubleZigZag", "LigneCoupee" }; // Need to be changed
    public static readonly string[] straightLine = { "LigneDroite", "LigneDroite", "LigneDroite", "LigneDroite" };
}
