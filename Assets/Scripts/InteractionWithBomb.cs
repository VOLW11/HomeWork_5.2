using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithBomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeExplosion;

    private float _time;

    private void OnTriggerStay(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _time += Time.deltaTime;

            Debug.Log(_time);

            if (_time >= _timeExplosion)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity, null);
                damageable.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
            _time = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4.5f);
    }

}
