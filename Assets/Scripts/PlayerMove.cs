using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    private IInputService _inputService;
    private Camera _camera;
    private Vector3 _moveDireciton;

    private void Start() {
        _inputService = GameManager.InputService;
        _camera = Camera.main;
    }

    private void Update() {
        _moveDireciton = Vector3.zero;
        if (_inputService.Axis.sqrMagnitude <= 0) {
            return;
        }
        _moveDireciton = _camera.transform.TransformDirection(_inputService.Axis);
        _moveDireciton.y = 0f;
        _moveDireciton.Normalize();
        //transform.rotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
    }

    private void FixedUpdate() {
        _rb.velocity = _moveDireciton * _speed;
        if(_moveDireciton.sqrMagnitude > Mathf.Epsilon) {
            _rb.rotation = Quaternion.LookRotation(_moveDireciton, Vector3.up);
        }
    }
}