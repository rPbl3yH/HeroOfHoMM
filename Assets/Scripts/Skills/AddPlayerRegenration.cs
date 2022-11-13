using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddPlayerRegenration), menuName = "Skill/Passive Skill/" + nameof(AddPlayerRegenration))]
public class AddPlayerRegenration : PassiveSkill
{
    [SerializeField] private float _upgradeValue = 1f;
    [field: SerializeField] public static float CooldownRegeneration { get; private set; } = 4f;

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.Player.PlayerStats.UpgradeRegeration(_upgradeValue);
    }

    public override string GetCurrentLevelDecription() {
        var result = $"Регенерация HP <color=#00ff00ff>{_upgradeValue}% </color>";
        return result;
    }
}