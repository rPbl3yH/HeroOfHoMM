using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPlagueable
{
    private float _timer;
    private bool _playerInCollision;

    private Transform _playerTransform;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private ExperienceDrop _expPrefab;
    [SerializeField] private HitText _hitTextPrefab;
    
    private EnemyManager _enemyManager;

    [Header("Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _damage = 3f;
    [SerializeField] private float _timeToAttack = 1f;

    [field: SerializeField] public float Healths { get; private set; } = 10f;
    public GameObject PlgueEffectPrefab { get; set; }

    private bool _isPlague;

    public void Setup(Transform transformPlayer, EnemyManager enemyManager) {
        _playerTransform = transformPlayer;
        _timer = _timeToAttack;
        _enemyManager = enemyManager;
    }

    private void Update() {
        _timer += Time.deltaTime;

        Vector3 toPlayer = _playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
    }

    private void FixedUpdate() {
        _rb.velocity = transform.forward * _speed;
    }

    private void Die() {
        if(_expPrefab != null) {
            ExperienceDrop experienceDrop = Instantiate(_expPrefab, transform.position, Quaternion.identity);
            experienceDrop.Setup(_playerTransform);
        }
        _enemyManager.CallDie(this);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        Healths -= damage;
        CreateHitText(damage);
        if (Healths <= 0) {
            Die();
        }
    }

    private void CreateHitText(float damage) {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        hit.Setup(damage, false);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.rigidbody.TryGetComponent(out Player player)) {
            if (player.TryGetComponent(out IDamageable damageable)) {
                if (_timer > _timeToAttack) {
                    _timer = 0;
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.TryGetComponent(out Player player)) {
            if (player.TryGetComponent(out IDamageable damageable)) {
                if (_timer > _timeToAttack) {
                    _timer = 0;
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }

    public void TakePlagueDamage(PlagueSkillStats plagueSkillStats) {
        StartCoroutine(StartDamagePlague(plagueSkillStats));
    }

    IEnumerator StartDamagePlague(PlagueSkillStats plagueSkillStats) {
        float _timer = 0;
        while (_timer < plagueSkillStats.Duration) {
            TakeDamage(plagueSkillStats.DPS);
            yield return new WaitForSeconds(1f);
            _timer += 1f;
        }
    }

    public void CreatePlagueEffect() {
        //throw new System.NotImplementedException();
    }
}