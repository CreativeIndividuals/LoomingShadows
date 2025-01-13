using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerDamage : MonoBehaviour {//holds your damage stats dmg upgrades multipliers crits...
    public static PlayerDamage instance;
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }
    [SerializeField]float dmg;//current damage stat
    [SerializeField]float mult;//multiplier (increases with combo ig)
    [SerializeField]int critChance;
    [SerializeField]float comboBonus;
    [SerializeField]float critDamage;//0 to 100
    private void Start() {//init
        dmg=GameState.instance.currentState.stats.dmg;
        mult=GameState.instance.currentState.stats.dmgMult;
        critChance=GameState.instance.currentState.stats.dmgCritChance;
        critDamage=GameState.instance.currentState.stats.dmgCritDmg;
        comboBonus=GameState.instance.currentState.stats.dmgComboBonus;
    }

    public float getEfectiveDamage(List<Structs.EnemyTags> toWho,int combo=0,bool Heavy=false){
        float effictiveDamage=dmg;//base dmg
        if (Random.Range(1,100)<critChance)effictiveDamage+=critDamage;
        foreach (KeyValuePair<Structs.EnemyTags,float> adjust in GameState.instance.currentState.damageAdjusts)
        {
            if (toWho.Contains(adjust.Key))effictiveDamage+=adjust.Value;
        }
        if(Heavy)effictiveDamage+=(dmg*3);
        return effictiveDamage*mult;
    }
    public float getEfectiveDamage(int combo,bool Heavy=false){
        float effictiveDamage=dmg;//base dmg
        if (Random.Range(1,100)<critChance)effictiveDamage+=critDamage;
        if(Heavy)effictiveDamage+=(dmg*3);
        return effictiveDamage*mult;
    }
}