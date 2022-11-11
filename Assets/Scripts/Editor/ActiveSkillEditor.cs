using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveSkill), true)]
public class ActiveSkillEditor : Editor
{
    const float width = 100f;

    GUIStyle _styleChanged;
    GUIStyle _styleDefault;
    GUIStyle _styleSum;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        _styleChanged = new GUIStyle(GUI.skin.textField);
        _styleChanged.normal.textColor = Color.green;
        _styleChanged.onHover.textColor = Color.green;

        _styleDefault = new GUIStyle(GUI.skin.textField);
        _styleDefault.normal.textColor = Color.white * 0.38f;
        _styleDefault.onHover.textColor = Color.white * 0.46f;

        _styleSum = new GUIStyle(GUI.skin.label);
        _styleSum.normal.textColor = Color.white * 0.42f;
        _styleSum.onHover.textColor = Color.white * 0.5f;

        ActiveSkill activeSkill = target as ActiveSkill;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(GUILayout.Width(60));
        GUILayout.Label("");

        for (int i = 0; i < 10; i++) {
            GUILayout.Label("LVL " + (i + 1));
            
        }
        EditorGUILayout.EndVertical();

        foreach (Feature item in Enum.GetValues(typeof(Feature))) {
            DrawColoumn(activeSkill, item.ToString(), item);
        }

        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(activeSkill);
    }

    private void DrawColoumn(ActiveSkill activeSkill, string label, Feature feature) {
        if (!activeSkill.AllowedFeatures.HasFlag(feature)) return;

        EditorGUILayout.BeginVertical(GUILayout.Width(width));
        GUILayout.Label(label, GUILayout.Width(width));
        float sum = 0;
        for (int i = 0; i < 10; i++) {
            GUILayout.BeginHorizontal();
            float fieldValue = activeSkill.AdditionsFeaturesLevel[i].GetValueFeature(feature);
            GUIStyle styleValue = fieldValue == 0 ? _styleDefault : _styleChanged;
            float value = EditorGUILayout.FloatField(fieldValue, styleValue, GUILayout.Width(width - 50));
            activeSkill.AdditionsFeaturesLevel[i].SetFeature(feature, value);
            sum += value;
            activeSkill.FeaturesLevel[i].SetFeature(feature, sum);
            GUILayout.Label(sum.ToString(), _styleSum);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

    }
}   
