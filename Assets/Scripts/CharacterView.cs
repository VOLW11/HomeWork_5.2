using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private float _maxWeight = 1f;

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

    public void TakeDamage()
    {
        _animator.SetTrigger("TakeDamage");
    }
}
