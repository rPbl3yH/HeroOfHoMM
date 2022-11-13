using System;
using System.Linq;
using UnityEngine;

public class ActiveSkill : Skill
{
    public Feature AllowedFeatures;

    [HideInInspector]
    [field: SerializeField] public LevelFeatures[] FeaturesLevel { get; private set; } = CreateAttributeArray();
    [HideInInspector]
    [field: SerializeField] public LevelFeatures[] AdditionsFeaturesLevel { get; private set; } = CreateAttributeArray();

    float _timer, _timerProducing;
    private bool _isProducing;
    protected TopIconCard CurrentIconCard;

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

    protected float GetAdditionalFeaturesValue(Feature feature, int level) {
        return AdditionsFeaturesLevel[level].GetValueFeature(feature);
    }


    public virtual void UpdateCall(SkillsManager skillsManager, float frameTime) {
        if(!_isProducing)
            _timer += frameTime;
        
        float colldown = GetCurrentFeaturesValue(Feature.Colldown) * GameManager.Instance.Player.PlayerStats.ColldownMultiplier;
        if (CurrentIconCard) {
            float value = _timer / colldown;
            CurrentIconCard.SetSkillDownloadValue(value);
        }

        if (_timer > colldown && !_isProducing) {
            Produce();
            _isProducing = true;
        }

        if (_isProducing) {
            _timerProducing += Time.deltaTime;
            float duration = GetCurrentFeaturesValue(Feature.Duration);
            if (_timerProducing > duration) {
                EndProduce();
                _timerProducing = 0;
                _timer = 0;
                _isProducing = false;
            }
        } 
    }

    protected virtual void Produce() {

    }

    protected virtual void EndProduce() {

    }

    public virtual void SetTopIconCard(TopIconCard topIconCard) {
        CurrentIconCard = topIconCard;
    }

    public override string GetCurrentLevelDecription() {
        if (Level == 0) {
            return Desctiprion;
        }
        string result = "";

        foreach (Feature feature in Enum.GetValues(typeof(Feature))) {
            if (!AllowedFeatures.HasFlag(feature)) continue;
            var nextLevel = Level == 10 ? 10 : Level;
            var value = GetAdditionalFeaturesValue(feature, nextLevel);
            if (value == 0) continue;
            var valueLabel = value.ToString();
            valueLabel = value > 0? "+" + valueLabel : valueLabel;    

            result += $"{feature} <color=#00ff00ff>{valueLabel}</color>\n";
        }

        return result;
    }

}