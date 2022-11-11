using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : ScriptableObject
{
    public string Name;
    public string Desctiprion;
    public Sprite Image;
    public int Level = 0;

    public abstract void Create(SkillsManager skillsManager);

    public virtual void Activate() {
        Level++;
    }
}
