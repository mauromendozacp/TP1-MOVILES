using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    static InputManager instance = null;

    public static InputManager Instance
    {
        get { if (instance == null) instance = new InputManager(); return instance; }
    }

    Dictionary<string, float> axisValues = new Dictionary<string, float>()
    {
        {"Horizontal1", 0f},{"Vertical", 0f},
        {"Horizontal2", 0f},
    };

    public void SetAxis(string axis, float value)
    {
        axisValues[axis] = value;
    }

    public float GetAxis(string axis)
    {
#if UNITY_EDITOR
        return axisValues[axis] + Input.GetAxis(axis);
#elif UNITY_ANDROID || UNITY_IOS
        return axisValues[axis];
#elif UNITY_STANDALONE
        return Input.GetAxis(axis);
#endif
    }

    public bool GetButton(string button)
    {
        return Input.GetButton(button);
    }
}