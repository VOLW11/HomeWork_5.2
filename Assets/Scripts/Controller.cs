using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    [SerializeField] private CharacterView _view;
    [SerializeField] private ParticleSystem _clickEffect;
    private ParticleSystem _prefabClickEffect;
    [SerializeField] private ParticleSystem _clickEffectErrorPath;

    private Camera _camera;
    private NavMeshAgent _agent;
    private int _leftMouseButton = 0;
    private RaycastHit _hitInfo;

    public void Initialize(Camera camera, NavMeshAgent agent)
    {
        _camera = camera;
        _agent = agent;
    }

    void Update()
    {
        if (_agent == null)
            return;

        if (_agent.destination == _agent.transform.position)
        {
            _agent.isStopped = true;
            _view.StopRunning();
        }

        if (Input.GetMouseButtonDown(_leftMouseButton) == false)
            return;

        if (_prefabClickEffect != null)
            Destroy(_prefabClickEffect.gameObject);

        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            Ground ground = hitInfo.collider.GetComponent<Ground>();

            if (ground != null)
            {
                _prefabClickEffect = Instantiate(_clickEffect, hitInfo.point, Quaternion.identity, null);

                Mover _mover = new Mover(_agent, hitInfo.point);

                _mover.MoveToClick();

                _agent.isStopped = false;
                _view.StartRunning();
            }
            else
            {
                Instantiate(_clickEffectErrorPath, hitInfo.point, Quaternion.identity, null);
            }
        }
    }
}
