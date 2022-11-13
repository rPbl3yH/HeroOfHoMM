using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddArmor), menuName = "Skill/Passive Skill/" + nameof(AddArmor))]
public class AddArmor : PassiveSkill
{
    [SerializeField] private float _upgradeValue = 5f;

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.Player.PlayerStats.UpgradeArmorPlayer(_upgradeValue);
    }

    public override string GetCurrentLevelDecription() {
        var result = $"Уменьшает урон на <color=#00ff00ff>{_upgradeValue}% </color>";
        return result;
    }
}