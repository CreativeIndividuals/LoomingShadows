using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
public class GameState : MonoBehaviour {//manages and saves game state
    [SerializeField]public Structs.SaveData currentState;
    public static GameState instance;
    private void Start() {//TODO:loadSaveFile
        currentState = new Structs.SaveData(
        Structs.PlayerState.human,
        new Dictionary<Structs.EnemyTags, float>
        {
            { Structs.EnemyTags.human, 0f },
            { Structs.EnemyTags.shadow, 0f },
            { Structs.EnemyTags.boss, -20f },
            { Structs.EnemyTags.vulnerable, 2.5f },
            { Structs.EnemyTags.poisoned, 5f },
            { Structs.EnemyTags.burning, 10f },
            { Structs.EnemyTags.cursed, 100f },
            { Structs.EnemyTags.tank, -10f }
        }, // Empty dictionary for damage adjustments
        new List<Structs.storyItems>
        {Structs.storyItems.dash,Structs.storyItems.wings,Structs.storyItems.superdash}, // filled for testing must be empty 
        new List<Structs.areas>
        {Structs.areas.castle}, // starting area
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
            combatAtkCooldown: 0.5f,
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
