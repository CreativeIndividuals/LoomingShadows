using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {//manages and saves game state
    Structs.SaveData currentState;
    public static GameState instance;
    private void Start() {//TODO:loadSaveFile
        currentState=new(//default values for now
        Structs.PlayerState.human,
        new(),
        new(),
        new(0f,0f-20f,2.5f,5f,10f,100f,-10f,-10f),
        new(100f,100f,10f,0f,20f,5f),
        new(new(KeyCode.Z,KeyCode.X,KeyCode.C,KeyCode.V,KeyCode.A,KeyCode.Shift))
        );
    }
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }
    public int Modify<T>(T value){//TODO:modify values by type passed
        //im not too much into c# how do you filter a struct by types dw all currentState values are of a unique type
    }
    public int Save(){//TODO:save the game
        
    }
}