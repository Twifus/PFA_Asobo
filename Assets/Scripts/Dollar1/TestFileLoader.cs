using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFileLoader : MonoBehaviour {
    
	void Start () {
        FigureLoader f = new FigureLoader(new Recognizer());
        f.LoadFigures();
	}
}
