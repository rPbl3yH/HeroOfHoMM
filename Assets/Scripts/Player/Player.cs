using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HitText _hitTextPrefab;

    [field: SerializeField] public PlayerAttack PlayerAttack { get; private set; }
    [field: SerializeField] public PlayerMove PlayerMove { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    public PlayerStats PlayerStats { get; private set; }

    public ParticleSystem ShieldEffect;
    [SerializeField] private GameObject _visual;

    float _timer;

    public void TakeDamage(float damage) {
        

        PlayerStats.TakeDamage(damage);
    }

    public void CreateHitText(float damage) {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        hit.Setup(damage, false);
    }

    public void CreateRegenText() {
        HitText hit = Instantiate(_hitTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        hit.Setup(PlayerStats.RegenerationHpValue, true);
    }

    private void Awake() {
        RestartGame();
    }

    private void Start() {
        GameManager.Instance.EventManager.OnLoseGame += OnLoseGame;
    }

    private void OnLoseGame() {
        _visual.SetActive(false);
    }

    private void RestartGame() {
        PlayerStats = new PlayerStats();
    }

    public void TakeExperience(float value) {
        PlayerStats.TakeExperience(value);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) LevelUp();

        _timer += Time.deltaTime;
        var cooldown = AddPlayerRegenration.CooldownRegeneration;
        if(_timer > cooldown) {
            _timer = 0f;
            if (PlayerStats.RegenerationHpValue > 0) {
                CreateRegenText();
                PlayerStats.Regenerate();
            }
        }
    }

    public void LevelUp() {
        PlayerStats.LevelUp();
    }
}