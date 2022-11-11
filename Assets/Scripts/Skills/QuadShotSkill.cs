using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill/QuadShot")]
public class QuadShotSkill : ActiveSkill
{
    [Space(10)]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _timeToSpawnBullet;
    [SerializeField] private float _damage = 15f;
    float _timer;

    public override void UpdateCall(SkillsManager skillsManager, float frameTime) {
        base.UpdateCall(skillsManager, frameTime);
        _timer += frameTime;

        if (_timer > _timeToSpawnBullet) {
            _timer = 0;
            SpawnBullets(skillsManager.PlayerTransform);
        }
    }

    private void SpawnBullets(Transform playerTransform) {

        Vector3[] bulletDirection = { playerTransform.right, -playerTransform.right, playerTransform.forward, -playerTransform.forward };
        foreach (var direction in bulletDirection) {
            Bullet newBullet = Instantiate(_bulletPrefab, playerTransform.position + Vector3.up , Quaternion.identity);
            newBullet.Setup(_damage, direction);
        }
    }
}
