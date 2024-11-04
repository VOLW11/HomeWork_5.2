using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamageable
{
    private CharacterView _view;
    private NavMeshAgent _agent;

    private Health _health;
    private Mover _mover;

    private CinemachineVirtualCamera _virtualCamera;

    private int _maxHealth;
    private int _leftMouseButton = 0;

    public void Initialize(Health health)
    {
        _health = health;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _view = GetComponentInChildren<CharacterView>();

        _mover = new Mover(_agent);

        _maxHealth = _health.Value;
    }

    private void Update()
    {
        if (_agent == null)
            return;

        if (_agent.destination == _agent.transform.position)
        {
            _agent.isStopped = true;
            _view.StopRunning();
        }

        if (_health.Value <= (int)(_maxHealth * 0.3f))
            _view.InjuredEnable();

        if (_health.Value == 0)
        {
            _view.Death();
            _agent.isStopped = true;
        }

        if (Input.GetMouseButtonDown(_leftMouseButton))
            MoveToTarget();
    }

    public void TakeDamage(int damage)
    {
        _view.TakeDamage();
        _health.Reduce(damage);
    }

    private void MoveToTarget()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            Ground ground = hitInfo.collider.GetComponent<Ground>();

            if (ground != null)
            {
                _view.TargetEffect(hitInfo.point);

                _mover.MoveToClick(hitInfo.point);

                _agent.isStopped = false;
                _view.StartRunning();
            }
            else
            {
                _view.TargetErrorEffect(hitInfo.point);
            }
        }
    }
}
