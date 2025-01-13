using UnityEngine;

public class AnimatorMap : MonoBehaviour {// there are the same animations for both forms and this will map the animator calls to the correct one
    [SerializeField]Animator humanAnimator;
    [SerializeField]Animator shadowAnimator;
    public static AnimatorMap instance;
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }
    public void setVar(string var,bool value){
        if (GameState.instance.currentState.playerState==Structs.PlayerState.human){
        humanAnimator.SetBool(var,value);
        }else shadowAnimator.SetBool(var,value);
    }
    // public void setVar(string var,int value){
    //     if(playerState.instance.state==Structs.PlayerState.human){
    //         humanAnimator.SetInt(var,value);
    //     }else{
    //         shadowAnimator.SetInt(var,value);
    //     }
    // }
    public void setVar(string var,float value){
        if(GameState.instance.currentState.playerState==Structs.PlayerState.human)
        {
        humanAnimator.SetFloat(var,value);
        }else shadowAnimator.SetFloat(var,value);
    }
}