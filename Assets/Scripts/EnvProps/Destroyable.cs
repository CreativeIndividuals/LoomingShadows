using UnityEngine;

public class Destroyable : MonoBehaviour {
    public float health;
    public GameObject loot;//if its a box or smt
    public void takeDamage(float amount){//TODO:invoke this from combat scripts when implemented
        health-=amount;
        if(health>=0){
            Instantiate(loot);
            //play some effect
            Destroy(this);
        }
    }
}