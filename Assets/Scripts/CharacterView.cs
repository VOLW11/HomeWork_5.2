using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private float _maxWeight = 1f;
    private ParticleSystem _prefabClickEffect;

    [SerializeField] private ParticleSystem _clickEffect;
    [SerializeField] private ParticleSystem _clickEffectErrorPath;
    [SerializeField] private Animator _animator;

    public void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    public void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    public void Death()
    {
        _animator.SetTrigger("Death");
    }

    public void InjuredEnable()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("InjuredLayer"), _maxWeight);
    }

    public void TargetEffect(Vector3 spawnPoint)
    {
        if (_prefabClickEffect != null)
            Destroy(_prefabClickEffect.gameObject);

        _prefabClickEffect = Instantiate(_clickEffect, spawnPoint, Quaternion.identity, null);
    }

    public void TargetErrorEffect(Vector3 spawnPoint)
    {
        Instantiate(_clickEffectErrorPath, spawnPoint, Quaternion.identity, null);
    }

    public void TakeDamage()
    {
        _animator.SetTrigger("TakeDamage");
    }
}
