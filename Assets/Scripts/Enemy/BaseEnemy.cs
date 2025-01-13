// using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
    public Structs.EnemyData data;
    public void init(){
        data=new();
        EnemyManager.instance.enemies.Add(this);
    }
    public void die(){
        EnemyManager.instance.enemies.Remove(this);
        Destroy(this);//TODO:add animation or particle system
    }
    private void Start() {
        init();
    }
    public void takeDamage(float damage){
        if(Random.Range(1,100)>data.stats.blockChance){
            data.stats.hp-=damage;
            if(data.stats.hp>=0)die();
        }
    }

    public void DealDamage()
    {
        PlayerHealth.instance.takeDamage(data.stats.damage);
    }
}