using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private TargetBullet _bulletPrefab;

    private PlagueSkillStats _plagueSkillStats;

    private bool _isPlague;
    private float _timer;

    private void Update() {
        if (!GameManager.Instance.IsPlaying) return;
        _timer += Time.deltaTime;
        var playerStats = GameManager.Instance.Player.PlayerStats;
        var cooldown = playerStats.DelayForMainAttack * playerStats.ColldownMultiplier;
        if (_timer > cooldown) {
            _timer = 0;
            Shot();
        }
    }

    private void Shot() {
        var targetEnemies = GameManager.Instance.EnemyManager.GetNearestEnemyTransform(transform.position, 1);
        if (targetEnemies.Length == 0) return; 
        TargetBullet newBullet = Instantiate(_bulletPrefab, _spawnBulletPoint.position, Quaternion.identity);
        newBullet.Setup(targetEnemies[0].transform, GameManager.Instance.Player.PlayerStats.Damage);
        if (_isPlague) {
            newBullet.Setup(_isPlague, _plagueSkillStats);
        }
    }

    public void ActivatePlague(PlagueSkillStats plagueSkillStats) {
        _isPlague = true;
        _plagueSkillStats = plagueSkillStats;
    }
}