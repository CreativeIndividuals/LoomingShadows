using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keychain : MonoBehaviour {
    private List<int> keyIDs;
    public TextMeshProUGUI keyCount;
    public void addKey(int id){
        keyIDs.Add(id);
    }
    public int tryUseKey(int id){
        for(int i=0; i<keyIDs.Count;i++)
        {
            if(keyIDs[i]==id){
                keyIDs.RemoveAt(i);
                keyCount.text=(keyIDs.Count).ToString();
                return 0;
            }
        }
        return 1;
    }

}