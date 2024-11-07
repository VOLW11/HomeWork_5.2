using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private int _leftMouseButton = 0;
    private Vector3 _mousePosition;

    public event Action<Vector3> OnClickMouse;


    void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            _mousePosition = Input.mousePosition;

            OnClickMouse?.Invoke(_mousePosition);
        }
    }
}
