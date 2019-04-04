using System;
using System.Collections.Generic;
using System.Numerics;
using unity = UnityEngine;


/// <summary>
/// Fichier de reconaissance d'un cuban eight avec l'avion incliné vers la droite au début de la séquence
/// </summary>
public class CERAutomata : SimpleAutomata
{
    /// <summary>
    /// Constructeur qui initialise un automate à n états
    /// </summary>
    public CERAutomata()
    {
        int n = 9; //NOMBRE D'ETATS
        CurrentState = 0;
        _finalState = n;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>();
        for (int i = 0; i < n; i++)
        {
            StateTransition t = new StateTransition(i, i + 1); //transition vers l'etat suivant
            DicoTransitions.Add(t, i + 1);
            t = new StateTransition(i, i); //transition vers l'etat courant (boucle)
            DicoTransitions.Add(t, i);
        }
    }

    public override figure_id getFigureId()
    {
        return figure_id.CUBANEIGHT;
    }

    public override string getName()
    {
        return "Cuban eight";
    }


    public override int calculateState(IFlyingObject plane)
    {
        init(plane);
        window = 1;
        if (isValid()) return 1;
        checkAltitude(plane, 50, 2);
        checkTime(5);

        figure[0] = Q1Loop();
        figure[1] = Q2Loop();
        figure[2] = Q3Loop();
        figure[3] = Q3ARoll();
        figure[4] = Q4ARoll();
        figure[5] = Q2Loop();
        figure[6] = Q3Loop();
        figure[7] = Q3ARoll();
        figure[8] = Q4ARoll();

        process();

        //unity.Debug.Log("0 : " + Q1Loop() + ", 1 : " + Q2Loop() + ", 2 : " + Q3Loop() + ", 3 : " + Q4Loop());
        //unity.Debug.Log(_finalState);
        //unity.Debug.Log("State : " + state);
        //unity.Debug.Log("_upScalar :" + _upScalar);
        //unity.Debug.Log("_rightScalar :" + _rightScalar);
        //unity.Debug.Log("_forwardScalar :" + _forwardScalar);
        //unity.Debug.Log(plane.pos.Y);
        //unity.Debug.Log(altitude);
        if (isValid()) return 1;
        return 0;
    }
}
