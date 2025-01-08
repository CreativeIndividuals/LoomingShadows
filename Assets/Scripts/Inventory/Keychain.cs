using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keychain : MonoBehaviour {
    [SerializeField]private List<int> keyIDs=new();
    public TextMeshProUGUI keyCount;
    public void addKey(int id){
        keyIDs.Add(id);
        keyCount.text=(keyIDs.Count).ToString();
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