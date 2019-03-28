using System;
using System.Collections.Generic;
using System.Numerics;
using unity = UnityEngine;


public class ARollAutomata : FSMDetection, IFigureAutomata {

    private int _finalState;
    private bool _yawState = true;
    private float _upScalar = 0;
    private float _forwardScalar = 0;
    float _rightScalarStart = 0;
    private float _rightScalar = 0;
    private float altitude = 0;
    int window = 3;
    int state;
    bool[] figure = new bool[20];

    public ARollAutomata(int n = 4){
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
    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    public void resetStates() {
        CurrentState = (int) ARollState.Start;
    }
    //renvoie l'id de la figure représentée par FigureId
    public figure_id getFigureId() {
        return figure_id.BARREL;
    }
    //affiche le nom de la figure que l'automate gère (debug)
    public string getName() {
        return "Aileron Roll";
    }
    //renvoie si l'automate est sur un état final ou pas
    public bool isValid() {
        return (CurrentState == _finalState);
    }
    //renvoie l'id de l'état actuel (debug)
    public int getCurrentState() {
        return CurrentState;
    }
    //renvoie le nombre d'états de l'automate (debug)
    public int getNumberOfState() {
        return _finalState+1;
    }
    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1
    public int calculateState(IFlyingObject plane) {
        init(plane);
        if (isValid()) return 1;

        figure[0] = Q1ARoll();
        figure[1] = Q2ARoll();
        figure[2] = Q3ARoll();
        figure[3] = Q4ARoll();

        /* CUBAN EIGHT
        figure[0] = Q1Loop();
        figure[1] = Q2Loop();
        figure[2] = Q3ARoll();
        figure[3] = Q4ARoll();
        figure[4] = Q1Loop();
        figure[5] = Q2Loop();
        figure[6] = Q3Loop();
        figure[7] = Q4Loop();
            */

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

    public void process()
    {
        if (figure[0])
        {
            MoveNext(1);
        }
        if (state > 0)
        {
            if (!figure[state - 1])
            {
                if (figure[state])
                {
                    MoveNext(state + 1);
                }
                else
                {
                    resetStates();
                }
            }
        }
    }

    public void init(IFlyingObject plane)
    {
        _forwardScalar = Vector3.Dot(plane.forward, System.Numerics.Vector3.UnitY);
        _upScalar = Vector3.Dot(plane.up, System.Numerics.Vector3.UnitY);
        _rightScalar = Vector3.Dot(plane.right, System.Numerics.Vector3.UnitY);
        state = getCurrentState();
        if (state == 0)
        {
            _rightScalarStart = Vector3.Dot(plane.right, System.Numerics.Vector3.UnitY);
        }
    }

    public bool Q1ARoll()
    {
        return (_upScalar <= 0 && _rightScalar <= 0 );
    }

    public bool Q2ARoll()
    {
        return (_upScalar <= 0 && _rightScalar >= 0);
    }

    public bool Q3ARoll()
    {
        return (_upScalar >= 0 && _rightScalar >= 0);
    }

    public bool Q4ARoll()
    {
        return (_upScalar >= 0 && _rightScalar <= 0);
    }


    public bool Q1Loop()
    {
        return (_upScalar <= 0 && _forwardScalar >= 0 && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    public bool Q2Loop()
    {
        return ((_upScalar <= 0 && _forwardScalar < 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    public bool Q3Loop()
    {
        return ((_upScalar > 0 && _forwardScalar < 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    public bool Q4Loop()
    {
        return ((_upScalar >= 0 && _forwardScalar >= 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

}