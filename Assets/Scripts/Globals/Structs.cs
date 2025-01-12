using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Structs {//holds global structures
    #region Enums
    public enum EnemyTags
    {//an enemy can contain all
       human,//generic (buff if shadow nerf if human)
       shadow,//generic (opposite of prev)
       boss,//reduction(shortuct for boss armour)
       vulnerable,//effect player applies
       poisoned,//same
       burning,//same
       cursed,//same but op (one hit can't be applied on bosses)
       blocking,//when an enemy has a shield or a block chance
       tank,//reduction(ma boi goated)
    }
    public enum areas{
        hw,
        pit,
        castle,
        outskirts,
        swamp,
        drainage,
        city,
        cavern,
        ally,
        depths,
        core,
        mines,
        utopia,
        lair,
        training,
        prefinal,
        final
    }
    public enum pickups{
        Coin10,
        Coin25,
        Coin50,//monetary value
        Heal10,
        Heal25,
        Heal50,//healing percentage
        Mana10,
        Mana25,
        Mana50//mana restored percentage
    }
    public enum storyItems{//abilities
        dash,
        wings,//dbjump
        superdash,//chargable long dash
        fireball,//any ranged spell really
        magicDown,//magical downwards attack like ddark in hollow knight
        magicUp,//magical downwards attack like shriek in hollow knight
        magicForward,//magic punch ig
        shadeLordsBlessing
    }
    public enum PlayerState
    {
        human,
        shadow
    }   
    #endregion
    #region SaveData
    //these must be saved on quit and are the ones to Load on start
    public struct SaveData{
        PlayerState playerState;
        List<areas> unlockedAreas;
        List<storyItems>FoundStoryItems;
        //damage addition on specific enemy tagss(can stack)
        Dictionary<EnemyTags,float> damageAdjusts;
        playerNumerics numerics;
        Settings settings;
    };
    
    #endregion
    #region Structs
    public struct KeyBinds//inspired from cuphead dead cells and hollow knight
    {
        KeyCode Jump;
        KeyCode Attack;
        KeyCode ChargeAtk;
        KeyCode Dash;
        KeyCode FireBall;
        KeyCode MagicAtk;
    }
    public struct Settings
    {
        KeyBinds keyBinds;
    }
    public struct playerNumerics{//any numbers the player carries
        float mana;
        float hp;
        float dmg;
        float dmgMult;
        float dmgCritChance;
        float dmgCritDmg;
        float autoheal;
    }
    public struct PlayerLocation{
        areas area;
        int room;
    }
    public struct EnemyData
    {
        List<Structs.EnemyTags> tags;//can change over time
        Dictionary<pickups,float> dropChances;//chance to drop pickups
    }
    public struct BossData{
        Item storyItem;
        areas areaUnlock;
    }
    #endregion
}