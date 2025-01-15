using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCombat : MonoBehaviour {
    BoxCollider2D range;
    HumanMovement movement;
    List<Destroyable> TargetDestroyables=new();
    List<BaseEnemy> TargetEnemies=new();
    float coolDownLen;
    float heavyCooldownLen;
    [SerializeField]float atkAnimLen=0.4f;
    [SerializeField]float heavyAtkAnimLen=0.5f;
    int comboCount=0;
    bool heavyATK=false;
    bool coolDown;
    bool heavyCoolDown;
    const int comboTimerLen=5;
    bool attackedInTimer=false;
    public static HumanCombat instance;

    public unsubscribe(BaseEnemy enemy){
        TargetEnemies.Remove(enemy);
    }
    public unsubscribe(Destroyable prop){
        TargetDestroyables.Remove(prop);
    }
    private void Awake() {
        instance=this;
    }
    private void Start() {
        movement=GetComponent<HumanMovement>();
        range=GetComponent<BoxCollider2D>();
        coolDownLen=GameState.instance.currentState.stats.combatAtkCooldown;
        heavyCooldownLen=GameState.instance.currentState.stats.combatHeavyAtkCooldown;
        StartCoroutine(ComboTimer());
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TargetEnemies.Add(other.gameObject.GetComponent<BaseEnemy>());
        }
        if(other.gameObject.CompareTag("Destroyable")){
            TargetDestroyables.Add(other.gameObject.GetComponent<Destroyable>());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TargetEnemies.Remove(other.gameObject.GetComponent<BaseEnemy>());
        }
        if(other.gameObject.CompareTag("Destroyable")){
            TargetDestroyables.Remove(other.gameObject.GetComponent<Destroyable>());
        }
    }
    private void Update() {
        
        if (Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.Attack))
        {
            AnimatorMap.instance.setVar("attacking",true);
            Invoke(nameof(endAtkAnim),atkAnimLen);
            ExecuteAttacks();
            coolDown=true;
            Invoke(nameof(endCooldown),coolDownLen);
        }
        if (Input.GetKeyDown(GameState.instance.currentState.settings.keyBinds.heavyAtk))
        {
            movement.PauseMovement(heavyAtkAnimLen);
            heavyATK=true;
            AnimatorMap.instance.setVar("heavyBtn",true);
            Invoke(nameof(endHeavyAtkAnim),heavyAtkAnimLen);
            Invoke(nameof(ExecuteAttacks),0.2f);
            heavyCoolDown=true;
            Invoke(nameof(endheavyCooldown),heavyCooldownLen);
        }
    }
    void endCooldown(){
        coolDown=false;
    }
    void endheavyCooldown(){
        heavyCoolDown=false;
    }
    void endHeavyAtkAnim(){
        AnimatorMap.instance.setVar("heavyBtn",false);
    }
    void endAtkAnim(){
        AnimatorMap.instance.setVar("attacking",false);
    }
    IEnumerator ComboTimer(){
        attackedInTimer=false;
        yield return new WaitForSeconds(comboTimerLen);
        if (!attackedInTimer)
        {
            comboCount=0;
        }
        StartCoroutine(ComboTimer());
    }
    private void ExecuteAttacks(){
        attackedInTimer=true;
        if (TargetEnemies.Count==0 && TargetDestroyables.Count==0){//you missed no combo for you
            comboCount=0;
            return;
        }
        foreach (BaseEnemy enemy in TargetEnemies)
        {
            enemy.takeDamage(PlayerDamage.instance.getEfectiveDamage(enemy.data.tags,comboCount,heavyATK));
        }
        foreach(Destroyable prop in TargetDestroyables){
            prop.takeDamage(PlayerDamage.instance.getEfectiveDamage(comboCount,heavyATK));
        }
        comboCount++;//successful attack
    }
}