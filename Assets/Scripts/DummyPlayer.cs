using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour {

    private int mode = 0;
    private int selection = 0;
    private int step = 0;
    private float ts = 0f;

    private int nModes = 2;

    public GameObject Player;

    private Plane _plane;

	// Use this for initialization
	void Start ()
    {
        Debug.Log(0);
        _plane = Plane.NewPlane(Player);
    }

    private void toggleDummy()
    {
        CustomInput.ToggleDummyInput("Accelerate");
        CustomInput.ToggleDummyInput("Pitch");
        CustomInput.ToggleDummyInput("Roll");
        CustomInput.ToggleDummyInput("Yaw");
    }

    private void NextStep()
    {
        step++;
        ts = Time.time;
    }
	
    private void Wait(float t)
    {
        if (Time.time - ts > t)
        {
            NextStep();
        }
    }
    
    private void Looping()
    {
        switch (step)
        {
            case 0:
                CustomInput.SetAxis("Accelerate", 1f);
                CustomInput.SetAxis("Pitch", 0f);
                CustomInput.SetAxis("Roll", 0f);
                CustomInput.SetAxis("Yaw", 0f);
                NextStep();
                break;
            case 1:
                Wait(5f);
                break;
            case 2:
                CustomInput.SetAxis("Pitch", 1f);
                NextStep();
                break;
            case 3:
                if (_plane.Pitch < 0f)
                    NextStep();
                break;
            case 4:
                if (_plane.Pitch > 5f)
                    NextStep();
                break;
            case 5:
                CustomInput.SetAxis("Pitch", 0f);
                break;
            default:
                break;
        }
    }

    private void Roll()
    {
        switch (step)
        {
            case 0:
                CustomInput.SetAxis("Accelerate", 1f);
                CustomInput.SetAxis("Pitch", 0f);
                CustomInput.SetAxis("Roll", 0f);
                CustomInput.SetAxis("Yaw", 0f);
                NextStep();
                break;
            case 1:
                Wait(5f);
                break;
            case 2:
                CustomInput.SetAxis("Pitch", 0.2f);
                NextStep();
                break;
            case 3:
                Wait(1f);
                break;
            case 4:
                CustomInput.SetAxis("Pitch", 0f);
                CustomInput.SetAxis("Roll", 1f);
                break;
            default:
                break;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            selection = (selection + 1) % (nModes + 1);
            Debug.Log(selection);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            selection = (nModes + selection) % (nModes + 1);
            Debug.Log(selection);
        }

        if ((Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.JoystickButton3)) && (selection != mode))
        {
            if ((mode == 0 && selection != 0) || (mode != 0 && selection == 0))
                toggleDummy();
            Debug.Log("Mode changed: " + mode + "->" + selection);
            mode = selection;
            step = 0;
            ts = 0f;
        }
        
        switch (mode)
        {
            case 0:

                break;
            case 1:
                Looping();
                break;
            case 2:
                Roll();
                break;
            default:
                break;

        }
	}
}
