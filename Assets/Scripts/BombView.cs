using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosion;

    public void Explosion()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity, null);
    }
}
