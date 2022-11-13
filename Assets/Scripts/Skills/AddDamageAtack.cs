using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddDamageAtack), menuName = "Skill/Passive Skill/" + nameof(AddDamageAtack))]
public class AddDamageAtack : PassiveSkill
{
    [SerializeField] private float _upgradeValue = 0.5f;

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.Player.PlayerStats.UpgradeDamageMultiplier(_upgradeValue);
    }

    public override string GetCurrentLevelDecription() {
        var result = $"Атака Сани увеличивается на <color=#00ff00ff>{_upgradeValue * 100}% </color>";
        return result;
    }
}