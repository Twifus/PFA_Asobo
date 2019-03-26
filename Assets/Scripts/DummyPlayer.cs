using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class DummyPlayer : MonoBehaviour {

    public Text modeText;
    public Text selectionText;

    private int mode = 0;
    private int selection = 0;

    private int nModes = 3;
    private string[] nameModes = { "Aucun", "Loop", "Roll", "Cuban8" };

    public GameObject Player;

    private Plane _plane;

    private StreamReader file;

	// Use this for initialization
	void Start ()
    {
        //Debug.Log("Selected mode " + 0);
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
        CultureInfo ci = new CultureInfo("en-US");
        CustomInput.SetAxis("Accelerate", float.Parse(data[1], ci));
        CustomInput.SetAxis("Roll", float.Parse(data[2], ci));
        CustomInput.SetAxis("Pitch", float.Parse(data[3], ci));
        CustomInput.SetAxis("Yaw", float.Parse(data[4], ci));
    }

    private void toggleDummy()
    {
        CustomInput.ToggleDummyInput("Accelerate");
        CustomInput.ToggleDummyInput("Pitch");
        CustomInput.ToggleDummyInput("Roll");
        CustomInput.ToggleDummyInput("Yaw");
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("SelectReplay"))
        {
            selection = (selection + 1) % (nModes + 1);
            selectionText.text = "Selection : " + nameModes[selection];
            //Debug.Log("Selected mode " + selection);
        }

        if (Input.GetButtonDown("StartReplay") && (selection != mode))
        {
            if ((mode == 0 && selection != 0) || (mode != 0 && selection == 0))
                toggleDummy();
            //Debug.Log("Mode changed: " + mode + "->" + selection);
            mode = selection;
            modeText.text = "Actif : " + nameModes[selection];
        }
        
        switch (mode)
        {
            case 1:
                replayFromFile("Assets/Figures/Loop-Input.csv");
                break;
            case 2:
                replayFromFile("Assets/Figures/Roll-Input.csv");
                break;
            case 3:
                replayFromFile("Assets/Figures/CubanEight-Input.csv");
                break;
            default:
                break;

        }
	}
}
