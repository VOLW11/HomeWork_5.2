using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] Character _characterPrefab;
    [SerializeField] private int _health;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private MouseControl _mouseControl;

    private void Awake()
    {
        _mouseControl = new MouseControl(_inputHandler);

        Character characterCopy = Instantiate(_characterPrefab, transform.position, Quaternion.identity, null);
        _virtualCamera.Follow = characterCopy.transform;
        characterCopy.Initialize(new Health(_health), _mouseControl);
    }
}
