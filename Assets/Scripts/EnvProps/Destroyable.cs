using UnityEngine;

public class Destroyable : MonoBehaviour {
    public float health;
    public WorldItem loot;//if its a box or smt
    public void takeDamage(float amount){
        health-=amount;
        if(health>0){
            Instantiate(loot);
            Destroy(this.gameObject);
        }
    }
}