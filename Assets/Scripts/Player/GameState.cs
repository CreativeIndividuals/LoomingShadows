using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameState : MonoBehaviour {//manages and saves game state
    public Structs.SaveData currentState;
    public static GameState instance;
    private void Start() {//TODO:loadSaveFile
currentState = new Structs.SaveData(
    Structs.PlayerState.human,
    new Dictionary<EnemyTags, float>
{
    { EnemyTags.human, 0f },
    { EnemyTags.shadow, 0f },
    { EnemyTags.boss, -20f },
    { EnemyTags.vulnerable, 2.5f },
    { EnemyTags.poisoned, 5f },
    { EnemyTags.burning, 10f },
    { EnemyTags.cursed, 100f },
    { EnemyTags.tank, -10f }
}, // Empty dictionary for damage adjustments
    new List<Structs.storyItems>(), // Empty list of found story items
    new List<Structs.areas>(), // Empty list of unlocked areas
    new Structs.PlayerStats(
        mana: 100f,
        hp: 100f,
        dmg: 10f,
        dmgMult: 1f,
        dmgCritChance: 20,
        dmgCritDmg: 5f,
        autoheal: 5f,
        dodgeChance: 2,
        dmgComboBonus: 2.5f,
        combatAtkCooldown: 0.3f,
        combatHeavyAtkCooldown: 1f
    ),
    new Structs.Settings(
        new Structs.KeyBinds(
            KeyCode.Z,   // Jump
            KeyCode.X,   // Attack
            KeyCode.V,   // HeavyAtk
            KeyCode.C,   // Dash
            KeyCode.A,   // FireBall
            KeyCode.LeftShift, // MagicAtk
            KeyCode.S    // SDash
        ),
        masterVolume: 100,
        sfxVolume: 100,
        musicVolume: 100,
        visual: new Structs.VisualSettings() // Default visual settings
    ),
    new Structs.PlayerLocation(Structs.areas.castle, 0) // Starting location: castle, position 0
);
}
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }
    public int Modify<T>(T value){//TODO:modify values by type passed
        //im not too much into c# how do you filter a struct by types dw all currentState values are of a unique type
        return 0;
    }
    public int Save(){//TODO:save the game
        return 0;
    }
}