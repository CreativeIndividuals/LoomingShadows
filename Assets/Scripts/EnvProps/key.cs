using UnityEngine;
using System.Collections.Generic;
public class key : MonoBehaviour {
    public int id;
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("contact");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Keychain>().addKey(id);
            Destroy(this.gameObject);
        }
    }
}