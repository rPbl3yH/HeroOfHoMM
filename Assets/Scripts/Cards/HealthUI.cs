using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _imageHealth;
    [SerializeField] private Image _imageExperience;
    [SerializeField] private TMP_Text _levelText;

    void Start()
    {
        _player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthUI();
        SetExpUI();
    }

    public void SetHealthUI() {
        var diffirenceMultiplier = _player.PlayerStats.Healths / _player.PlayerStats.MaxHealths;
       
        _imageHealth.fillAmount = diffirenceMultiplier;
    }

    public void SetExpUI() {
        var diffirenceMultiplier = _player.PlayerStats.ExperienceValue / _player.PlayerStats.NextLevelExperienceValue;
        _imageExperience.fillAmount = diffirenceMultiplier;
        _levelText.text = _player.PlayerStats.Level.ToString();
    }
}
