﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour
{
    public GUISkin guiSkin;
    public Texture2D background, LOGO;
    public bool DragWindow = false;
    public string levelToLoadWhenClickedPlay = "world1";
    public string[] AboutTextLines = new string[0];

    private string clicked = "", MessageDisplayOnAbout = "About \n ";
    private Rect WindowRect = new Rect(700, 300, Screen.width-1400, Screen.height-600);
    private float volume = 1.0f;
    private float UserWingArea = 5.0f;
    private float UserLiftCoeff = 10.0f;
    private float UserDragCoeff = 20.0f;
    private float UserThrustPower = 50.0f;
    private float UserRollIntensity = 30.0f;
    private float UserPitchIntensity = 30.0f;
    private float UserYawIntensity = 30.0f;
    private BasicPlaneControllerRotated PlaneController;

    private void Start()
    {
        PlaneController = GetComponent<BasicPlaneControllerRotated>();
        for (int x = 0; x < AboutTextLines.Length; x++)
        {
            MessageDisplayOnAbout += AboutTextLines[x] + " \n ";
        }
        MessageDisplayOnAbout += "Press Esc To Go Back";
    }

    private void OnGUI()
    {
        if (background != null)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
        if (LOGO != null && clicked != "about")
            GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);

        GUI.skin = guiSkin;
        if (clicked == "")
        {
            WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu");
        }
        else if (clicked == "options")
        {
            WindowRect = GUI.Window(1, WindowRect, optionsFunc, "Options");
        }
        else if (clicked == "about")
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), MessageDisplayOnAbout);
        }
        else if (clicked == "resolution")
        {
            GUILayout.BeginVertical();
            for (int x = 0; x < Screen.resolutions.Length; x++)
            {
                if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height))
                {
                    Screen.SetResolution(Screen.resolutions[x].width, Screen.resolutions[x].height, true);
                }
            }
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Back"))
            {
                clicked = "options";
            }
            GUILayout.EndHorizontal();
        }
    }

    private void optionsFunc(int id)
    {
        if (GUILayout.Button("Resolution"))
        {
            clicked = "resolution";
        }
        GUILayout.Box("Volume");
        volume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f);
        AudioListener.volume = volume;
        GUILayout.Box("WingArea");
        UserWingArea = GUILayout.HorizontalSlider(UserWingArea, 0.0f, 100.0f);
        PlaneController.WingArea = UserWingArea;
        GUILayout.Box("LiftCoeff");
        UserLiftCoeff = GUILayout.HorizontalSlider(UserLiftCoeff, 0.0f, 100.0f);
        PlaneController.LiftCoeff = UserLiftCoeff;
        GUILayout.Box("DragCoeff");
        UserDragCoeff = GUILayout.HorizontalSlider(UserDragCoeff, 0.0f, 100.0f);
        PlaneController.DragCoeff = UserDragCoeff;
        GUILayout.Box("ThrustPower");
        UserThrustPower = GUILayout.HorizontalSlider(UserThrustPower, 0.0f, 100.0f);
        PlaneController.ThrustPower = UserThrustPower;
        GUILayout.Box("RollIntensity");
        UserRollIntensity = GUILayout.HorizontalSlider(UserRollIntensity, 0.0f, 360.0f);
        PlaneController.RollIntensity = UserRollIntensity;
        GUILayout.Box("PitchIntensity");
        UserPitchIntensity = GUILayout.HorizontalSlider(UserPitchIntensity, 0.0f, 360.0f);
        PlaneController.PitchIntensity = UserPitchIntensity;
        GUILayout.Box("YawIntensity");
        UserYawIntensity = GUILayout.HorizontalSlider(UserYawIntensity, 0.0f, 360.0f);
        PlaneController.YawIntensity = UserYawIntensity;
        if (GUILayout.Button("Back"))
        {
            clicked = "";
        }
        if (DragWindow)
            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }

    private void menuFunc(int id)
    {
        //buttons 
        if (GUILayout.Button("Play Game"))
        {
            //play game is clicked
            SceneManager.LoadScene(levelToLoadWhenClickedPlay);
        }
        if (GUILayout.Button("Options"))
        {
            clicked = "options";
        }
        if (GUILayout.Button("About"))
        {
            clicked = "about";
        }
        if (GUILayout.Button("Quit Game"))
        {
            Application.Quit();
        }
        if (DragWindow)
            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }

    private void Update()
    {
        if (clicked == "about" && Input.GetKey(KeyCode.Escape))
            clicked = "";
    }
}