using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AddPlayerSpeedSkill), menuName = "Skill/Passive Skill/" + nameof(AddPlayerSpeedSkill))]
public class AddPlayerSpeedSkill : PassiveSkill
{
    [SerializeField] private float _valueUpgrade;

    public override void Activate() {
        base.Activate();
        SetLevel();
    }

    public override string GetCurrentLevelDecription() {
        string result = $"Увеличивает скорость на <color=#00ff00ff>{_valueUpgrade * 100}%</color>\n";
        return result;
    }

    public override void SetLevel() {
        base.SetLevel();
        GameManager.Instance.SkillsManager.AddSpeed(_valueUpgrade);
    }
}
