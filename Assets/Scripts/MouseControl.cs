using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseControl
{
    public event Action<Vector3> HitInfoPoint;
    public event Action<Vector3> HitInfoPathError;

    private InputHandler _inputHandler;

    public MouseControl(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;

        _inputHandler.OnClickMouse += MovePosition;
    }

    private void MovePosition(Vector3 mousePosition)
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ground ground = hitInfo.collider.GetComponent<Ground>();

            if (ground != null)
            {
                HitInfoPoint?.Invoke(hitInfo.point);
            }
            else
            {
                HitInfoPathError?.Invoke(hitInfo.point);
            }
        }
    }
}
