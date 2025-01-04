using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TMPro;

public class Keychain : MonoBehaviour {
    private int[] keyIDs;
    public Text keyCount;
    public int tryUseKey(int id){
        foreach (int key in keyIDs)
        {
            if(key==id){
                keyIDs.Pop(id);
                keyCount=(keyIDs.Length).ToString;
                return 0;
            }
        }
        return 1;
    }

}