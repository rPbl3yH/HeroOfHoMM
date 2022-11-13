using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill/Mine")]
public class MineSkill : ActiveSkill
{
    [SerializeField] private Mine _minePrefab;
    float _radius, _damage;

    protected override void Produce() {
        base.Produce();
        var pos = GameManager.Instance.Player.transform.position;
        Mine mine = Instantiate(_minePrefab, pos, Quaternion.identity);
        mine.Setup(_damage, _radius);
    }

    public override void SetLevel() {
        base.SetLevel();
        _radius = GetCurrentFeaturesValue(Feature.Radius);
        _damage = GetCurrentFeaturesValue(Feature.Damage);
    }
}
