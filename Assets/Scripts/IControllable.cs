using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void MoveTo(Vector3 direction, float speed);

    void ForceRotateTo(Vector3 direction, Transform transform);
}
