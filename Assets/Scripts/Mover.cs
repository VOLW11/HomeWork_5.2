using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class Mover
{
    private NavMeshAgent _agent;
    private Vector3 _direction;

    public Mover(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void MoveToClick(Vector3 direction)
    {
        NavMeshPath pathToTarget = new NavMeshPath();

        if (GetPath(pathToTarget, direction) == false)
            return;

        _agent.SetDestination(direction);
    }

    private bool GetPath(NavMeshPath pathToTarget, Vector3 direction)
    {
        pathToTarget.ClearCorners();

        if (_agent.CalculatePath(direction, pathToTarget) && pathToTarget.status != NavMeshPathStatus.PathInvalid)
            return true;

        return false;
    }
}
