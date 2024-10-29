using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement
{
    private CharacterController _characterController;
    private float _speed;

    public Movement(CharacterController characterController, float speed)
    {
        _characterController = characterController;
        _speed = speed;
    }

    public void MoveTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
        _characterController.transform.rotation = lookRotation;

        Vector3 velocity = direction.normalized * _speed;
        _characterController.Move(velocity * Time.deltaTime);
    }
}
