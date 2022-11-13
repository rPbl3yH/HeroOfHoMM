using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyKilledText;
    [SerializeField] private TMP_Text _levelText;

    [SerializeField] private string _prevEnemyKill;
    [SerializeField] private string _prevLevel;

    public void Setup(int enemyKilled, int level) {
        _enemyKilledText.text = $"{_prevEnemyKill}\n " +
            $"{enemyKilled}";
        _levelText.text = $"{_prevLevel}\n " +
            $"{level}";
    }
}
