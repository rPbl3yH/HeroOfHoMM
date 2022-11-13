using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddPickUpExperience), menuName = "Skill/Passive Skill/" + nameof(AddPickUpExperience))]
public class AddPickUpExperience : PassiveSkill
{
    [SerializeField] private float _upgradeValue = 0.02f;

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.Player.PlayerStats.UpgradeExperienceMultiplier(_upgradeValue);
    }

    public override string GetCurrentLevelDecription() {
        var result = $"Получаемый опыт увеличен на <color=#00ff00ff>{_upgradeValue * 100}% </color>";
        return result;
    }
}