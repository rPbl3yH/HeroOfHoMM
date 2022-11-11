using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlagueable {

    

    GameObject PlgueEffectPrefab { get; set; }

    void TakePlagueDamage(PlagueSkillStats plagueSkillStats);

    void CreatePlagueEffect();
    
}
