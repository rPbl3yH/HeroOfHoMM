using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill/Sky Shield")]
public class SkyShieldSkill : ActiveSkill
{
    [SerializeField] private ParticleSystem _shieldEffectPrefab;

    protected override void Produce() {
        base.Produce();
        SetShield(true);
    }

    protected override void EndProduce() {
        base.EndProduce();
        SetShield(false);
    }

    private void SetShield(bool value) {
        GameManager.Instance.Player.PlayerStats.SetInverulable(value);
        var effect = GameManager.Instance.Player.ShieldEffect;
        if (effect) {
            effect.gameObject.SetActive(value);
            effect.Play();
        }
    }

}
