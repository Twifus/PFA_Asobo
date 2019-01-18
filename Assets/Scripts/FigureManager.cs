using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigureManager : MonoBehaviour{

    //public FigureDetection IfigureDetection;
    //public Settings settings;

    public GameObject plane;
    public Text textScore;

    private Plane _plane;
    private int _score;

    private List<List<float>> _coordinates = new List<List<float>>();

    private List<float> _coordinateX = new List<float>();
    private List<float> _coordinateY = new List<float>();
    private List<float> _coordinateZ = new List<float>();

    private List<List<float>> _rotates = new List<List<float>>();
    private List<float> _rotateX = new List<float>();
    private List<float> _rotateY = new List<float>();
    private List<float> _rotateZ = new List<float>();
    private List<float> _rotateW = new List<float>();

    private List<float> _time = new List<float>();

    private void Start()
    {
        _score = 0;
        _plane = Plane.NewPlane(plane);

        _coordinates.Add(_coordinateX);
        _coordinates.Add(_coordinateY);
        _coordinates.Add(_coordinateZ);

        _rotates.Add(_rotateX);
        _rotates.Add(_rotateY);
        _rotates.Add(_rotateZ);
        _rotates.Add(_rotateW);
    }

    private void Update()
    {
        GetCoordinates(_plane);
        DisplayScore();
    }

    #region Private Methods

    /** Récupère les coordonnées et les rotations de l'avion dans un tableau (List<float> à voir) */
    private void GetCoordinates(Plane _plane)
    {
        _coordinateX.Add(_plane.Position.x);
        _coordinateY.Add(_plane.Position.y);
        _coordinateZ.Add(_plane.Position.z);

        _rotateX.Add(_plane.Rotation.x);
        _rotateY.Add(_plane.Rotation.y);
        _rotateZ.Add(_plane.Rotation.z);
        _rotateW.Add(_plane.Rotation.w);
        _time.Add(Time.time);
    }

    /** Affiche le score du joueur */
    private void DisplayScore()
    {
        textScore.text = "Score : " + _score;
    }

    /** Met à jour le score du joueur */
    private void UpdateScore(int points)
    {
        _score += points;
    }

    /** Appelle la fonction qui analyse la trajectoire */
    private void AnalyzeTrajectory()
    {
        //TODO
    }

    /** Détermine quelle figure a été réalisée */
    private void WhichFigure()
    {
        //TODO
    }
}

#endregion