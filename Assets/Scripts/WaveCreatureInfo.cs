using System;
using UnityEngine;

[Serializable]
public class WaveCreatureInfo 
{
    public Enemy Enemy;
    public float StartCooldown;
    public float Cooldown;
    public int Count;

    private float _timer;
    private bool _isStartDelay;
    private int _currentCount;

    public void Init() {
        _isStartDelay = false;
        _timer = 0;
        _currentCount = 0;
    }

    public void UpdateCall(float frameTime) {
        _timer += frameTime;
        if(_timer > StartCooldown && !_isStartDelay) {
            _isStartDelay = true;
            _timer = 0;
        }
        if (!_isStartDelay) return;

        if (_timer > Cooldown) {
            _timer = 0;
            if(_currentCount < Count) {
                GameManager.Instance.EnemyManager.Spawn(Enemy);
                _currentCount++;
            }
            
        }
    }
}
