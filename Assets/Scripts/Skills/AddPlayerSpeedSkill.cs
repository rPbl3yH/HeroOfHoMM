using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Passive Skill/AddPlayerSpeedSkill")]
public class AddPlayerSpeedSkill : PassiveSkill
{
    [SerializeField] private float _percentUpgrade;

    public override void Create(SkillsManager skillsManager) {
        base.Create(skillsManager);
        skillsManager.AddSpeed(_percentUpgrade);
    }
}
