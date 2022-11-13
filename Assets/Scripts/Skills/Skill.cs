using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : ScriptableObject
{
    public string Name;
    public string Desctiprion;
    public Sprite Image;
    public int Level = 0;


    public virtual void Activate() {
        Level++;

        SetLevel();
    }

    public virtual void SetLevel() {

    }


    public virtual string GetCurrentLevelDecription() {
        return "";
    }
}
