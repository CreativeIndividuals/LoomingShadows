using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Damage : MonoBehaviour {//holds your damage stats dmg upgrades multipliers crits...
    [SerializeField]float dmg;//current damage stat
    [SerializeField]float mult;//multiplier (increases with combo ig)
    [SerializeField]float critChance;
    [SerializeField]float critDamage;//0 to 100
    private void Start() {//init
        dmg=GameState.instance.currentState.numerics.dmg;
        mult=GameState.instance.currentState.numerics.dmgMult;
        critChance=GameState.instance.currentState.numerics.dmgCritChance;
        critDamage=GameState.instance.currentState.numerics.dmgCritDamage;
    }

    public float getEfectiveDamage(List<Structs.EnemyTags> toWho){
        float effictiveDamage=dmg;//base dmg
        if (new Random.range(1,100)<critChance)effictiveDamage+=critDamage;
        foreach (KeyValuePair<Structs.EnemyTags,float> adjut in GameState.instance.currentState.damageAdjusts)
        {
            if (toWho.Contains(adjut.key))effictiveDamage+=adjut.Value;
        }
        return effictiveDamage*mult;
    }
}