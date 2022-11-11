using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HitText _hitTextPrefab;

    [field: SerializeField] public PlayerAttack PlayerAttack { get; private set; }
    [field: SerializeField] public PlayerMove PlayerMove { get; private set; }

    public PlayerStats PlayerStats { get; private set; }

    public void TakeDamage(float damage) {
        CreateHitText(damage);

        PlayerStats.TakeDamage(damage);
    }

    private void CreateHitText(float damage) {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        hit.Setup(damage, false);
    }

    private void Awake() {
        RestartGame();
    }

    private void RestartGame() {
        PlayerStats = new PlayerStats();
    }

    public void TakeExperience(float value) {
        PlayerStats.TakeExperience(value);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) LevelUp();
    }

    public void LevelUp() {
        PlayerStats.LevelUp();
    }
}
