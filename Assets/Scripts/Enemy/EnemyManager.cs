using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;
    public List<DungeonDoors> doors;    
    public List<BaseEnemy> enemies;
    public float safeInterval=1f;//timer to check if the room has no enemies and open the doors for the player
    private void OnEnable() {
        if (instance==this)
        {
            return;
        }
        Destroy(instance.gameObject);
        instance=this;
    }
    private void Start() {
        StartCoroutine(safeCheckTimer());
    }
    IEnumerator safeCheckTimer(){
        if (enemies.Count!=0)
        {
            yield return new WaitForSeconds(safeInterval);
        }else{
            foreach (DungeonDoors door in doors)
            {
             door.open();   
            }
        }
        
    }
}