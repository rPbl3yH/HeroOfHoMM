using UnityEngine;

class TargetBullet : MonoBehaviour
{
    private Transform _target;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed, _speedRotation;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _colliderPoint;
    bool _isPlague;

    PlagueSkillStats _plagueStats;
    float _damage;

    public void Setup(Transform transform, float damage) {
        _target = transform;
        _damage = damage;
    }

    public void Setup(bool isPlague, PlagueSkillStats plagueSkillStats) {
        _isPlague = isPlague;
        _plagueStats = plagueSkillStats;
    }

    private void Start() {
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate() {
        Vector3 toTarget = _target.position - transform.position;
        _rb.rotation = Quaternion.LookRotation(toTarget);
        _rb.velocity = transform.forward * _speed;
    }

    private void Die() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.TryGetComponent(out Enemy enemy)) {
            if (enemy.TryGetComponent(out IDamageable damageable)) {
                damageable.TakeDamage(_damage);
                print("Plague " + _isPlague);
                if (_isPlague) {
                    Collider[] enemiesAround = Physics.OverlapSphere(_colliderPoint.position, _plagueStats.Radius, _layerMask);
                    print("EnemiesAround " + enemiesAround.Length);
                    print("Radius " + _plagueStats.Radius);
                    foreach (var enemyCollider in enemiesAround) {
                        enemyCollider.GetComponent<Enemy>().TakePlagueDamage(_plagueStats);
                    }
                }

                Die();
            }
        }
    }

   
}