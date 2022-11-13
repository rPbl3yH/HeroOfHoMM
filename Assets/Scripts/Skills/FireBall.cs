using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireBall :MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private LayerMask _layerMask;
    private float _damage;
    private float _radius;
    private Transform _target;

    public void Setup(FireBallStats ballStats) {
        _damage = ballStats.Damage;
        _radius = ballStats.Radius;
        _target = ballStats.Target;
    }

    private void Update() {
        if (_target == null) Destroy(gameObject);
    }

    private void FixedUpdate() {
        Vector3 toTarget = _target.position - transform.position;
        _rb.rotation = Quaternion.LookRotation(toTarget);
        _rb.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody) {
            if(other.attachedRigidbody.TryGetComponent(out Enemy enemy)){
                Explosion();
            }
        }    
    }

    public void Explosion() {
        Collider[] aroundEnemyColliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        foreach (var collider in aroundEnemyColliders) {
            if (collider.attachedRigidbody.TryGetComponent(out Enemy enemy)) {
                enemy.TakeDamage(_damage);
            }
        }
    }
}

public class FireBallStats 
{
    public float Radius;
    public float Damage;
    public Transform Target;

    public FireBallStats(float radius, float damage, Transform target) {
        Radius = radius;
        Damage = damage;
        Target = target;
    }
}