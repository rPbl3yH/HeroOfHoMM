using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    float _damage;

    private void Start() {
        Destroy(gameObject, 3f);
    }

    public void Setup(float damage, Vector3 velocity) {
        _damage = damage;
        _rb.velocity = velocity * _speed;
    }

    private void Die() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.rigidbody.TryGetComponent(out Enemy enemy)) {
            if(enemy.TryGetComponent(out IDamageable damageable)) {
                damageable.TakeDamage(_damage);
                Die();
            }
        }
    }
}
