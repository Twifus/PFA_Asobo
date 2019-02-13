using System;
using System.Collections.Generic;


public class LoopingAutomata : IFigureAutomata {
    private FSMLooping myAuto;
    public LoopingAutomata (){
        myAuto = new FSMLooping();
    }
    

    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    void resetStates();
    //renvoie l'id de la figure représentée par FigureId
    figure_id getFigureId();
    //affiche le nom de la figure que l'automate gère (debug)
    string getName();
    //renvoie si l'automate est sur un état final ou pas
    bool isValid();
    //renvoie l'id de l'état actuel (debug)
    int getCurrentState();
    //renvoie le nombre d'états de l'automate (debug)
    int getNumberOfState();
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1
    int calculateState(Coordinate newPos);
}