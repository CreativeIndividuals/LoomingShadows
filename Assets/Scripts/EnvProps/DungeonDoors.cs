using UnityEngine;

public class DungeonDoors : MonoBehaviour {
    public void open(){//TODO:add some door animation and disable collider
        Destroy(this.gameObject);//destroy for now
    }
    private void Start() {
        EnemyManager.instance.doors.Add(this);
    }
}
