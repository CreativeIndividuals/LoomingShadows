using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseInventory : MonoBehaviour
{
    public static OpenCloseInventory Instance;
    private void Awake()
    {
        Instance = this;
    }


    public GameObject InventoryObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && InventoryObject.activeInHierarchy == false)
            OpenInventory();
        else if (Input.GetKeyDown(KeyCode.Tab) && InventoryObject.activeInHierarchy == true)
            CloseInventory();
    }
    public void OpenInventory()
    {
        InventoryObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseInventory()
    {
        InventoryObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
