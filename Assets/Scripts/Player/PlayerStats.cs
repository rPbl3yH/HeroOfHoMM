using UnityEngine;

public class PlayerStats : IDamageable
{
    private const int CritValueUpgrading = 2;
    private float damage = 1;

    public PlayerStats() {
        Healths = MaxHealths;
        
    }

    public float MaxHealths { get; private set; } = 100;
    public float Healths { get; private set; }
    public float Damage { get => damage * DamageMultuplier; private set => damage = value; }
    public float DamageMultuplier { get; private set; } = 1;
    public float CritChance { get; private set; } = 5;
    public float Armor { get; private set; } = 0;
    public float Speed { get; private set; } = 5;
    public float ExperienceValue { get; private set; }
    public float NextLevelExperienceValue { get; private set; } = 50;
    public float DistanceToPullExp { get; private set; } = 2;
    public int Level { get; private set; } = 1;
    public float DelayForMainAttack { get; private set; } = 1.5f;

    public bool Invelnerable { get; private set; } = false;


    public bool GetCritChance() {
        int randomValue = Random.Range(1, 100);
        return randomValue <= CritChance;
    }

    public void UpgradeMaxHealth() {
        MaxHealths = UpgradeStat10Percent(MaxHealths);
    }

    public void UpgradeDamageMultiplier() {
        DamageMultuplier = UpgradeStat10Percent(DamageMultuplier);
    }

    public void UpgradeCritChance() {
        CritChance += CritValueUpgrading;
    }

    private float UpgradeStat10Percent(float value) {
        var result = value + value * 0.1f;
        return result;
    }

    public void UpgradeSpeed(float percent) {
        Speed *= (1 + percent/100f);
    }

    public void TakeDamage(float damage) {
        if (Invelnerable) return;
        Healths -= damage - GetAbsorbingDamage(damage);
        //Debug.Log($"Take Damage {damage}. HP: {Healths}");
        if (Healths <= 0) {
            Debug.Log("Player die");
            //Вызвать ивент смерти игрока
        }
    }

    private float GetAbsorbingDamage(float damage) {
        return damage * (Armor / 100);
    }

    public void TakeExperience(float expValue) {
        ExperienceValue += expValue;
        if(ExperienceValue >= NextLevelExperienceValue) {
            ExperienceValue -= NextLevelExperienceValue;
            NextLevelExperienceValue *= 1.1f;
            LevelUp();
            //Debug.Log("Next level exp value " + NextLevelExperienceValue);
            //Debug.Log("Exp value now " + ExperienceValue);
            //Debug.Log("Level " + Level);
        }
    }

    public void LevelUp() {
        
        Level++;
        GameManager.Instance.SkillsManager.ShowCards();
    }
}