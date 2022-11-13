using UnityEngine;

public class PlayerStats : IDamageable
{
    private float damage = 10;
    private float speed = 5;
    private float expValue;

    public PlayerStats() {
        Healths = MaxHealths;
    }

    public float MaxHealths { get; private set; } = 100;
    public float MaxHealthsMultiplier { get; private set; } = 1.05f;
    public float Healths { get; private set; }
    public float Damage { get => damage * DamageMultuplier; private set => damage = value; }
    public float CritChance { get; private set; } = 5;
    public float CritMultiplier { get; private set; } = 1.5f;
    public float Armor { get; private set; } = 0;
    public float Speed { get => speed; private set => speed = value * SpeedMultipler; }
    public float ExperienceValue { get => expValue; private set => expValue = value * ExperienceMultiplier; }
    public float NextLevelExperienceValue { get; private set; } = 50;
    public float DistanceToPullExp { get; private set; } = 2;
    public int Level { get; private set; } = 1;
    public float DelayForMainAttack { get; private set; } = 1.5f;

    public bool Invelnerable { get; private set; } = false;

    public float ExperienceMultiplier { get; private set; } = 1;

    public float SpeedMultipler { get; private set; } = 1;

    public float DamageMultuplier { get; private set; } = 1;

    public float ColldownMultiplier { get; private set; } = 1;

    public float RegenerationHpValue { get; private set; } = 0;

    public bool GetCritChance() {
        int randomValue = Random.Range(1, 100);
        return randomValue <= CritChance;
    }

    public void UpgradeMaxHealth() {
        MaxHealths *= MaxHealthsMultiplier;
        Healths *= MaxHealthsMultiplier;
    }

    public void TakeDamage(float damage) {
        if (Invelnerable) return;
        var damageValue = damage - GetAbsorbingDamage(damage);
        Healths -= damageValue;
        GameManager.Instance.Player.CreateHitText(damageValue);
        if (Healths <= 0) {
            GameManager.Instance.GameOver();
        }
    }

    private float GetAbsorbingDamage(float damage) {
        return damage * (Armor / 100);
    }

    public void TakeExperience(float expValue) {
        ExperienceValue += expValue;
        if (ExperienceValue >= NextLevelExperienceValue) {
            ExperienceValue -= NextLevelExperienceValue;
            NextLevelExperienceValue *= 1.5f;
            LevelUp();
        }
    }

    public void LevelUp() {
        Level++;
        UpgradeMaxHealth();
        GameManager.Instance.SkillsManager.ShowCards();
    }

    public void UpgradeDamageMultiplier(float value) {
        DamageMultuplier += value;
    }

    public void UpgradeExperienceMultiplier(float value) {
        ExperienceMultiplier += value;
    }

    public void UpgradeColldownMultiplier(float value) {
        ColldownMultiplier -= value;
    }

    public void UpgradeRegeration(float value) {
        RegenerationHpValue += value;
    }

    public void UpgradeCritChance(float value) {
        CritChance += value;
    }

    public void UpgradeSpeed(float value) {
        SpeedMultipler += value;
    }

    public void UpgradeArmorPlayer(float value) {
        Armor += value;
    }

    public void Regenerate() {
        Healths += RegenerationHpValue;
        Healths = Mathf.Clamp(Healths, 0, MaxHealths);
    }

    public float GetCritDamage() {
        return Damage * CritMultiplier;
    }

    public void SetInverulable(bool value) {
        Invelnerable = value;
    }
}