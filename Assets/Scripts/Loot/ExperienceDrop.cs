using UnityEngine;

internal class ExperienceDrop : MonoBehaviour
{
    [SerializeField] private float _expirienceValue = 10f;
    [SerializeField] private float _speedRotation = 40f;
    [SerializeField] private Transform _body;
    private float _distanceToGo;

    private Transform _playerTransform;
    private Player _player;
    [SerializeField] private float _speedToPlayer;

    public void Setup(Transform playerTransform) {
        _playerTransform = playerTransform;
        if (playerTransform.TryGetComponent(out Player player)) {
            _distanceToGo = player.PlayerStats.DistanceToPullExp;
            _player = player;
        }
    }

    private void Update() {
        _body.Rotate(Vector3.up * _speedRotation);

        var distanceDropPlayer = Vector3.Distance(_playerTransform.position, transform.position);
        if (distanceDropPlayer < _distanceToGo) {
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, Time.deltaTime * _speedToPlayer);
        }
    }

    private void PickUp() {
        _player.TakeExperience(_expirienceValue);
        //print("Получили опыт " + _expirienceValue);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody) {
            if (other.attachedRigidbody.GetComponent<Player>()) {
                PickUp();
            }
        }
    }
}