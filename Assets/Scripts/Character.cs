using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpDuration;

    private CharacterView _view;
    private NavMeshAgent _agent;

    private Health _health;
    private Mover _mover;

    private CinemachineVirtualCamera _virtualCamera;

    private int _maxHealth;
    private float stoppingDistance = 0.05f;

    private Coroutine _jumpCoroutine;

    private MouseControl _mouseControl;

    public void Initialize(Health health, MouseControl mouseControl)
    {
        _mouseControl = mouseControl;
        _mouseControl.HitInfoPoint += MoveToTarget;
        _mouseControl.HitInfoPathError += PathError;
        _health = health;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _view = GetComponentInChildren<CharacterView>();

        _mover = new Mover(_agent);

        _maxHealth = _health.Value;
    }

    private void OnDestroy()
    {
        _mouseControl.HitInfoPoint -= MoveToTarget;
        _mouseControl.HitInfoPathError -= PathError;
    }

    private void Update()
    {
        if (_agent == null)
            return;

        if ((_agent.destination - _agent.transform.position).magnitude <= stoppingDistance)
        {
            _agent.isStopped = true;
            _view.StopRunning();
        }

        if (_agent.isOnOffMeshLink)
        {
            if (_jumpCoroutine == null)
            {
                _jumpCoroutine = StartCoroutine(Jump(_jumpDuration));
            }

            return;
        }

        if (_health.Value <= (int)(_maxHealth * 0.3f))
            _view.InjuredEnable();

        if (_health.Value == 0)
        {
            _view.Death();
            _agent.isStopped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        _view.TakeDamage();
        _health.Reduce(damage);
    }

    private void MoveToTarget(Vector3 hitInfoPoint)
    {
        _view.TargetEffect(hitInfoPoint);

        _mover.MoveToClick(hitInfoPoint);

        _agent.isStopped = false;
        _view.StartRunning();
    }

    private void PathError(Vector3 hitInfoPoint)
    {
        _view.TargetErrorEffect(hitInfoPoint);
    }

    private IEnumerator Jump(float duration)
    {
        _view.StartJumping();

        OffMeshLinkData data = _agent.currentOffMeshLinkData;
        Vector3 startPos = _agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * _agent.baseOffset;

        float progress = 0;

        while (progress < duration)
        {
            float yOffset = _jumpCurve.Evaluate(progress / duration);
            _agent.transform.position = Vector3.Lerp(startPos, endPos, progress / duration) + yOffset * Vector3.up;
            transform.rotation = Quaternion.LookRotation(endPos - startPos);
            progress += Time.deltaTime;
            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _view.StopJumping();
        _jumpCoroutine = null;
    }
}
