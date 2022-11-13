using System;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Action OnStartedGame;
    public Action OnLoseGame;
    public Action OnWinGame;
    public Action OnChoseSkillCard;
 

    public void StartedGame() {
        OnStartedGame?.Invoke();
    }

    public void LoseGame() {
        OnLoseGame?.Invoke();
    }

    public void ChoseSkillCard() {
        OnChoseSkillCard?.Invoke();
    }

    public void WinGame() {
        OnWinGame?.Invoke();
    }
}