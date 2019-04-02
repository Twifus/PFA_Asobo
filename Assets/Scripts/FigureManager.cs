using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * Utilisation de Time.frameCount permettra de savoir le nombre de frame passées 
 * afin d'enregistrer les données suivant la volonté de l'utilisateur 
 */
public class FigureManager : MonoBehaviour{
    public enum Detector { Faussaire, Automata, Dollar };
    public static string[] DetectorName = { "Faussaire", "Automata", "$P"};

    public static Detector detector = Detector.Automata;
    private IFigureDetection _figureDetection;
    //public Settings settings;

    public GameObject plane;
    public Text textScore;
    public Text textFigure;
    public Text textAlgo;

    private IFlyingObject _plane;
    private int _score;
    private float _timeToDisplay;
    private string[] _figureName;

    private int[] _figurePoint;
    
    /** Met à jour le score du joueur */
    public void UpdateScore(int points)
    {
        _score += points;
        Invoke("DisplayScore",0f);
    }

    #region Private Methods

    private void Start()
    {
        // Init detector
        if (detector == Detector.Faussaire)
            _figureDetection = new FigureFaussaire();
        else if (detector == Detector.Automata)
            _figureDetection = new AutomataDetector();
        else if (detector == Detector.Dollar)
            _figureDetection = new DollarDetector();
        
        _score = 0;
        _plane = Plane.NewPlane(plane);
        _timeToDisplay = Time.time;

        _figureName = new string[] { "LOOP", "BARREL", "CUBANEIGHT" };
        _figurePoint = new int[] { 20, 10, 50 };

        DisplayScore();
        DisableText();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("SwitchAlgorithm"))
            SwitchAlgorithm();
        DisplayAlgorithme();

        //Condition sur les frames pour enregistrement des coordonnées
        //GetCoordinates(_plane);
        _figureDetection.setPoint(_plane);
        AnalyzeTrajectory();
        if(Time.time > _timeToDisplay + 1.5f)
        {
            DisableText();
        }
    }


    /** Récupère les coordonnées et les rotations de l'avion dans un tableau (List<float> à voir) */
    private void GetCoordinates(IFlyingObject _plane)
    {
        /*Coordinate point = new Coordinate();
        point.xpos = _plane.Position.x;
        point.ypos = _plane.Position.y;
        point.zpos = _plane.Position.z;

        point.xangle = _plane.pitch;
        point.yangle = _plane.yaw;
        point.zangle = _plane.roll;

        point.roll = _plane.roll;
        point.pitch = _plane.pitch;
        point.yaw = _plane.yaw;

        point.time = Time.time;*/

        //_figureDetection.setPoint(point);

    }

    private void SwitchAlgorithm()
    {
        if (detector == Detector.Automata)
        {
            detector = Detector.Dollar;
            _figureDetection = new DollarDetector();
        }
        else
        {
            detector = Detector.Automata;
            _figureDetection = new AutomataDetector();
        }
    }

    private void DisplayAlgorithme()
    {
        textAlgo.text = DetectorName[(int)detector];
    }

    /** Affiche le score du joueur */
    private void DisplayScore()
    {
        textScore.text = "Score : " + _score;
    }

    /** Appelle la fonction qui analyse la trajectoire */
    private void AnalyzeTrajectory()
    { 
        //Debug.Log(
        List<Figure> result = _figureDetection.detection();
        for(int i = 0; i < result.Count; i++)
        {
            if(result[i].quality == 1f)
            {
                Display(result[i].id);
            }
        }
    }

    private void DisableText()
    {
        textFigure.text = "";
    }

    private void Display(figure_id id)
    {
        textFigure.text = _figureName[(int)id];
        UpdateScore(_figurePoint[(int)id]);
        textScore.text = "Score : " + _score;
        _timeToDisplay = Time.time;
    }

    private string GetNameFigure(figure_id id)
    {
        return _figureName[(int)id];
    }
}

#endregion