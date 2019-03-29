using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;
using PDollarGestureRecognizer;

public class MainP : MonoBehaviour
{

    void Start()
    {
        Point[] _points1 = new Point[180];
        Point[] _points2 = new Point[180];
        Point[] _points3 = new Point[180];
        Gesture[] _gestures = new Gesture[3];
        BestGesture result;
        int i;

        _gestures[0] = GestureIO.ReadGesture(".\\Assets\\Scripts\\DollarP\\line.xml");
        _gestures[1] = GestureIO.ReadGesture(".\\Assets\\Scripts\\DollarP\\triangle.xml");
        _gestures[2] = GestureIO.ReadGesture(".\\Assets\\Scripts\\DollarP\\circle.xml");

        for (i = 0; i < 180; i++)
        {
            _points1[i] = new Point(50 + i, 100, 0);
        }
        Gesture _gesture1 = new Gesture(_points1, "line");
        result = PointCloudRecognizer.Classify(_gesture1, _gestures);
        Debug.Log(result.Name + " " + result.Score);

        for (i = 0; i < 60; i++)
        {
            _points2[i] = new Point(i, i, 0);
        }
        for (i = 60; i < 120; i++)
        {
            _points2[i] = new Point(i, 120-i, 0);
        }
        for (i = 120; i < 180; i++)
        {
            _points2[i] = new Point(180-i, 0, 0);
        }
        Gesture _gesture2 = new Gesture(_points2, "triangle");
        result = PointCloudRecognizer.Classify(_gesture2, _gestures);
        Debug.Log(result.Name + " " + result.Score);

        for (i = 0; i < 180; i++)
        {
            _points3[i] = new Point(200 + 100*Math.Cos(2*i * Math.PI / 180), 200 + 100*Math.Sin(2*i * Math.PI / 180), 0);
        }
        Gesture _gesture3 = new Gesture(_points3, "circle");
        result = PointCloudRecognizer.Classify(_gesture3, _gestures);
        Debug.Log(result.Name + " " + result.Score);
    }

}