using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddCritChance), menuName = "Skill/Passive Skill/" + nameof(AddCritChance))]
public class AddCritChance : PassiveSkill
{
    [SerializeField] private float _upgradeValue = 5f;

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.Player.PlayerStats.UpgradeCritChance(_upgradeValue);
    }

    public override string GetCurrentLevelDecription() {
        var result = $"вероятность крита +<color=#00ff00ff>{_upgradeValue}% </color>";
        return result;
    }
}