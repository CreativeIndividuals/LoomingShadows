using UnityEngine;

public class Teleporter : MonoBehaviour {//created to manage any teleport logic/animation/effects..
    public void teleport(Vector3 portal){
        transform.position = portal;
    }
}