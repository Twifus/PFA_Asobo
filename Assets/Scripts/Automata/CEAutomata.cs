using System;
using System.Collections.Generic;
using System.Numerics;
using unity = UnityEngine;

public class CEAutomata : SimpleAutomata
{

    public CEAutomata()
    {
        int n = 9; //NOMBRE D'ETATS
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

    //renvoie l'id de la figure représentée par FigureId
    public override figure_id getFigureId()
    {
        return figure_id.CUBANEIGHT;
    }
    //affiche le nom de la figure que l'automate gère (debug)
    public override string getName()
    {
        return "Cuban eight";
    }

    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1

    public override int calculateState(IFlyingObject plane)
    {
        init(plane);
        window = 1;
        if (isValid()) return 1;
        checkAltitude(plane, 50, 2);
        //checkAltitude(plane, 50, 7);
        checkTime(8);

        figure[0] = Q2Loop();
        figure[1] = Q3Loop();
        figure[2] = Q3ARoll();
        figure[3] = Q4ARoll();
        figure[4] = Q1ARoll();
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
