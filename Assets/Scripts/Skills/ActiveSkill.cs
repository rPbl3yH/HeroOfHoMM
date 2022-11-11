using System;
using System.Linq;

public class ActiveSkill : Skill
{
    public Feature AllowedFeatures;

    public LevelFeatures[] FeaturesLevel { get; private set; } = CreateAttributeArray();
    public LevelFeatures[] AdditionsFeaturesLevel { get; private set; } = CreateAttributeArray();

    float _timer;

    public static LevelFeatures[] CreateAttributeArray() {
        LevelFeatures[] features = new LevelFeatures[10];

        for (int i = 0; i < 10; i++) {
            int numberOfElements = Enum.GetValues(typeof(Feature)).Length;
            features[i].FeatureValues = new FeatureValue[numberOfElements];
            for (int j = 0; j < numberOfElements; j++) {
                features[i].FeatureValues[j].Feature = Enum.GetValues(typeof(Feature)).Cast<Feature>().ToList()[j];
            }
        }

        return features;
    }

    protected float GetCurrentFeaturesValue(Feature feature) {
        return FeaturesLevel[Level - 1].GetValueFeature(feature);
    }

    public override void Create(SkillsManager skillsManager) {
        
    }

    public virtual void UpdateCall(SkillsManager skillsManager, float frameTime) {
        _timer += frameTime;
        float colldown = GetCurrentFeaturesValue(Feature.Colldown);

        if (_timer > colldown) {
            _timer = 0;
            Produce();
        }
    }

    protected virtual void Produce() {

    }
}