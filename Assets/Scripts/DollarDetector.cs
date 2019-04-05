using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using PDollarGestureRecognizer;

/// <summary>
/// Detecteur de figures se reposant sur les algorithmes "Dollar"
/// </summary>
/// <remarks>
/// Ce detecteur se base sur des buffers de coordonnées qui sont soumis aux algorithmes "Dollar".
/// Ceux-ci attribuent un nom et un score à chaque courbe, puis le detecteur vérifie si l'ensemble de résultats corresponds à une figure de référence.
/// </remarks>
public class DollarDetector : IFigureDetection {

    #region Members

    /// <summary>
    /// Taille maximale d'un buffer de coordonnées
    /// </summary>
    private readonly int MAX_SIZE = 280;

    /// <summary>
    /// Minimum de points nécessaires pour lancer une détection
    /// </summary>
    private readonly int MINPOINTSCOUNT = 30;

    /// <summary>
    /// Indice courant dans les buffers de coordonnées
    /// </summary>
    private int index = 0;

    /// <summary>
    /// Buffers de coordonnées
    /// </summary>
    protected List<Point>[] _timePoints;

    /// <summary>
    /// Courbes de référence pour chaque buffer
    /// </summary>
    protected List<Gesture>[] gestures;

    #endregion

    #region Constructor
    
    public DollarDetector()
    {
        gestures = new List<Gesture>[DollarFigure.curvesPerFigure];
        for (int i = 0; i < gestures.Length; i++) {
            gestures[i] = new List<Gesture>();
        }
        
        FigureLoaderP loader = new FigureLoaderP(gestures);
        loader.LoadFigures();
        
        _timePoints = new List<Point>[DollarFigure.curvesPerFigure];
        for (int i = 0; i < _timePoints.Length; i++) {
            _timePoints[i] = new List<Point>();
        }
    }

    #endregion

    #region Set Points

    /// <summary>
    /// Ajoute une nouvelle coordonnée à la trajectoire à traiter
    /// </summary>
    public void setPoint(Coordinate point) {
        if (_timePoints[0].Count == MAX_SIZE)
        {
            // Remove first element
            for (int j = 0; j < _timePoints.Length; j++) {
                _timePoints[j].RemoveAt(0);
            }

            // Update X coordinates
            for (int i = 0; i < MAX_SIZE - 1; i++)
            {
                for (int j = 0; j < _timePoints.Length; j++) {
                    _timePoints[j][i].X--;
                }
            }
        }
        else
        {
            index++;
        }

        // Add new points
        _timePoints[0].Add(new Point(index, point.ypos, 0));
        _timePoints[1].Add(new Point(index, point.roll, 0));
        _timePoints[2].Add(new Point(index, point.pitch, 0));
        _timePoints[3].Add(new Point(index, point.yaw, 0));
    }

    /// <summary>
    /// Ajoute les coordonnées courantes d'un IFlyingObject à la trajectoire à traiter
    /// </summary>
    public void setPoint(IFlyingObject flyingObject) {
        if (_timePoints[0].Count == MAX_SIZE)
        {
            // Remove first element
            for (int j = 0; j < _timePoints.Length; j++) {
                _timePoints[j].RemoveAt(0);
            }

            // Update X coordinates
            for (int i = 0; i < MAX_SIZE - 1; i++) {
                for (int j = 0; j < _timePoints.Length; j++) {
                    _timePoints[j][i].X--;
                }
            }
        }
        else
        {
            index++;
        }

        float rightScalar = Vector3.Dot(flyingObject.right, Vector3.UnitY);
        float upScalar = Vector3.Dot(flyingObject.up, Vector3.UnitY);
        float forwardScalar = Vector3.Dot(flyingObject.forward, Vector3.UnitY);

        // Add new points
        _timePoints[0].Add(new Point(index, flyingObject.pos.Y, 0));
        _timePoints[1].Add(new Point(index, rightScalar, 0));
        _timePoints[2].Add(new Point(index, upScalar, 0));
        _timePoints[3].Add(new Point(index, forwardScalar, 0));
    }

