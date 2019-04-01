using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using System.Globalization;
using unity = UnityEngine;

public abstract class SimpleAutomata : FSMDetection, IFigureAutomata
{

    public int _finalState;
    public bool _yawState = true;
    public float _upScalar = 0;
    public float _forwardScalar = 0;
    public float _rightScalarStart = 0;
    public float _rightScalar = 0;
    public float altitude = 0;
    public float window = 0.2f;
    public int state;
    public int tmpState = 0;
    public int time;
    public bool[] figure = new bool[20];

    //reinitialise l'automate de la figure 
    //necessaire pour reset les automates terminés
    //appelé par l'interface et/ou les automates parents
    public void resetStates()
    {
        CurrentState = (int)ARollState.Start;
    }
    //renvoie l'id de la figure représentée par FigureId
    public abstract figure_id getFigureId();
    //affiche le nom de la figure que l'automate gère (debug)
    public abstract string getName();
    //renvoie si l'automate est sur un état final ou pas
    public bool isValid()
    {
        return (CurrentState == _finalState);
    }
    //renvoie l'id de l'état actuel (debug)
    public int getCurrentState()
    {
        return CurrentState;
    }
    //renvoie le nombre d'états de l'automate (debug)
    public int getNumberOfState()
    {
        return _finalState + 1;
    }

    public abstract int calculateState(IFlyingObject plane);

    public void process()
    {
        if (figure[0])
        {
            MoveNext(1);
        }
        if (state > 0)
        {
            if (figure[state - 1] && figure[state])
                MoveNext(state + 1);

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


    public bool Q2ARoll()
    {
        return (_upScalar <= 0 && _rightScalar <= 0);
    }

    public bool Q3ARoll()
    {
        return (_upScalar <= 0 && _rightScalar >= 0);
    }

    public bool Q4ARoll()
    {
        return (_upScalar >= 0 && _rightScalar >= 0);
    }

    public bool Q1ARoll()
    {
        return (_upScalar >= 0 && _rightScalar <= 0);
    }


    public bool Q2Loop()
    {
        return (_upScalar <= 0 && _forwardScalar >= 0 && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    public bool Q3Loop()
    {
        return ((_upScalar <= 0 && _forwardScalar < 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    public bool Q4Loop()
    {
        return ((_upScalar > 0 && _forwardScalar < 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    public bool Q1Loop()
    {
        return ((_upScalar >= 0 && _forwardScalar >= 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
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

    //Vérifie si l'écart d'altitude entre l'état 0 et l'état controlSttae est bien supérieur à minAltitude
    public void checkAltitude(IFlyingObject plane, int minAltitude, int controlState)
    {
        if (_forwardScalar <= 0.3 && state == 0)
        {
            altitude = plane.pos.Y;
        }
        if (_forwardScalar <= 0.2 && state == controlState)
        {
            if (plane.pos.Y < altitude + minAltitude)
                resetStates();
        }
    }

    public void checkTime(int maxTime)
    {
        
        int tmpTime = 0;
        
        if (tmpState != getCurrentState())
        {
            tmpState = getCurrentState();
            time = DateTime.Now.Second;
        } else
        {
            if (time + maxTime > 60)
            {
                tmpTime = DateTime.Now.Second - 60;
            }
            else tmpTime = DateTime.Now.Second;

            if ((time + maxTime) % 60 <= tmpTime)
            {
                resetStates();
            }
        }
    }

}
