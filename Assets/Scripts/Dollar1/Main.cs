using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;
using Recognizer1Dollar;

public class Main : MonoBehaviour
{

    void Start()
    {
        List<TimePointF> _points1 = new List<TimePointF>(180);
        List<TimePointF> _points2 = new List<TimePointF>(180);
        List<TimePointF> _points3 = new List<TimePointF>(180);
        Recognizer _rec = new Recognizer();
        NBestList result;
        int time = 1000;
        int i;

        _rec.LoadGesture(".\\Assets\\Scripts\\Dollar1\\line.xml");
        _rec.LoadGesture(".\\Assets\\Scripts\\Dollar1\\circle.xml");
        _rec.LoadGesture(".\\Assets\\Scripts\\Dollar1\\triangle.xml");

        for (i = 0; i < 180; i++)
        {
            time += 10;
            _points1.Add(new TimePointF(50+i, 100, time));
        }
        result = _rec.Recognize(_points1, false);
        Debug.Log(result.Name);
        Debug.Log(result.Score);

        for (i = 0; i < 60; i++)
        {
            time += 10;
            _points2.Add(new TimePointF(i, i, time));
        }
        for (i = 60; i < 120; i++)
        {
            time += 10;
            _points2.Add(new TimePointF(i, 120-i, time));
        }
        for (i = 120; i < 180; i++)
        {
            time += 10;
            _points2.Add(new TimePointF(180-i, 0, time));
        }
        result = _rec.Recognize(_points2, false);
        Debug.Log(result.Name);
        Debug.Log(result.Score);

        for (i = 0; i < 180; i++)
        {
            time += 10;
            _points3.Add(new TimePointF(200 + 100*Math.Cos(2*i * Math.PI / 180), 200 + 100*Math.Sin(2*i * Math.PI / 180), time));
        }
        result = _rec.Recognize(_points3, false);
        Debug.Log(result.Name);
        Debug.Log(result.Score);

    }

}