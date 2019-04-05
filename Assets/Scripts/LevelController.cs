using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère l'accès au menu et le respawn depuis un niveau de jeu
/// </summary>
public class LevelController : MonoBehaviour {
	void Update () {
        if (Input.GetButton("Menu"))
        {
            SceneManager.LoadScene("SettingsUI");
        }

        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
