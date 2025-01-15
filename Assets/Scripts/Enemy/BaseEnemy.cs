using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
    public Structs.EnemyData data;
    public float hpdebug;
    public void init(){
        data=new();
        data.tags=new List<Structs.EnemyTags>{Structs.EnemyTags.human};
        data.stats.hp= hpdebug=100f;
        EnemyManager.instance.enemies.Add(this);
    }
    public void die(){
        EnemyManager.instance.enemies.Remove(this);
        Destroy(this.gameObject);//TODO:add animation or particle system
    }
    private void Start() {
        init();
    }
    public void takeDamage(float damage){
        if(Random.Range(1,100)>data.stats.blockChance){
            hpdebug=data.stats.hp-=damage;
            if(data.stats.hp>=0)die();
        }
        
    }

    public void DealDamage()
    {
        PlayerHealth.instance.takeDamage(data.stats.damage);
    }
}