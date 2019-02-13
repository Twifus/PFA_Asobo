﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * Utilisation de Time.frameCount permettra de savoir le nombre de frame passées 
 * afin d'enregistrer les données suivant la volonté de l'utilisateur 
 */
public class FigureManager : MonoBehaviour{
    private IFigureDetection _figureDetection;
    //public Settings settings;

    public GameObject plane;
    public Text textScore;
    public Text textFigure;

    private Plane _plane;
    private int _score;
    private float _timeToDisplay;
    private string[] _figureName;
    private int[] _figurePoint;

    /*private List<List<float>> _coordinates = new List<List<float>>();

    private List<float> _coordinateX = new List<float>();
    private List<float> _coordinateY = new List<float>();
    private List<float> _coordinateZ = new List<float>();

    private List<List<float>> _rotates = new List<List<float>>();
    private List<float> _rotateX = new List<float>();
    private List<float> _rotateY = new List<float>();
    private List<float> _rotateZ = new List<float>();
    private List<float> _rotateW = new List<float>();

    private List<float> _time = new List<float>();*/

    
    /** Met à jour le score du joueur */
    public void UpdateScore(int points)
    {
        _score += points;
        Invoke("DisplayScore",0f);
    }

    #region Private Methods

    private void Start()
    {
        _figureDetection = new FigureFaussaire();
        _score = 0;
        _plane = Plane.NewPlane(plane);
        _timeToDisplay = Time.time;

        _figureName = new string[] { "LOOP", "BARREL", "CUBANEIGHT" };
        _figurePoint = new int[] { 20, 10, 50 };

        /*_coordinates.Add(_coordinateX);
        _coordinates.Add(_coordinateY);
        _coordinates.Add(_coordinateZ);

        _rotates.Add(_rotateX);
        _rotates.Add(_rotateY);
        _rotates.Add(_rotateZ);
        _rotates.Add(_rotateW);*/
    }

    private void Update()
    {
        //Condition sur les frames pour enregistrement des coordonnées
        GetCoordinates(_plane);
        AnalyzeTrajectory();
        if(Time.time > _timeToDisplay + 1.5f)
        {
            DisableText();
        }
    }


    /** Récupère les coordonnées et les rotations de l'avion dans un tableau (List<float> à voir) */
    private void GetCoordinates(Plane _plane)
    {
        /*        _coordinateX.Add(_plane.Position.x);
                _coordinateY.Add(_plane.Position.y);
                _coordinateZ.Add(_plane.Position.z);

                _rotateX.Add(_plane.Rotation.x);
                _rotateY.Add(_plane.Rotation.y);
                _rotateZ.Add(_plane.Rotation.z);
                _rotateW.Add(_plane.Rotation.w);
                _time.Add(Time.time);*/

        Coordinate point = new Coordinate();
        point.xpos = _plane.Position.x;
        point.ypos = _plane.Position.y;
        point.zpos = _plane.Position.z;

        point.xangle = _plane.Rotation.x;
        point.yangle = _plane.Rotation.y;
        point.zangle = _plane.Rotation.z;
        point.wangle = _plane.Rotation.w;

        point.time = Time.time;

        _figureDetection.setPoint(point);

    }

    /** Affiche le score du joueur */
    private void DisplayScore()
    {
        textScore.text = "Score : " + _score;
    }

    /** Appelle la fonction qui analyse la trajectoire */
    private void AnalyzeTrajectory()
    {
        List<Figure> result = _figureDetection.detection();
        for(int i = 0; i < result.Count; i++)
        {
            if(result[i].quality == 1f)
            {
                Display(i);
            }
        }
    }

    private void DisableText()
    {
        textFigure.text = "";
    }

    private void Display(int id)
    {
        textFigure.text = _figureName[id];
        UpdateScore(_figurePoint[id]);
        textScore.text = "Score : " + _score;
        _timeToDisplay = Time.time;
    }
}

#endregion