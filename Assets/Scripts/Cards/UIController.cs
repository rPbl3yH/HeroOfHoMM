using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [field: SerializeField] public CardsManager CardsManager { get; private set; }
    [field: SerializeField] public HealthUI HealthUI { get; private set; }
    [field: SerializeField] public FinishMenu FinishMenu { get; private set; }
    [field: SerializeField] public TopIconCardsManager TopIconManager { get; private set; }

    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _joystickCanvas;
    [SerializeField] private GameObject _playerBar;

    private void Start() {
        CardsManager.gameObject.SetActive(false);
        _startMenu.SetActive(true);
        _joystickCanvas.SetActive(false);
        FinishMenu.gameObject.SetActive(false);
        GameManager.Instance.EventManager.OnStartedGame += OnStartedGame;
        GameManager.Instance.EventManager.OnLoseGame += OnLoseGame;
    }

    private void OnLoseGame() {
        _joystickCanvas.SetActive(false);
        _playerBar.SetActive(false);
        FinishMenu.gameObject.SetActive(true);
    }

    private void OnStartedGame() {
        _startMenu.SetActive(false);
        _joystickCanvas.SetActive(true);
        _playerBar.SetActive(true);
    }


}
