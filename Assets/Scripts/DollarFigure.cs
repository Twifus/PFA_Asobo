using System.Collections;
using System.Collections.Generic;

public static class DollarFigure
{
    public static readonly int curvesPerFigure = 4;

    // Files loading variables
    public static readonly string filePath = "./Assets/Figures/";
    public static readonly int[] columnToPick = { 2, 11, 14, 17 }; // the size must be equals to curvesPerFigure

    // Curves names for each figure  /!\ size must be equals to curvesPerFigure
    public static readonly string[] loop         = { "BosseHaut", "LigneDroite", "BosseBas", "ZigZag" };
    public static readonly string[] barrelL      = { "LigneDroite", "ZigZag", "BosseBas", "LigneDroite" };
    public static readonly string[] barrelR      = { "LigneDroite", "ZagZig", "BosseBas", "LigneDroite" };
    public static readonly string[] cubanEight   = { "DoubleBosse", "DoubleDemieLigneMontante", "DoubleZigZag", "LigneCoupee" }; // Need to be changed
    public static readonly string[] straightLine = { "LigneDroite", "LigneDroite", "LigneDroite", "LigneDroite" };
}