    #endregion

    #region Detection

    /// <summary>
    /// Vérifie la correspondance entre les courbes détectées et des courbes de référence.
    /// </summary>
    /// <remarks>
    /// Cette fonction permet de vérifier si l'ensemble des courbes reconnues par l'algorithmes correspondent aux courbes de référence d'une figure donnée.
    /// </remarks>
    /// <returns>
    /// Retourne true si les courbes de <paramref name="results"/> sont celles indiquées dans <paramref name="curvesNames"/>
    /// </returns>
    protected bool AnalyseResults(BestGesture[] results, string[] curvesNames) {
        bool ret = true;
        for (int i = 0; i < results.Length; i++) {
            ret = ret && results[i].Name.Equals(curvesNames[i]) /*&& results[i].Score > 0.8f*/;
        }
        return ret;
    }

    /// <summary>
    /// Lance l'algorithme de détection sur la trajectoire actuelle
    /// </summary>
    /// <returns>
    /// Retourne une liste des figures détectées par l'algorithme.
    /// </returns>
    public List<Figure> detection() {

        List<Figure> figures = new List<Figure>();

        //U.Debug.Log(_timePoints[0].Count);

        if (_timePoints[0].Count > MINPOINTSCOUNT)
        {
            // Get best gestures for each curve
            BestGesture[] results = new BestGesture[DollarFigure.curvesPerFigure];
            for (int i = 0; i < results.Length; i++) {
                results[i] = PointCloudRecognizer.Classify(new Gesture(_timePoints[i].ToArray()), gestures[i].ToArray());
            }

            // Analyse results

            // Straight Line
            if (AnalyseResults(results, DollarFigure.straightLine)) { // buffer cleaner
                //UnityEngine.Debug.Log("StraightLine");
                //UnityEngine.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                ClearLists();
            }

            // Loop
            if (AnalyseResults(results, DollarFigure.loop)) {
                //UnityEngine.Debug.Log("Loop");
                //UnityEngine.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                figures.Add(new Figure(figure_id.LOOP, 1f));
                ClearLists();
            }

            // Roll
            if (AnalyseResults(results, DollarFigure.barrelL) || AnalyseResults(results, DollarFigure.barrelR)) {
                //UnityEngine.Debug.Log("Roll");
                //UnityEngine.Debug.Log(results[0].Score + ", " + results[1].Score + ", " + results[2].Score + ", " + results[3].Score);
                figures.Add(new Figure(figure_id.BARREL, 1f));
                ClearLists();
            }

            // Cuban Eight
            if (AnalyseResults(results, DollarFigure.cubanEight)) {
                //UnityEngine.Debug.Log("CubanEight");
                figures.Add(new Figure(figure_id.CUBANEIGHT, 1f));
                ClearLists();
            }
        }
        return figures;
	}

    #endregion

    #region Utility Methods

    /// <summary>
    /// Vide les buffers contenant les trajectoires
    /// </summary>
    protected void ClearLists() {
        //U.Debug.Log("Clear");
        for (int i = 0; i < _timePoints.Length; i++) {
            _timePoints[i].Clear();
        }
        index = 0;
    }

    /// <summary>
    /// (DEBUG) Ecrit le contenu actuel des buffers des trajectoires dans un fichier
    /// </summary>
    public void WriteLists()
    {
        string path = string.Format("../DollarLog-{0}", System.DateTime.Now.ToFileTime());
        StreamWriter Writer = new StreamWriter(path + ".csv", true);
        for (int i = 0; i < _timePoints[0].Count; i++) {
            Writer.WriteLine(string.Format("{0};{1};{2};{3};{4}",
            _timePoints[0][i].X, _timePoints[0][i].Y, _timePoints[1][i].Y, _timePoints[2][i].Y, _timePoints[3][i].Y));
        }
        Writer.Close();
    }

    #endregion
}
