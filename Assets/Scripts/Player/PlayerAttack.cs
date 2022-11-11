using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private TargetBullet _bulletPrefab;
    [SerializeField] private float _attackValue;

    PlagueSkillStats _plagueSkillStats;

    private bool _isPlague;
    private float _timer;


    private void Update() {
        _timer += Time.deltaTime;
        if (_timer > GameManager.Instance.Player.PlayerStats.DelayForMainAttack) {
            _timer = 0;
            Shot();
        }
    }

    private void Shot() {
        var targetEnemy = GameManager.Instance.EnemyManager.GetNearestEnemyTransform(transform.position, 1)[0].transform;
        if (targetEnemy == null) return;
        TargetBullet newBullet = Instantiate(_bulletPrefab, _spawnBulletPoint.position, Quaternion.identity);
        newBullet.Setup(targetEnemy, _attackValue);
        if (_isPlague) {
            newBullet.Setup(_isPlague, _plagueSkillStats);
        }
    }

    public void ActivatePlague(PlagueSkillStats plagueSkillStats) {
        _isPlague = true;
        _plagueSkillStats = plagueSkillStats;
    }
}