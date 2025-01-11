using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour {//created to manage any teleport logic/animation/effects..
    public float cooldownLength=0.5f;
    private bool onCooldown=false;
    public void teleport(Vector3 portal){
        if(onCooldown) return;
        transform.position = portal;
        onCooldown=true;
        StartCoroutine(cooldown());
    }
    IEnumerator cooldown(){
        yield return new WaitForSeconds(cooldownLength);
        onCooldown=false;
    }
}