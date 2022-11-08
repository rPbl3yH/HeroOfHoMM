using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedFollowing;

    private Vector3 _targetPoint;

    private void Update() {
        if (_target == null) return;

        _targetPoint = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.position = Vector3.Lerp(transform.position, _targetPoint, Time.deltaTime * _speedFollowing);
    }
}