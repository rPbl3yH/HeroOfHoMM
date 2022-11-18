using UnityEngine;

class TargetBullet : MonoBehaviour
{
    private Transform _target;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed, _speedRotation;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _colliderPoint;
    private bool _isPlague;

    private PlagueSkillStats _plagueStats;
    private float _damage;

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

    private void Update() {
        if (_target == null) {
            var enemies = GameManager.Instance.EnemyManager.GetNearestEnemyTransform(transform.position, 1);
            if (enemies.Length > 0) {
                _target = enemies[0].transform;
            }
            else {
                Destroy(gameObject);
            }
        }
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
    }

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody) {
            if (other.attachedRigidbody.TryGetComponent(out Enemy enemy)) {
                MakeDamage(enemy);
                MakePlagueAround();

                Die();
            }
        }
    }

    private void MakePlagueAround() {
        if (_isPlague) {
            Collider[] enemiesAround = Physics.OverlapSphere(_colliderPoint.position, _plagueStats.Radius, _layerMask);
            foreach (var enemyCollider in enemiesAround) {
                if (enemyCollider.attachedRigidbody) {
                    enemyCollider.attachedRigidbody.GetComponent<Enemy>().TakePlagueDamage(_plagueStats);
                }
            }
        }
    }

    private void MakeDamage(Enemy enemy) {
        var damage = _damage;
        if (GameManager.Instance.Player.PlayerStats.GetCritChance()) {
            damage = GameManager.Instance.Player.PlayerStats.GetCritDamage();
            enemy.CreateCritText();
        }
        enemy.TakeDamage(damage);
    }
}