using System.Runtime.InteropServices;
using UnityEngine;

public class HumanCombat : MonoBehaviour {
    [SerializeField]Animator animator;
    [Header("combat parameters")]
    [SerializeField]Damage dmg;
    BoxCollider2D range;
    [SerializeField] LayerMask enemyLayer;
    int comboCount=0;
    private void Start() {
        range=GetComponent<BoxCollider2D>();
    }
    private void Update() {

        if (Input.GetKeyDown(GameState.instance.currentState.Settings.keyBinds.Attack))
        {
            AnimatorMap.instance.setVar("attacking",true);
        }
        if (Input.GetKeyDown(GameState.instance.currentState.Settings.keyBinds.FireBall))
        {
            AnimatorMap.instance.setVar("fireballing",true);
        }
        if (Input.GetKeyDown(GameState.instance.currentState.Settings.keyBinds.MagicAtk))
        {
            AnimatorMap.instance.setVar("magicDown",true);
        }
        if (Input.GetKeyDown(GameState.instance.currentState.Settings.keyBinds.ChargeAtk))
        {
            AnimatorMap.instance.setVar("chargebtn",true);
        }
    }
}