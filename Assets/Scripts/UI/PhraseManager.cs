using System.Collections.Generic;
using UnityEngine;

public class PhraseManager : MonoBehaviour
{
    [SerializeField] private List<string> _phrases = new List<string>();
    [SerializeField] private HitText _phrasePrefab;
    [SerializeField] private Transform _spawnPoint;

    private List<string> _currentPhrasesList = new List<string>();

    private void Start() {
        GameManager.Instance.EventManager.OnChoseSkillCard += OnLevelUp;
        foreach (var item in _phrases) {
            _currentPhrasesList.Add(item);
        }
    }

    private void OnLevelUp() {
        HitText newPhrase = Instantiate(_phrasePrefab, _spawnPoint.position, Quaternion.identity);
        if (_currentPhrasesList.Count == 0) return;
        var randomNum = Random.Range(0, _currentPhrasesList.Count);

        newPhrase.Setup(_currentPhrasesList[randomNum]);
        _currentPhrasesList.RemoveAt(randomNum);
    }
}