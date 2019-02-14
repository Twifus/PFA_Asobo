using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;
//using Recognizer.Dollar;


namespace Recognizer.Dollar
{

    public class Main : MonoBehaviour
    {

        

        void Start()
        {
            List<TimePointF> _points = new List<TimePointF>(256);
            Recognizer _rec = new Recognizer();
            int time = 1000;
            _rec.LoadGesture("C:\\Users\\Adrien\\Documents\\GitHub\\PFA_Asobo\\Assets\\Scripts\\$1\\line.xml");
            _rec.LoadGesture("C:\\Users\\Adrien\\Documents\\GitHub\\PFA_Asobo\\Assets\\Scripts\\$1\\circle.xml");
            _rec.LoadGesture("C:\\Users\\Adrien\\Documents\\GitHub\\PFA_Asobo\\Assets\\Scripts\\$1\\triangle.xml");
            for (int i = 0; i < 256; i++)
            {
                time += 10;
                _points.Add(new TimePointF(100, i, time));
            }
            NBestList result = _rec.Recognize(_points, false);
            Debug.Log(result.Name);
            Debug.Log(result.Score);
            
        }

    }
}