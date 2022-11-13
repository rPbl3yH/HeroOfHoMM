using System;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField] private LayerMask _layerMask;
    float _damage, _radius;

    public void Setup(float damage, float radius) {
        _damage = damage;
        _radius = radius;   
    }

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody) {
            if (other.attachedRigidbody.GetComponent<Enemy>()) {
                Collider[] enemyAround = Physics.OverlapSphere(transform.position, _radius, _layerMask);
                foreach (var enemyCol in enemyAround) {
                    if (enemyCol.attachedRigidbody) {
                        enemyCol.attachedRigidbody.GetComponent<Enemy>().TakeDamage(_damage);

                    }
                }
                Die();
            }
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}