﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recognizer1Dollar;

public class TestFileLoader : MonoBehaviour {
    
	void Start () {
        FigureLoader f = new FigureLoader(new Recognizer(), new Recognizer(), new Recognizer(), new Recognizer());
        f.LoadFigures();
	}
}
