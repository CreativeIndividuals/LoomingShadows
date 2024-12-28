using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormSwitcher : MonoBehaviour
{
    [SerializeField] GameObject Human;
    [SerializeField] GameObject Shadow;
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

        PlayerState.instance.SwitchState(PlayerState.playerState.human);
        human = true;
    }

    void SwitchToShadow()
    {
        Shadow.SetActive(true);

        Human.SetActive(false);

        SwitchParticles.Play();

        PlayerState.instance.SwitchState(PlayerState.playerState.shadow);
        human = false;
    }
}
