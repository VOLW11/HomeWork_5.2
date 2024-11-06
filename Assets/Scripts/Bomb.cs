using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private BombView _view;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeExplosion;

    [SerializeField] private AudioManager _audioManager;

    private float _time;
    private bool _isActive;

    private void Update()
    {
        if (_isActive == false)
            return;

        _time += Time.deltaTime;

        if (_time >= _timeExplosion)
        {
            Bang(_view);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _isActive = true;
        }
    }

    private void Bang(BombView view)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 4.5f);

        foreach (Collider target in targets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.TakeDamage(_damage);
        }

        view.Explosion();
        
       // Explosion?.Invoke();
       _audioManager.OnBombExplosion();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4.5f);
    }
}
