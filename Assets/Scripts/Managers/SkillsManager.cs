using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillsManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Skill[] _allSkill;
    [SerializeField] private List<ActiveSkill> _activeSkills = new List<ActiveSkill>();
    [SerializeField] private List<PassiveSkill> _passiveSkills = new List<PassiveSkill>();

    [SerializeField] private List<ActiveSkill> _appliedActiveSkills = new List<ActiveSkill>();
    [SerializeField] private List<PassiveSkill> _appliedPassiveSkills = new List<PassiveSkill>();

    public Transform PlayerTransform { get; private set; }

    private void Start() {
        PlayerTransform = _player.transform;
        InitializePassiveAndActiveSkills();
    }

    private void InitializePassiveAndActiveSkills() {
        for (int i = 0; i < _allSkill.Length; i++) {
            _allSkill[i] = Instantiate(_allSkill[i]);
            //_allSkill[i].Initialize();
            if (_allSkill[i] is ActiveSkill) {
                ActiveSkill activeSkill = _allSkill[i] as ActiveSkill;
                _activeSkills.Add(activeSkill);
            }
            else {
                PassiveSkill passiveSkill = _allSkill[i] as PassiveSkill;
                _passiveSkills.Add(passiveSkill);
            }
        }
    }

    private void Update() {
        if (!GameManager.Instance.IsPlaying) return;
        foreach (var skill in _appliedActiveSkills) {
            skill.UpdateCall(this, Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            ShowCards();
        }
    }

    [ContextMenu("Show Cards")]
    public void ShowCards() {
        List<Skill> skillsToShow = new List<Skill>();

        foreach (var p_skill in _appliedPassiveSkills) {
            if (p_skill.Level < 10) {
                skillsToShow.Add(p_skill);
            }
        }

        foreach (var a_skill in _appliedActiveSkills) {
            if (a_skill.Level < 10) {
                skillsToShow.Add(a_skill);
            }
        }

        if (_appliedActiveSkills.Count < 4) {
            skillsToShow.AddRange(_activeSkills);
        }

        if (_appliedPassiveSkills.Count < 4) {
            skillsToShow.AddRange(_passiveSkills);
        }

        int minCount = Math.Min(3, skillsToShow.Count);
        int[] randomNumbers = RandomArrayInt(skillsToShow, minCount);

        List<Skill> skillsForCards = new List<Skill>();
        for (int i = 0; i < randomNumbers.Length; i++) {
            skillsForCards.Add(skillsToShow[randomNumbers[i]]);
        }

        if (skillsForCards.Count > 0) {
            GameManager.Instance.UIController.CardsManager.ShowCards(skillsForCards);
            Time.timeScale = 0f;
        }
        
    }

    private int[] RandomArrayInt(List<Skill> skillsToShow, int countInt) {
        List<int> numbersList = new List<int>();
        List<int> resultList = new List<int>();
        for (int i = 0; i < skillsToShow.Count; i++) {
            numbersList.Add(i);
        }

        for (int i = 0; i < countInt; i++) {
            var randomId = Random.Range(0, numbersList.Count - 1);
            resultList.Add(numbersList[randomId]);

            numbersList.Remove(numbersList[randomId]);
        }

        return resultList.ToArray();
    }

    public void AddSkill(Skill skill) {
        GameManager.Instance.UIController.CardsManager.HideCards();

        if (skill is ActiveSkill) {
            var activeSkill = (ActiveSkill)skill;
            if (!_appliedActiveSkills.Contains(activeSkill)) {
                _activeSkills.Remove(activeSkill);
                _appliedActiveSkills.Add(activeSkill);
                GameManager.Instance.UIController.TopIconManager.AddIconSkill(skill);
            }
        }

        if (skill is PassiveSkill) {
            var passiveSkill = (PassiveSkill)skill;
            if (!_appliedPassiveSkills.Contains(passiveSkill)) {
                _passiveSkills.Remove(passiveSkill);
                _appliedPassiveSkills.Add(passiveSkill);
                GameManager.Instance.UIController.TopIconManager.AddIconSkill(skill);
            }
        }

        skill.Activate();
        
        Time.timeScale = 1f;
    }

    public void AddSpeed(float percent) {
        _player.PlayerStats.UpgradeSpeed(percent);
    }
}