using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill/Fireball")]
public class FireballSkill : ActiveSkill
{
    [SerializeField] private FireBall _fireballPrefab;
    protected override void Produce() {
        base.Produce();
        var playerTransform = GameManager.Instance.Player.transform;
        var targetEnemy = GameManager.Instance.EnemyManager.GetNearestEnemyTransform(playerTransform.position, 1);
        if (targetEnemy.Length == 0) return;

        FireBall fireball = Instantiate(_fireballPrefab, playerTransform.position, Quaternion.identity);

        var radius = GetCurrentFeaturesValue(Feature.Radius);
        var damage = GetCurrentFeaturesValue(Feature.Damage);
        
       
        FireBallStats fireBallStats = new FireBallStats(radius, damage, targetEnemy[0].transform);
        fireball.Setup(fireBallStats);
    }
}
