using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public GameObject Player;
    public GameObject Camera;

    private Rigidbody _rb;

	// Use this for initialization
	void Start () {
        _rb = Player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Menu"))
        {
            SceneManager.LoadScene("SettingsUI");
        }

        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Player.transform.position = transform.position;
            //Player.transform.rotation = transform.rotation;
            //_rb.velocity = new Vector3(0f, 0f, 0f);
            //_rb.angularVelocity = new Vector3(0f, 0f, 0f);
            //Camera.transform.position = transform.position;
            //Camera.transform.rotation = transform.rotation;
        }
    }
}
