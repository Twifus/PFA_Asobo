using System;
using System.Collections.Generic;
using System.Numerics;
//using unity = UnityEngine;

/// <summary>
/// Fichier de reconaissance d'un Aileron Roll dans le sens horaire
/// </summary>
public class ARollAutomata : SimpleAutomata {

    /// <summary>
    /// Constructeur qui initialise un automate à n états
    /// </summary>
    public ARollAutomata(){
        int n = 5;//NOMBRES D'ETATS
        CurrentState = 0;
        _finalState = n;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>();
        for (int i = 0; i < n; i++)
        {
            StateTransition t = new StateTransition(i, i + 1); //transition vers l'etat suivant
            DicoTransitions.Add(t, i + 1);
            //t = new StateTransition(i, 0); //transition vers l'etat de depart
            //DicoTransitions.Add(t, 0);
            t = new StateTransition(i, i); //transition vers l'etat courant (boucle)
            DicoTransitions.Add(t, i);
        }
    }

    public override figure_id getFigureId()
    {
        return figure_id.BARREL;
    }

    public override string getName() {
        return "Aileron Roll";
    }

    public override int calculateState(IFlyingObject plane) {
        init(plane);
        if (isValid()) return 1;
        checkTime(3);
        checkForward();
        figure[0] = Q1ARoll();
        figure[1] = Q2ARoll();
        figure[2] = Q3ARoll();
        figure[3] = Q4ARoll();
        figure[4] = Q1ARoll();

        process();

        //unity.Debug.Log("0 : " + Q1Loop() + ", 1 : " + Q2Loop() + ", 2 : " + Q3Loop() + ", 3 : " + Q4Loop());
        //unity.Debug.Log(_finalState);
        //unity.Debug.Log("State : " + state);
        //unity.Debug.Log("_upScalar :" + _upScalar);
        //unity.Debug.Log("_forwardScalar :" + _forwardScalar);
        //unity.Debug.Log(plane.pos.Y);
        //unity.Debug.Log(altitude);
        if (isValid()) return 1;
        return 0;
    }



}