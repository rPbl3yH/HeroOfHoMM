using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill/Plague")]
public class PlagueSkill : ActiveSkill
{

    public override void Activate() {
        base.Activate();
        var radius = FeaturesLevel[Level - 1].GetValueFeature(Feature.Radius);
        var dps = FeaturesLevel[Level - 1].GetValueFeature(Feature.DPS);
        var duration = FeaturesLevel[Level - 1].GetValueFeature(Feature.Duration);

        PlagueSkillStats skillStats = new PlagueSkillStats(radius, dps, duration);

        GameManager.Instance.Player.PlayerAttack.ActivatePlague(skillStats);
    }
}

public class PlagueSkillStats
{
    public float Radius;
    public float Duration;
    public float DPS;

    public PlagueSkillStats( float radius, float dps, float duration) {
        Radius = radius;
        DPS = dps;
        Duration = duration;
    }
}