using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterView _view;
    [SerializeField] private ParticleSystem _clickEffect;

    private int _leftMouseButton = 0;
    private Camera _camera;
    private NavMeshAgent _agent;

    private bool _isAgro;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;

        
    }

    private void Update()
    {
        NavMeshPath pathToTarget = new NavMeshPath();

        if (Input.GetMouseButtonDown(_leftMouseButton) == false)
            return;

        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            Ground ground = hitInfo.collider.GetComponent<Ground>();

            if (ground != null)
            {
                //Instantiate(_clickEffect, hitInfo.point, Quaternion.identity, null);

                Move(hitInfo.point, pathToTarget);
            }
        }
    }


    public void Move(Vector3 direction, NavMeshPath pathToTarget)
    {
        if (GetPath(pathToTarget, direction))
        {
            Debug.Log(_agent.pathPending);

            if (_agent.pathPending)
            {
                if (_isAgro == false)
                {
                    _agent.isStopped = false;
                    _view.StartRunning();
                }

                _isAgro = true;
                _agent.SetDestination(direction);
            }
            else
            {
                if (_isAgro)
                {
                    _agent.isStopped = true;
                    _view.StopRunning();
                }

                _isAgro = false;
            }
        }
    }

    private bool GetPath(NavMeshPath pathToTarget, Vector3 direction)
    {
        pathToTarget.ClearCorners();

        if (_agent.CalculatePath(direction, pathToTarget) && pathToTarget.status != NavMeshPathStatus.PathInvalid)
            return true;

        return false;
    }
}
