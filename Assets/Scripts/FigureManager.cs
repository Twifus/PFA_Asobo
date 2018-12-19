using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour{
    private Plane _plane;
    //public FigureDetection IfigureDetection;
    //public Settings settings;

    public GameObject plane;
    private int _score;
    private ArrayList[][] _position = new ArrayList[2][];

    private ArrayList[] _coordinates = new ArrayList[3];

    private ArrayList _coordinateX = new ArrayList();
    private ArrayList _coordinateY = new ArrayList();
    private ArrayList _coordinateZ = new ArrayList();

    private ArrayList[] _rotates = new ArrayList[4];
    private ArrayList _rotateX = new ArrayList();
    private ArrayList _rotateY = new ArrayList();
    private ArrayList _rotateZ = new ArrayList();
    private ArrayList _rotateW = new ArrayList();


    private void Start()
    {
        _plane = new Plane(plane);

        _coordinates[0] = _coordinateX;
        _coordinates[1] = _coordinateY;
        _coordinates[2] = _coordinateZ;

        _rotates[0] = _rotateX;
        _rotates[1] = _rotateY;
        _rotates[2] = _rotateZ;
        _rotates[3] = _rotateW;

        _position[0] = _coordinates;
        _position[1] = _rotates;
    }

    private void Update()
    {
        GetCoordinates(_plane);
        Debug.Log(_coordinates[0][0]);
        print(_coordinates[0][0]);

    }

    #region Private Methods

    /** Récupère les coordonnées et les rotations de l'avion dans un tableau (ArrayList à voir) */
    private void GetCoordinates(Plane _plane)
    {
        _coordinates[0].Add(_plane.Position.x);
        _coordinates[1].Add(_plane.Position.y);
        _coordinates[2].Add(_plane.Position.z);

        _rotates[0].Add(_plane.Rotation.x);
        _rotates[1].Add(_plane.Rotation.y);
        _rotates[2].Add(_plane.Rotation.z);
        _rotates[3].Add(_plane.Rotation.w);
    }

    /** Affiche le score du joueur */
    private void DisplayScore(int points)
    {
        //TODO
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