using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

/// <summary>
/// Contrôleur de jeu fictif rejouant des séquences enregistrées
/// </summary>
public class DummyPlayer : MonoBehaviour {

    /// <summary>
    /// Component Text affichant le mode actif
    /// </summary>
    public Text modeText;

    /// <summary>
    /// Component Text affichant le mode sélectionné
    /// </summary>
    public Text selectionText;

    /// <summary>
    /// Indice du mode actif
    /// </summary>
    private int mode = 0;

    /// <summary>
    /// Indice du mode sélectionné
    /// </summary>
    private int selection = 0;

    /// <summary>
    /// Nom des modes disponibles
    /// </summary>
    private string[] nameModes = { "Aucun", "Loop", "Roll", "Cuban8" };

    /// <summary>
    /// GameObject du joueur 
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Classe Plane associée au joueur
    /// </summary>
    private Plane _plane;

    /// <summary>
    /// Lecteur de flux lisant les séquences enregistrées
    /// </summary>
    private StreamReader file;


	void Start ()
    {
        _plane = Plane.NewPlane(Player);
    }

    /// <summary>
    /// Rejoue une frame d'une séquence de référence
    /// </summary>
    /// <remarks>
    /// Lis une ligne du fichier en paramètre, extrait les inputs de cette ligne, et modifie les valeurs des axes via CustomInput.
    /// Si la fin du fichier est atteint, le fichier est fermé.
    /// </remarks>
    // <param name="path">Chemin du fichier contenant la figure de référence</param>
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
        CultureInfo ci = CultureInfo.InvariantCulture;
        CustomInput.SetAxis("Accelerate", float.Parse(data[1], ci));
        CustomInput.SetAxis("Roll", float.Parse(data[2], ci));
        CustomInput.SetAxis("Pitch", float.Parse(data[3], ci));
        CustomInput.SetAxis("Yaw", float.Parse(data[4], ci));
    }

    /// <summary>
    /// (Dés)active l'émulation des inputs contrôlant l'avion
    /// </summary>
    private void toggleDummy()
    {
        CustomInput.ToggleDummyInput("Accelerate");
        CustomInput.ToggleDummyInput("Pitch");
        CustomInput.ToggleDummyInput("Roll");
        CustomInput.ToggleDummyInput("Yaw");
    }
    
    /// <remarks>
    /// Chaque frame, scrute les entrées de l'utilisateur pour changer le mode de simulation en conséquence.
    /// Puis, si une séquence doit être rejouée, simule une frame de cette séquence.
    /// </remarks>
	void Update ()
    {
        if (Input.GetButtonDown("SelectReplay"))
        {
            selection = (selection + 1) % (nameModes.Length);
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
