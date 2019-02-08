using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour {

    public int mode = -1;
    private int step = 0;
    private float ts = 0f;

	// Use this for initialization
	void Start ()
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
                NextStep();
                break;
            case 1:
                Wait(5f);
                break;
            case 2:
                CustomInput.SetAxis("Pitch", 1f);
                NextStep();
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
	void Update () {
		switch (mode)
        {
            case 0:
                Looping();
                break;
            case 1:
                Roll();
                break;
            default:
                break;

        }
	}
}
