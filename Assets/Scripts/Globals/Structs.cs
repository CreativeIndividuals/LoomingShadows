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
        public PlayerState playerState;
        public List<areas> unlockedAreas;
        public List<storyItems> foundStoryItems; //damage addition on specific enemy tagss(can stack)
        public Dictionary<EnemyTags,float> damageAdjusts;
        public PlayerStats stats;
        public Settings settings;
        public PlayerLocation location;
        public SaveData(PlayerState lstate,Dictionary<EnemyTags,float> lDMGadjusts,List<storyItems> lFoundStoryItems,List<areas> Uareas,PlayerStats lstats,Settings lsettings, PlayerLocation loc){
            playerState=lstate;
            unlockedAreas=Uareas;
            damageAdjusts=lDMGadjusts;
            foundStoryItems=lFoundStoryItems;
            stats=lstats;
            settings=lsettings;
            location=loc;
        }
    };
    
    #endregion
    #region Structs
    public struct KeyBinds//inspired from cuphead dead cells and hollow knight
    {
        public KeyCode Jump;
        public KeyCode Attack;
        public KeyCode heavyAtk;
        public KeyCode Dash;
        public KeyCode FireBall;
        public KeyCode MagicAtk;
        public KeyCode Sdash;

        public KeyBinds(
        KeyCode jump,
        KeyCode attack,
        KeyCode heavyAtk,
        KeyCode dash,
        KeyCode fireBall,
        KeyCode magicAtk,
        KeyCode sDash)
    {
        Jump = jump;
        Attack = attack;
        this.heavyAtk = heavyAtk;
        Dash = dash;
        FireBall = fireBall;
        MagicAtk = magicAtk;
        Sdash = sDash;
    }
    }
    public struct Settings
    {
        public KeyBinds keyBinds;
        public int masterVolume;
        public int sfxVolume;
        public int musicVolume;
        public VisualSettings visual;
        public Settings(KeyBinds keyBinds, int masterVolume, int sfxVolume, int musicVolume, VisualSettings visual)
        {
        this.keyBinds = keyBinds;
        this.masterVolume = masterVolume;
        this.sfxVolume = sfxVolume;
        this.musicVolume = musicVolume;
        this.visual = visual;
    }
    }
    public struct VisualSettings
    {
        //shaders and shit
    }
    public struct PlayerStats{//any numbers the player carries
        public float mana;
        public float hp;
        public float dmg;
        public float dmgMult;
        public int dmgCritChance;
        public float dmgCritDmg;
        public float autoheal;
        public int dodgeChance;
        public float dmgComboBonus;
        public float combatAtkCooldown;
        public float combatHeavyAtkCooldown;
        public PlayerStats(
        float mana, 
        float hp, 
        float dmg, 
        float dmgMult, 
        int dmgCritChance, 
        float dmgCritDmg, 
        float autoheal, 
        int dodgeChance, 
        float dmgComboBonus, 
        float combatAtkCooldown, 
        float combatHeavyAtkCooldown)
    {
        this.mana = mana;
        this.hp = hp;
        this.dmg = dmg;
        this.dmgMult = dmgMult;
        this.dmgCritChance = dmgCritChance;
        this.dmgCritDmg = dmgCritDmg;
        this.autoheal = autoheal;
        this.dodgeChance = dodgeChance;
        this.dmgComboBonus = dmgComboBonus;
        this.combatAtkCooldown = combatAtkCooldown;
        this.combatHeavyAtkCooldown = combatHeavyAtkCooldown;
    }
    }
    public struct PlayerLocation{
        public areas area;
        public int room;
        public PlayerLocation( areas area,int roomindex){
            this.area=area;
            this.room=roomindex;
        }
    }
    public struct EnemyStats{
        public float hp;
        public float damage;
        public int blockChance;
    }
    public struct BossStats{//temp defenition
        public float hp;
        public float damage;
        public int blockChance;
    }
    public struct EnemyData
    {
        public EnemyStats stats;
        public List<Structs.EnemyTags> tags;//can change over time
        public Dictionary<pickups,float> dropChances;//chance to drop pickups
        public List<GameObject> spawns;//enemies spawned on death
    }
    public struct BossData
    {
        public BossStats stats;
        public List<Structs.EnemyTags> tags;//can change over time
        public Dictionary<pickups,float> dropChances;//chance to drop pickups
        public List<GameObject> spawns;//enemies spawned on death
        public storyItems storyItem;
        public areas areaUnlock;
    }
    #endregion
}