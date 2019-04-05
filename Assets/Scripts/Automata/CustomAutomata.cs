using System.Collections;
using System.Collections.Generic;
//using unity = UnityEngine;


/// <summary>
/// Fichier de reconaissance d'une figure personalisable
/// </summary>
public class CustomAutomata : SimpleAutomata
{
    /// <summary>
    /// Constructeur qui initialise un automate à n états
    /// </summary>
    public CustomAutomata()
    {
        int n = 10;//NOMBRES D'ETATS
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
        return figure_id.CUSTOMFIGURE;
    }

    public override string getName()
    {
        return "Custom Figure";
    }

    public override int calculateState(IFlyingObject plane)
    {
        init(plane);
        if (isValid()) return 1;
        /*
         * METTRE ICI LES CHECKS
         */

        /*
        * REMPLIR IC LE TABLEAU FIGURE AVEC LA SEQUENCE D'ETATS 
        * NE PAS OUBLIER DE CHANGER LE NOMBRE D ETATS DANS LE CONSTRUCTEUR
        */

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
