using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DummyPlayer : MonoBehaviour {

    private int mode = 0;
    private int selection = 0;
    private int step = 0;
    private float ts = 0f;

    private int nModes = 3;

    public GameObject Player;

    private Plane _plane;

    private StreamReader file;

	// Use this for initialization
	void Start ()
    {
        Debug.Log(0);
        _plane = Plane.NewPlane(Player);
    }

    private void replayFromFile(string path)
    {
        if (file == null)
        {
            file = new StreamReader(path);
        }
        string line = file.ReadLine();
        if (line == null)
        {
            file.Close();
            file = null;
            return;
        }
        string[] data = line.Split(';');
        CustomInput.SetAxis("Accelerate", float.Parse(data[1]));
        CustomInput.SetAxis("Roll", float.Parse(data[2]));
        CustomInput.SetAxis("Pitch", float.Parse(data[3]));
        CustomInput.SetAxis("Yaw", float.Parse(data[4]));
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
        if (Input.GetButtonDown("SelectReplay"))
        {
            selection = (selection + 1) % (nModes + 1);
            Debug.Log(selection);
        }

        if (Input.GetButtonDown("StartReplay") && (selection != mode))
        {
            if ((mode == 0 && selection != 0) || (mode != 0 && selection == 0))
                toggleDummy();
            Debug.Log("Mode changed: " + mode + "->" + selection);
            mode = selection;
            //step = 0;
            //ts = 0f;
        }
        
        switch (mode)
        {
            case 1:
                replayFromFile("Assets/Loop-Input.csv");
                break;
            case 2:
                replayFromFile("Assets/Roll-Input.csv");
                break;
            case 3:
                replayFromFile("Assets/CubanEight-Input.csv");
                break;
            default:
                break;

        }
	}
}
