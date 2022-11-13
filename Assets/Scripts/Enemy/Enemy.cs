using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IPlagueable
{
    private const string AttackAnim = "IsAttack";
    private float _timer;

    protected Transform PlayerTransform;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private ExperienceDrop _expPrefab;
    [SerializeField] private HitText _hitTextPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _critText = "Crit!";

    private EnemyManager _enemyManager;

    [Header("Stats")]
    [SerializeField] private float _speed;

    [SerializeField] private float _speedRotation;
    [SerializeField] private float _damage = 3f;
    [SerializeField] private float _timeToAttack = 1f;
    [SerializeField] private float _dealyAttackAnim = 0.3f;

    [field: SerializeField] public float Healths { get; private set; } = 10f;
    public GameObject PlgueEffectPrefab { get; set; }

    private bool _isPlague, _isPlayerInCollision;

    public virtual void Setup(Transform transformPlayer, EnemyManager enemyManager) {
        PlayerTransform = transformPlayer;
        _timer = _timeToAttack;
        _enemyManager = enemyManager;
    }

    private void Update() {
        _timer += Time.deltaTime;

        Vector3 toPlayer = PlayerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
    }

    private void FixedUpdate() {
        _rb.velocity = transform.forward * _speed;
    }

    private void Die() {
        if (_expPrefab != null) {
            ExperienceDrop experienceDrop = Instantiate(_expPrefab, transform.position, Quaternion.identity);
            experienceDrop.Setup(PlayerTransform);
        }
        _enemyManager.CallDie(this);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage) {
        Healths -= damage;
        CreateHitText(damage);
        if (Healths <= 0) {
            Die();
        }
    }

    protected void CreateHitText(float damage) {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        hit.Setup(damage, false);
    }

    protected void CreateHitText(string text) {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        hit.Setup(text);
    }

    protected void CreateCritText(string text) {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up * 2.5f, Quaternion.identity);
        hit.Setup(text);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.rigidbody.TryGetComponent(out Player player)) {
            if (_timer > _timeToAttack) {
                _timer = 0;
                Attack(player);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.TryGetComponent(out Player player)) {
            _isPlayerInCollision = true;
            if (_timer > _timeToAttack) {
                _timer = 0;
                Attack(player);
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.rigidbody.TryGetComponent(out Player player)) {
            _isPlayerInCollision = false;
        }
    }

    private void Attack(Player player) {
        StartCoroutine(DoAttack(player));

        _animator.SetBool(AttackAnim, true);
    }

    private IEnumerator DoAttack(IDamageable damageable) {
        yield return new WaitForSeconds(_dealyAttackAnim);
        StartCoroutine(StopAnimAttack());
        if (!_isPlayerInCollision) {
            yield break;
        }

        damageable.TakeDamage(_damage);
    }

    private IEnumerator StopAnimAttack() {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool(AttackAnim, false);
    }

    public void TakePlagueDamage(PlagueSkillStats plagueSkillStats) {
        if (!_isPlague)
            StartCoroutine(StartDamagePlague(plagueSkillStats));
        _isPlague = true;
    }

    private IEnumerator StartDamagePlague(PlagueSkillStats plagueSkillStats) {
        float _timer = 0;
        while (_timer < plagueSkillStats.Duration) {
            TakeDamage(plagueSkillStats.DPS);
            yield return new WaitForSeconds(1f);
            _timer += 1f;
        }
        _isPlague = false;
    }

    public void CreatePlagueEffect() {
        //throw new System.NotImplementedException();
    }

    public void CreateCritText() {
        CreateCritText(_critText);
    }
}