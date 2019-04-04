using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using System.Globalization;
using unity = UnityEngine;


/*
 * Classe des automates basiques :
 * contient les méthodes principales de calcul de l'état de l'avion (vertical, horizontal etc)
 */
public abstract class SimpleAutomata : FSMDetection, IFigureAutomata
{

    public int _finalState;
    public bool _yawState = true;
    public float _upScalar = 0;
    public float _forwardScalar = 0;
    public float _rightScalarStart = 0;
    public float _forwardScalarStart = 0;
    public float _rightScalar = 0;
    public float altitude = 0;
    public float window = 0.2f;
    public int state;
    public int tmpState = 0;
    public int time;
    public bool[] figure = new bool[20];


    /// <summary>
    /// Réinitialise l'automate de la figure
    /// </summary>
    /// <remarks>
    /// necessaire pour reset les automates terminés ou qui ont échoués
    /// </remarks>

    public void resetStates()
    {
        CurrentState = 0;
    }
    /// <summary>
    /// Renvoie l'id de la figure représentée par FigureId
    /// </summary>
    public abstract figure_id getFigureId();

    /// <summary>
    /// affiche le nom de la figure que l'automate gère (debug)
    /// </summary>
    public abstract string getName();

    /// <summary>
    /// Renvoie si l'automate est sur un état final ou non
    /// </summary>
    public bool isValid()
    {
        return (CurrentState == _finalState);
    }
    /// <summary>
    /// Renvoie l'id de l'état actuel (debug)
    /// </summary>
    /// <returns></returns>
    public int getCurrentState()
    {
        return CurrentState;
    }

    /// <summary>
    /// Renvoie le nombre d'états de l'automate 
    /// </summary>
    public int getNumberOfState()
    {
        return _finalState + 1;
    }

    /// <summary>
    /// Coeur de l'algorithme : un appel calcule et effectue le changement d'état de l'automate.
    /// </summary>
    /// <remarks>
    /// C'est dans calculateState que l'on crée l'automate et qu'on ajoute les fonctions
    /// de vérifications.
    /// </remarks>
    public abstract int calculateState(IFlyingObject plane);

    /// <summary>
    /// Détermine le changement d'état l'automate contenu dans la tableau SImpleautomata.figure[]
    /// </summary>
    /// <remarks> Appelé par calculateState </remarks>

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

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le premier quart de l'Aileron Roll
    /// </summary>
    /// <returns></returns>
    public bool Q1ARoll()
    {
        return (_upScalar >= 0 && _rightScalar <= 0);
    }

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le deuxième quart de l'Aileron Roll
    /// </summary>
    public bool Q2ARoll()
    {
        return (_upScalar <= 0 && _rightScalar <= 0);
    }
    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le Troisième quart de l'Aileron Roll
    /// </summary>
    /// <returns></returns>
    public bool Q3ARoll()
    {
        return (_upScalar <= 0 && _rightScalar >= 0);
    }

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le quartrième quart de l'Aileron Roll
    /// </summary>
    /// <returns></returns>
    public bool Q4ARoll()
    {
        return (_upScalar >= 0 && _rightScalar >= 0);
    }

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le premier quart du Loop
    /// </summary>
    public bool Q1Loop()
    {
        return ((_upScalar >= 0 && _forwardScalar >= 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le deuxième quart du Loop
    /// </summary>
    public bool Q2Loop()
    {
        return (_upScalar <= 0 && _forwardScalar >= 0 && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le troisième quart du Loop
    /// </summary>
    public bool Q3Loop()
    {
        return ((_upScalar <= 0 && _forwardScalar < 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    /// <summary>
    /// Retourne un bouléen permettant de savoir si l'avion est dans le quartrième quart du Loop
    /// </summary>
    public bool Q4Loop()
    {
        return ((_upScalar > 0 && _forwardScalar < 0) && (Math.Abs(_rightScalarStart - _rightScalar) < window));
    }

    /// <summary>
    /// Vérifie si le forward de l'avion reste constant de l'état 1 jusqu'au restant de la figure, reset sinon.
    /// </summary>
    public void checkForward()
    {
        if (state == 1)
        {
            _forwardScalarStart = _forwardScalar;
        }
        if (Math.Abs(_forwardScalarStart - _forwardScalar) > 0.3)
        {
            //unity.Debug.Log("CheckForward failed");
            resetStates();
        }
    }

    /// <summary>
    /// Initialise toutes les variables necessaires au traitement de l'automate
    /// </summary>
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

    /// <summary>
    /// Vérifie si l'écart d'altitude entre l'état 1 et l'état controlSttae est bien supérieur à minAltitude, reset sinon
    /// </summary>
    public void checkAltitude(IFlyingObject plane, int minAltitude, int controlState)
    {
        if (_forwardScalar <= 0.3 && state == 1)
        {
            altitude = plane.pos.Y;
        }
        if (_forwardScalar <= 0.2 && state == controlState)
        {
            if (plane.pos.Y < altitude + minAltitude)
            {
                //unity.Debug.Log("CheckAltitude failed");
                resetStates();
            }
        }
    }

    /// <summary>
    /// Laisse  un temps maxTime secondes au joueur pour effectuer chaque étape de la figure, reset sinon
    /// </summary>
    public void checkTime(int maxTime)
    {
        if (getCurrentState() > 1)
        {
            int tmpTime = 0;

            if (tmpState != getCurrentState())
            {
                tmpState = getCurrentState();
                time = DateTime.Now.Second;
            }
            else
            {  
                if (time + maxTime > 60)
                {
                    tmpTime = DateTime.Now.Second - 60;
                }
                else tmpTime = DateTime.Now.Second;
                //unity.Debug.Log(tmpTime + ", " + time + ", " + maxTime);
                if ((time + maxTime) % 60 <= tmpTime)
                {
                    //unity.Debug.Log("CheckTime failed");
                    resetStates();
                    tmpState = -1;
                    
                }
            }
        }
    }

}
