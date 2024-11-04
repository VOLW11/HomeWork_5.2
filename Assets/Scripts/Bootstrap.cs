using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] Character _characterPrefab;
    [SerializeField] private int _health;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        Character character = Instantiate(_characterPrefab, transform.position, Quaternion.identity, null);
        character.Initialize(new Health(_health));
        _virtualCamera.Follow = character.transform;
    }
}
