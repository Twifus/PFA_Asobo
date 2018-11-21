using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour {

    public GameObject Player;

    private float _lastPlot;
    public float DeltaPlot;
    public int MaxPlot;

    private LineRenderer _lr;
    private List<Vector3> _path;

    // Use this for initialization
    void Start () {
        _lr = GetComponent<LineRenderer>();
        _lastPlot = 0;
        _path = new List<Vector3>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - _lastPlot > DeltaPlot)
        {
            _lastPlot = Time.time;
            _path.Add(Player.transform.position);
            _path.RemoveRange(0, Mathf.Max(0, _path.Count - MaxPlot));
            _lr.positionCount = _path.Count;
            _lr.SetPositions(_path.ToArray());
        }
    }
}
