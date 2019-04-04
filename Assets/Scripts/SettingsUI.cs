using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère l'affichage du menu de jeu
/// </summary>
public class SettingsUI : MonoBehaviour
{
    public GUISkin guiSkin;
    public Texture2D background, LOGO;
    public bool DragWindow = true;
    public string[] AboutTextLines = new string[0];

    private string clicked = "", MessageDisplayOnAbout = "About \n ";
    private Rect WindowRect;
    private Vector2 scrollPosition;

    private float volume = 1.0f;
    private float UserWingArea;
    private float UserLiftCoeff;
    private float UserDragCoeff;
    private float UserThrustPower;
    private float UserRollIntensity;
    private float UserYawIntensity;

    private void Start()
    {
        UpdateMenuSize();
        for (int x = 0; x < AboutTextLines.Length; x++)
        {
            MessageDisplayOnAbout += AboutTextLines[x] + " \n ";
        }
        MessageDisplayOnAbout += "Press Esc To Go Back";
        UserWingArea = PlaneSettings.WingArea;
        UserLiftCoeff = PlaneSettings.LiftCoeff;
        UserDragCoeff = PlaneSettings.DragCoeff;
        UserThrustPower = PlaneSettings.ThrustPower;
        UserRollIntensity = PlaneSettings.RollIntensity;
        UserYawIntensity = PlaneSettings.YawIntensity;
    }

    private void UpdateMenuSize()
    {
        WindowRect = new Rect(Screen.width / 8f, Screen.height / 6f, Screen.width / 4f, 2f * Screen.height / 3f);
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
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width / 5f), GUILayout.Height(8f * Screen.height / 10f));
            for (int x = 0; x < Screen.resolutions.Length; x++)
            {
                if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height))
                {
                    Screen.SetResolution(Screen.resolutions[x].width, Screen.resolutions[x].height, true);
                    UpdateMenuSize();
                }
            }
            GUILayout.EndScrollView();
            if (GUILayout.Button("Back"))
            {
                clicked = "options";
            }
        }
        else if (clicked == "algorithm")
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Pattern Matching ($P)"))
            {
                FigureManager.detector = FigureManager.Detector.Dollar;
                clicked = "options";
            }
            else if (GUILayout.Button("State Machines (Automata)"))
            {
                FigureManager.detector = FigureManager.Detector.Automata;
                clicked = "options";
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

    private float Slider(string name, float f, float max)
    {
        GUILayout.Box(name + " : " + f);
        return GUILayout.HorizontalSlider(f, 0.0f, max);
        
    }

    private void optionsFunc(int id)
    {
        if (GUILayout.Button("Resolution" + " (" + Screen.currentResolution.width + "x" + Screen.currentResolution.height + ")"))
        {
            clicked = "resolution";
        }
        if (GUILayout.Button("Algorithme" + " (" + FigureManager.DetectorName[(int)FigureManager.detector] + ")"))
        {
            clicked = "algorithm";
        }
        volume = Slider("Volume", volume, 1.0f);
        UserWingArea = Slider("Wing Area", UserWingArea, 100.0f);
        UserLiftCoeff = Slider("Lift Coefficient", UserLiftCoeff, 100.0f);
        UserDragCoeff = Slider("Drag Coefficient", UserDragCoeff, 100.0f);
        UserThrustPower = Slider("Thrust Power", UserThrustPower, 100.0f);
        UserRollIntensity = Slider("Roll Intensity", UserRollIntensity, 2f);
        UserYawIntensity = Slider("Yaw Intensity", UserYawIntensity, 2f);

        AudioListener.volume = volume;
        PlaneSettings.WingArea = UserWingArea;
        PlaneSettings.LiftCoeff = UserLiftCoeff;
        PlaneSettings.DragCoeff = UserDragCoeff;
        PlaneSettings.ThrustPower = UserThrustPower;
        PlaneSettings.RollIntensity = UserRollIntensity;
        PlaneSettings.YawIntensity = UserYawIntensity;

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
            SceneManager.LoadScene("world1");
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
        if ((clicked == "about" || clicked == "options") && Input.GetKey(KeyCode.Escape))
            clicked = "";
    }
}