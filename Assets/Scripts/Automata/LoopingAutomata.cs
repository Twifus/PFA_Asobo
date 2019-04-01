using System;
using System.Collections.Generic;
using System.Numerics;
using unity = UnityEngine;

public class LoopingAutomata : SimpleAutomata {


    public LoopingAutomata() {
        int n = 5;
        CurrentState = 0;
        _finalState = n;
        DicoTransitions = new Dictionary<FSMDetection.StateTransition, int>();
        for (int i = 0; i < n; i++) {
            StateTransition t = new StateTransition(i, i + 1); //transition vers l'etat suivant
            DicoTransitions.Add(t, i + 1);
            //t = new StateTransition(i, 0); //transition vers l'etat de depart
            //DicoTransitions.Add(t, 0);
            t = new StateTransition(i, i); //transition vers l'etat courant (boucle)
            DicoTransitions.Add(t, i);
        }
    }

    //renvoie l'id de la figure représentée par FigureId
    public override figure_id getFigureId() {
        return figure_id.LOOP;
    }
    //affiche le nom de la figure que l'automate gère (debug)
    public override string getName() {
        return "Looping";
    }

    //calcule le nouvel état de l'automate étant donné la position passée en paramètre
    //renvoie 1 si le nouvel état est terminal (même résultat que isValid())
    //0 si le nouvel état est intermédiaire
    //-1 si l'automate recommence à l'état initial
    //si l'automate est déjà à l'état final, devrait renvoyer 1
    
    public override int calculateState(IFlyingObject plane)
    {
        init(plane);
        if (isValid()) return 1;
        checkAltitude(plane, 50, 3);
        checkTime(3);

        figure[0] = Q1Loop();
        figure[1] = Q2Loop();
        figure[2] = Q3Loop();
        figure[3] = Q4Loop();
        figure[4] = Q1Loop();


        process();

        //unity.Debug.Log("0 : " + Q1Loop() + ", 1 : " + Q2Loop() + ", 2 : " + Q3Loop() + ", 3 : " + Q4Loop());
        //unity.Debug.Log(_finalState);
        unity.Debug.Log("State : " + state);
        //unity.Debug.Log("_upScalar :" + _upScalar);
        //unity.Debug.Log("_rightScalar :" + _rightScalar);
        //unity.Debug.Log("_forwardScalar :" + _forwardScalar);
        //unity.Debug.Log(plane.pos.Y);
        //unity.Debug.Log(altitude);
        if (isValid()) return 1;
        return 0;
    }

    



    /*  ANCIENNE VERSION */

    /*
     public int calculateState(IFlyingObject plane) {
        _forwardScalar = Vector3.Dot(plane.forward, System.Numerics.Vector3.UnitY);
        _upScalar = Vector3.Dot(plane.up, System.Numerics.Vector3.UnitY);
        _rightScalar = Vector3.Dot(plane.right, System.Numerics.Vector3.UnitY);
        unity.Debug.Log("_upScalar :" + _upScalar);
        unity.Debug.Log("_forwardScalar :" + _forwardScalar);
        if (isValid()) return 1;
        
        int state = getCurrentState();
        unity.Debug.Log(state);

        //Set les checkers au debut du loop
        if (state ==0){
            _rightScalarStart = Vector3.Dot(plane.right, System.Numerics.Vector3.UnitY);
            
            if (_forwardScalar <= 0.3)
            {
                altitude = plane.pos.Y;
            }
        }
        //Verif si l'alitude est assez haute au sommet du loop
        if (_forwardScalar <= 0.2 && state == 2)
        {
            if (plane.pos.Y < altitude + 50)
                resetStates();
        }

        unity.Debug.Log(plane.pos.Y);
        unity.Debug.Log(altitude);
        if (_upScalar <= 0 && _forwardScalar >= 0){          
            if ((state == 0 || state == 1) && (Math.Abs(_rightScalarStart - _rightScalar) < window)){
                MoveNext(1);
            } else resetStates();
        } else if (_upScalar <= 0 && _forwardScalar < 0){
            if ((state == 1 || state == 2) && (Math.Abs(_rightScalarStart - _rightScalar) < window)){
                MoveNext(2);
            } else resetStates();
        } else if (_upScalar > 0 && _forwardScalar < 0){
            if ((state == 2 || state == 3) && (Math.Abs(_rightScalarStart - _rightScalar) < window)){
                MoveNext(3);
            } else resetStates();
        } else if (_upScalar >= 0 && _forwardScalar >= 0){
            if ((state == 3 || state == 4) && (Math.Abs(_rightScalarStart - _rightScalar) < window)){
                MoveNext(4);
            } else resetStates();
        } 

        if (isValid()) return 1;
        return 0;
    }*/
}
 