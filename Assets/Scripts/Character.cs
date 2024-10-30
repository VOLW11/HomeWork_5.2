using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private Controller _controller;
    [SerializeField] private CharacterView _view;
    [SerializeField] private int _health;

    private Camera _camera;
    private NavMeshAgent _agent;
    private int _maxHealth;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
        _maxHealth = _health;
    }

    private void Update()
    {
        _controller.Initialize(_camera, _agent);

        if (_health <= (int)(_maxHealth * 0.3f))
            _view.InjuredEnable();

        if (_health <= 0)
        {
            _health = 0;
            _view.Death();
            _controller.enabled = false;
            _agent.isStopped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        _view.TakeDamage();
        _health -= damage;
    }
}
