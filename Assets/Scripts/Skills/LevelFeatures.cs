using System;
using UnityEngine;

[Serializable]
public struct LevelFeatures
{
    public FeatureValue[] FeatureValues;

    public float GetValueFeature(Feature feature) {
        for (int i = 0; i < FeatureValues.Length; i++) {
            if (FeatureValues[i].Feature == feature) { 
                return FeatureValues[i].Value;
            }
        }
        return 0;
    }

    public void SetFeature(Feature feature, float value) {
        for (int i = 0; i < FeatureValues.Length; i++) {
            if (FeatureValues[i].Feature == feature) {
                FeatureValues[i].Value = value;
            }
        }
    }
}