using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour {

    public GameObject Player;

    public int MaxPlot;

    private LineRenderer _lr;
    private List<Vector3> _path;

    private bool _active = false;

    // Use this for initialization
    void Start () {
        _lr = GetComponent<LineRenderer>();
        _path = new List<Vector3>();
    }
	
    public void toggle ()
    {
        _active = !_active;
    }

	// Update is called once per frame
	void Update () {
        if (_active)
        {
            _path.Add(Player.transform.position);
            _path.RemoveRange(0, Mathf.Max(0, _path.Count - MaxPlot));
            _lr.positionCount = _path.Count;
            _lr.SetPositions(_path.ToArray());
        }
    }
}
