using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopIconCardsManager : MonoBehaviour
{
    [SerializeField] private TopIconCard[] _activeIconSkills;
    [SerializeField] private TopIconCard[] _passiveIconSkills;

    private int _countActiveSkill;
    private int _countPassiveSkill;

    public void AddIconSkill(Skill skill) {
        if(skill is ActiveSkill) {
            _activeIconSkills[_countActiveSkill].Setup((ActiveSkill)skill);
            _countActiveSkill++;
        }
        else if (skill is PassiveSkill) {
            _passiveIconSkills[_countPassiveSkill].Setup((PassiveSkill)skill);
            _countPassiveSkill++;
        }
    }
}
