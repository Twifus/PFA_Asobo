using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour
{
    public GUISkin guiSkin;
    public Texture2D background, LOGO;
    public bool DragWindow = true;
    public string[] AboutTextLines = new string[0];

    private string clicked = "", MessageDisplayOnAbout = "About \n ";
    private Rect WindowRect = new Rect(3*Screen.width/7, Screen.height/3, 300, 500);
    private float volume = 1.0f;
    private float UserWingArea = PlaneSettings.WingArea;
    private float UserLiftCoeff = PlaneSettings.LiftCoeff;
    private float UserDragCoeff = PlaneSettings.DragCoeff;
    private float UserThrustPower = PlaneSettings.ThrustPower;
    private float UserThrustCoeff = PlaneSettings.ThrustCoeff;
    private float UserRollIntensity = PlaneSettings.RollIntensity;
    private float UserPitchIntensity = PlaneSettings.PitchIntensity;
    private float UserYawIntensity = PlaneSettings.YawIntensity;

    private void Start()
    {
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

    private float Slider(string name, float f, float max)
    {
        GUILayout.Box(name + " : " + f);
        return GUILayout.HorizontalSlider(f, 0.0f, max);
        
    }

    private void optionsFunc(int id)
    {
        if (GUILayout.Button("Resolution"))
        {
            clicked = "resolution";
        }
        volume = Slider("Volume", volume, 1.0f);
        UserWingArea = Slider("Wing Area", UserWingArea, 100.0f);
        UserLiftCoeff = Slider("Lift Coefficient", UserLiftCoeff, 100.0f);
        UserDragCoeff = Slider("Drag Coefficient", UserDragCoeff, 100.0f);
        UserThrustPower = Slider("Thrust Power", UserThrustPower, 100.0f);
        UserRollIntensity = Slider("Roll Intensity", UserRollIntensity, 360.0f);
        UserPitchIntensity = Slider("Pitch Intensity", UserPitchIntensity, 360.0f);
        UserYawIntensity = Slider("Yaw Intensity", UserYawIntensity, 360.0f);

        AudioListener.volume = volume;
        BasicPlaneControllerRotated.WingArea = UserWingArea;
        BasicPlaneControllerRotated.LiftCoeff = UserLiftCoeff;
        BasicPlaneControllerRotated.DragCoeff = UserDragCoeff;
        BasicPlaneControllerRotated.ThrustPower = UserThrustPower;
        BasicPlaneControllerRotated.ThrustCoeff = UserThrustCoeff;
        BasicPlaneControllerRotated.RollIntensity = UserRollIntensity;
        BasicPlaneControllerRotated.PitchIntensity = UserPitchIntensity;
        BasicPlaneControllerRotated.YawIntensity = UserYawIntensity;

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
        if (clicked == "about" && Input.GetKey(KeyCode.Escape))
            clicked = "";
    }
}