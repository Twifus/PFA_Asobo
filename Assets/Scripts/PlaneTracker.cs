using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using s = System.Numerics;
using System.Globalization;

/// <summary>
/// Permet l'écriture dans un fichier les coordonnées successives de l'avion au cours du temps
/// </summary>
/// <remarks>
/// Pour démarrer un enregistrement, l'utilisateur doit maintenair le bouton correspondant pendant un court délai.
/// </remarks>
public class PlaneTracker : MonoBehaviour {

    private StreamWriter FileWriter;

    /// <summary>
    /// Instance de Plane associée à l'avion suivi
    /// </summary>
    private Plane Plane;

    /// <summary>
    /// GameObject de l'avion suivi
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Component RawImage indicateur d'enregistrement
    /// </summary>
    public RawImage recordImage;

    /// <summary>
    /// Temps du dernier clignotement
    /// </summary>
    private float blinkTime;

    /// <summary>
    /// Delai de clignotement
    /// </summary>
    private float blinkDelay = 0.5f;

    /// <summary>
    /// Temps du premier appui sur le bouton d'enregistrement
    /// </summary>
    private float ts;

    /// <summary>
    /// Delai d'appui pour démarrer l'enregistrement
    /// </summary>
    private float pressDelay = 0.3f;

    /// <summary>
    /// Etat de l'appui sur le bouton d'enregistrement
    /// </summary>
    private bool buttonPress;

    /// <summary>
    /// Statut de l'enregistrement
    /// </summary>
    private bool record;

    public void Start()
    {
        Plane = Plane.NewPlane(Player);
        recordImage.enabled = false;
    }

    /// <summary>
    /// Ferme le fichier courant lorsque la scène est quittée
    /// </summary>
    private void OnApplicationQuit()
    {
        if (FileWriter != null)
            FileWriter.Close();
    }

    /// <summary>
    /// Vérifie si l'utilisateur démarre/arrête un enregistrement, et ouvre/ferme un fichier si nécessaire
    /// </summary>
    private void Update()
    {
        if (Input.GetButtonDown("ToggleRecord"))
        {
            ts = Time.time;
            buttonPress = true;
        }

        if (buttonPress && Input.GetButton("ToggleRecord") && Time.time > ts + pressDelay)
        {
            if (!record)
            {
                string path = string.Format("../Record-{0}", System.DateTime.Now.ToFileTime());
                FileWriter = new StreamWriter(path + ".csv", true);
                FileWriter.WriteLine(
                    "Time;" +
                    "X;Y;Z;" +
                    "Vx;Vy;Vz;" +
                    "Roll;Pitch;Yaw;" +
                    "pRight.wRight;pRight.wUp;pRight.wForward;" +
                    "pUp.wRight;pUp.wUp;pUp.wForward;" +
                    "pForward.wRight;pForward.wUp;pForward.wForward;" +
                    "ScalarRoll;ScalarPitch;ScalarYaw;" +
                    "InputAccelerate;" +
                    "InputRoll;InputPitch;InputRoll");
                recordImage.enabled = true;
                blinkTime = Time.time;
                Debug.Log("Record started - Writting at " + path);
            }
            else
            {
                FileWriter.Close();
                recordImage.enabled = false;
                Debug.Log("Record stopped");
            }
            record = !record;
            buttonPress = false;
        }
    }

    /// <summary>
    /// Enregistre périodiquement les coordonnées de l'avion dans le fichier
    /// </summary>
    private void FixedUpdate()
    {
        if (record)
        {
            if (Time.time > blinkTime + blinkDelay)
            {
                recordImage.enabled = !recordImage.enabled;
                blinkTime = Time.time;
            }
            // Convert data to invariable culture strings
            float[] data = {
                Time.time,
                Plane.pos.X, Plane.pos.Y, Plane.pos.Z,
                Plane.speed.X, Plane.speed.Y, Plane.speed.Z,
                Plane.roll, Plane.pitch, Plane.yaw,
                s.Vector3.Dot(Plane.right, s.Vector3.UnitX), s.Vector3.Dot(Plane.right, s.Vector3.UnitY), s.Vector3.Dot(Plane.right, s.Vector3.UnitZ),
                s.Vector3.Dot(Plane.up, s.Vector3.UnitX), s.Vector3.Dot(Plane.up, s.Vector3.UnitY), s.Vector3.Dot(Plane.up, s.Vector3.UnitZ),
                s.Vector3.Dot(Plane.forward, s.Vector3.UnitX), s.Vector3.Dot(Plane.forward, s.Vector3.UnitY), s.Vector3.Dot(Plane.forward, s.Vector3.UnitZ),
                Plane.rollScalar, Plane.pitchScalar, Plane.yawScalar,
                CustomInput.GetAxis("Accelerate"), CustomInput.GetAxis("Roll"), CustomInput.GetAxis("Pitch"), CustomInput.GetAxis("Yaw")
            };
            List<string> sdata = new List<string>();
            foreach(float f in data)
            {
                sdata.Add(f.ToString(CultureInfo.InvariantCulture));
            }
            // Write in file
            FileWriter.WriteLine(string.Join(";", sdata));
        }
    }

}
