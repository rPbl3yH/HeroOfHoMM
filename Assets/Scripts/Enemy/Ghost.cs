using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Warrior
{
    
    int _currentCountMiss, _currentCountHit;

    public override void TakeDamage(float damage) {

        var randomPoint = Random.Range(0, 100f);
        if (randomPoint >= 50f) {
            if (_currentCountHit >= 2) {
                Miss();
                _currentCountHit = 0;
                _currentCountMiss++;
            }
            else {
                _currentCountHit++;
                base.TakeDamage(damage);
            }
        }
        else {
            if (_currentCountMiss >= 2) {
                base.TakeDamage(damage);
                _currentCountMiss = 0;
                _currentCountHit++;
            }
            else {
                Miss();
                _currentCountMiss++;
            }
        }
    }

    private void Miss() {
        print("miss");
        CreateHitText("Miss");
    }
}
