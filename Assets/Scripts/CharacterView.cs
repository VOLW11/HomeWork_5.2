using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");

    [SerializeField] private Animator _animator;

    public void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    public void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }
}
