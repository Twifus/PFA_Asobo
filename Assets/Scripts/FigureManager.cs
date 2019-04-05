using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestionnaire des détecteurs de figures et de score
/// </summary>
/// <remarks>
/// Le FigureManager s'assure de l'éxecution d'un détecteur de figures à chaque frame.
/// C'est également lui qui est responsable du calcul du score, et des divers affichages liés à celui-ci.
/// </remarks>
public class FigureManager : MonoBehaviour{

    /// <summary>
    /// Enumération des détecteurs disponibles
    /// </summary>
    public enum Detector { Faussaire, Automata, Dollar };

    /// <summary>
    /// Nom des détectuers disponibles
    /// </summary>
    public static string[] DetectorName = { "Faussaire", "Automata", "$P"};

    /// <summary>
    /// Nom des figures réalisables
    /// </summary>
    private string[] _figureName = { "LOOP", "BARREL", "CUBANEIGHT" };

    /// <summary>
    /// Points associés à chaque figure
    /// </summary>
    private int[] _figurePoint = { 20, 10, 50 };

    /// <summary>
    /// Détecteur à utiliser
    /// </summary>
    public static Detector detector = Detector.Automata;

    /// <summary>
    /// Instance du détecteur utilisé
    /// </summary>
    private IFigureDetection _figureDetection;

    /// <summary>
    /// GameObject du joueur
    /// </summary>
    public GameObject plane;

    /// <summary>
    /// Component Text affichant le score
    /// </summary>
    public Text textScore;

    /// <summary>
    /// Component Text affichant le nom de la figure réalisée
    /// </summary>
    public Text textFigure;

    /// <summary>
    /// Component Text affichant le détecteur utilisé
    /// </summary>
    public Text textAlgo;

    /// <summary>
    /// Instance de Plane associée au joueur
    /// </summary>
    private IFlyingObject _plane;

    /// <summary>
    /// Score actuel du joueur
    /// </summary>
    private int _score;

    /// <summary>
    /// Temps du début d'affichage de la figure réalisée
    /// </summary>
    private float _timeToDisplay;

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

        _figureName = new string[] { "LOOP", "BARREL", "CUBANEIGHT", "CUSTOM FIGURE" };
        _figurePoint = new int[] { 20, 10, 50, 5};

        DisplayScore();
        DisplayAlgorithm();
        DisableText();
    }

    /// <remarks>
    /// En premier lieu, change de détecteur si l'utilisateur a pressé le bouton correspondant.
    /// Puis, transmet la coordonnée actuelle du joueur au détecteur.
    /// Ensuite, lance la détection de trajectoire et traite le résultat.
    /// Enfin, si un nom de figure a été affiché suffisament longtemps, le masque.
    /// </remarks>
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("SwitchAlgorithm"))
            SwitchAlgorithm();
        
        _figureDetection.setPoint(_plane);
        AnalyzeTrajectory();
        if(Time.time > _timeToDisplay + 1.5f)
        {
            DisableText();
        }
    }

    /// <summary>
    /// Incrémente le score et met à jour l'affichage
    /// </summary>
    /// <param name="points">Nombre de points à ajouter au score</param>
    public void UpdateScore(int points)
    {
        _score += points;
        Invoke("DisplayScore", 0f);
    }

    /// <summary>
    /// Passe au detecteur suivant dans la liste des détecteurs disponibles
    /// </summary>
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
        DisplayAlgorithm();
    }
    
    /// <summary>
    /// Met à jour l'affichage du nom de l'algorithme
    /// </summary>
    private void DisplayAlgorithm()
    {
        textAlgo.text = DetectorName[(int)detector];
    }

    /// <summary>
    /// Met à jour l'affichage du score
    /// </summary>
    private void DisplayScore()
    {
        textScore.text = "Score : " + _score;
    }

    /// <summary>
    /// Lance la détection de la trajectoire et affiche la figure réalisé si nécessaire
    /// </summary>
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


    /// <summary>
    /// Masque l'indicateur de figure réalisé
    /// </summary>
    private void DisableText()
    {
        textFigure.text = "";
    }

    /// <summary>
    /// Affiche le nom d'une figure
    /// </summary>
    /// <param name="id">Indice de la figure à afficher</param>
    private void Display(figure_id id)
    {
        textFigure.text = _figureName[(int)id];
        UpdateScore(_figurePoint[(int)id]);
        textScore.text = "Score : " + _score;
        _timeToDisplay = Time.time;
    }
}