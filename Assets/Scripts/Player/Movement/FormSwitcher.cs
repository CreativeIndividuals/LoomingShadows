using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormSwitcher : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [Space]
    [SerializeField] GameObject Human;
    [SerializeField] float HumanDrag;
    [SerializeField] float HumanGravity;
    [Space]
    [SerializeField] GameObject Shadow;
    [SerializeField] float ShadowDrag;
    [SerializeField] float ShadowGravity;
    [Space]
    [SerializeField] ParticleSystem SwitchParticles;
    bool human = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && human)
            SwitchToShadow();
        else if (Input.GetKeyDown(KeyCode.B) && !human)
            SwitchToHuman();
    }

    void SwitchToHuman()
    {
        Shadow.SetActive(false);

        Human.SetActive(true);

        SwitchParticles.Play();

        GameState.instance.currentState.playerState=Structs.PlayerState.human;

        rb.drag = HumanDrag;
        rb.gravityScale = HumanGravity;

        human = true;
    }

    void SwitchToShadow()
    {
        Shadow.SetActive(true);

        Human.SetActive(false);

        SwitchParticles.Play();

        GameState.instance.currentState.playerState=Structs.PlayerState.shadow;

        rb.drag = ShadowDrag;
        rb.gravityScale = ShadowGravity;

        human = false;
    }
}
