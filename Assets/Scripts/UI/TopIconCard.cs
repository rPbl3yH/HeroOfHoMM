using UnityEngine;
using UnityEngine.UI;

public class TopIconCard : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _downloadCard;
    [SerializeField] private Image _backgroundImage;

    public void Setup(PassiveSkill skill) {
        if (skill.Image != null) {
            _iconImage.color = new Color(_iconImage.color.r, _iconImage.color.g, _iconImage.color.b, 1f);
            _iconImage.sprite = skill.Image;
        }

        _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
        _backgroundImage.sprite = GameManager.Instance.UIController.CardsManager.PassiveSkillSprite;
    }

    public void Setup(ActiveSkill skill) {
        if (skill.Image != null) {
            _iconImage.color = new Color(_iconImage.color.r, _iconImage.color.g, _iconImage.color.b, 1f);
            _iconImage.sprite = skill.Image;
        }
        skill.SetTopIconCard(this);
        _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
        _backgroundImage.sprite = GameManager.Instance.UIController.CardsManager.ActiveSkillSprite;
    }

    public void SetSkillDownloadValue(float value) {
        _iconImage.fillAmount = value;
    }
}