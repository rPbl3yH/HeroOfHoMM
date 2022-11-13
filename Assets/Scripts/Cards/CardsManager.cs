using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public Sprite ActiveSkillSprite;
    public Sprite PassiveSkillSprite;

    [SerializeField] private Card[] _cards;
    [SerializeField] private GameObject _canvasJoystic;



    private void Start() {
        foreach (var card in _cards) {
            card.Initialize();
        }
    }

    public void ShowCards(List<Skill> skills) {
        _canvasJoystic.SetActive(false);
        gameObject.SetActive(true);

        for (int i = 0; i < skills.Count; i++) {
            _cards[i].Setup(skills[i]);
        }

        for (int i = skills.Count; i < _cards.Length; i++) {
            _cards[i].Hide();
        }

        foreach (var item in skills) {
            //print("Name "  + item.Name + " DESC: " + item.Desctiprion);
        }
    }

    public void HideCards() {
        _canvasJoystic.SetActive(true);
        gameObject.SetActive(false);

    }
}