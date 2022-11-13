using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerCooldown), menuName = "Skill/Passive Skill/" + nameof(PlayerCooldown))]
public class PlayerCooldown : PassiveSkill
{
    [SerializeField] private float _upgradeValue = 0.5f;

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.Player.PlayerStats.UpgradeColldownMultiplier(_upgradeValue);
    }

    public override string GetCurrentLevelDecription() {
        var result = $"Ускоряет перезарядку на <color=#00ff00ff>{_upgradeValue * 100}% </color>";
        return result;
    }
}