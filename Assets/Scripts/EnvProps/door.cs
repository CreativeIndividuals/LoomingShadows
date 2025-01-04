using UnityEngine;

public class door : MonoBehaviour {
    public int id;
    private void open(){//TODO:add some door animation and disable collider
        Destroy(this.gameObject);//destroy for now
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(other.gameObject.GetComponent<Keychain>().tryUseKey(id)==0)open();
        }
    }
}