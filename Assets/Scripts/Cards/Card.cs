using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Button _button;

    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _skillImage;

    [SerializeField] private Skill _skill;
    private UnityEvent _event;
    public void Initialize() {
        
        _button.onClick.AddListener(OnClick);
    }

    public void Setup(Skill skill) {
        _skill = skill;
        _titleText.text = skill.Name;

        _levelText.text = "LVL " + skill.Level;
        _descriptionText.text = skill.Desctiprion;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void OnClick() {
        GameManager.Instance.SkillsManager.AddSkill(_skill);
    }

}
