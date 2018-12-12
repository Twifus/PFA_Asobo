using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour{
    public GameObject plane;
    public FigureDetection figureDetection;
    public Settings settings;

    private int _score;

    /** Récupère les coordonnées et les rotations de l'avion dans un tableau (ArrayList à voir) */
    private ArrayList GetCoordinates(GameObject plane)
    {
        ArrayList array = new ArrayList();
        return array;
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
