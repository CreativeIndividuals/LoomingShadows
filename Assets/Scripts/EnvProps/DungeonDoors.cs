using UnityEngine;

public class DungeonDoors : MonoBehaviour {
    private BoxCollider2D bc;
    public void Open(){//TODO:add some door animation
        bc.isTrigger=true;
        EnemyManager.instance.doors.Remove(this);
    }
    public void Lock(){//TODO:add some door animation
        bc.isTrigger=false;
    }
    private void Start() {
        bc=GetComponent<BoxCollider2D>();
        EnemyManager.instance.doors.Add(this);
    }
}
