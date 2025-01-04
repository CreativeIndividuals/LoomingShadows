using UnityEngine;
using System.Collections.Generic;

public class portal : MonoBehaviour {
    public portal siblingPortal;// the whole portal not just position so we can add more logic if we wanted
    public Vector3 GetPosition(){
        return transform.position;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Teleporter>().teleport(siblingPortal.GetPosition());
        }
    }
}