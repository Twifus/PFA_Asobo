using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput {

    private static Dictionary<string, bool> simulate = new Dictionary<string, bool>();
    private static Dictionary<string, float> values =  new Dictionary<string, float>();

    private static void AddAxis(string axis)
    {
        simulate.Add(axis, false);
        values.Add(axis, 0);
    }

    public static void ToggleDummyInput(string axis) {
        if (!simulate.ContainsKey(axis))
            AddAxis(axis);

        simulate[axis] = !simulate[axis];
    }

    public static float GetAxis(string axis)
    {
        if (!simulate.ContainsKey(axis))
            AddAxis(axis);

        if (simulate[axis])
            return values[axis];
        else
            return Input.GetAxis(axis);
    }

    public static void SetAxis(string axis, float value)
    {
        if (!simulate.ContainsKey(axis))
            AddAxis(axis);

        if (values.ContainsKey(axis))
            values[axis] = value;
        else
            values.Add(axis, value);
    }
}
