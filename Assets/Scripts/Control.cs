using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private int _leftMouseButton = 0;
    private Camera _camera;
    [SerializeField] LayerMask _mask;
    [SerializeField] private float _speed;
    [SerializeField] CharacterController _controller;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton) == false)
            return;

        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

        Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(cameraRay + " ScreenPointToRay");
        Debug.Log(worldPosition + " ScreenToWorldPoint");

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, Mathf.Infinity, _mask.value))
        {           
            Transform ground = hitInfo.collider.GetComponent<Transform>();

            Debug.Log(hitInfo.point + " hit");

            if (ground != null)
            {
                Debug.DrawRay(cameraRay.origin, cameraRay.direction * 100, Color.green);

                Debug.Log(hitInfo.point + " hit");
              
                MoveTo(hitInfo.point);
            }
        }
    }

    public void MoveTo(Vector3 direction)
    {
        Vector3 pos = direction - _controller.transform.position;

        Vector3 velocity = pos * _speed;

        _controller.Move(pos * Time.deltaTime);
    }
}
