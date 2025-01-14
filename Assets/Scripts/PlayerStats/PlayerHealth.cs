using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance {get;private set;}
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }
    [Header("References")]
    [SerializeField] Slider HpSlider;
    [SerializeField] SpriteRenderer[] ObjectSprite;

    [Header("Values")]
    [SerializeField] float MaxHp = 100f;
    [SerializeField] float CurrentHp;
    [SerializeField] int DodgeChance;
    [Space]
    [SerializeField] float damageFlashDuration = 0.05f;
    [SerializeField] float HealFlashDuration = 0.05f;
    Color[] OriginColor;
    [SerializeField] enum DeathMode { RestartGame, DestroyObject, Nothing };
    public bool Dead;
    [Space]
    [SerializeField] DeathMode AfterDeath = DeathMode.DestroyObject;

    [Header("Sounds")]
    [SerializeField] AudioSource _Audio;
    [SerializeField] AudioClip Hit;

    [Header("Particles")]
    [SerializeField] GameObject DeathParticle;

    [Header("Knockback")]
    public float knockbackForce;

    [Header("AutoHeal")]
    public float AutoHealTimer;//how long one must not take damage to autoheal
    public float AutoHealAmount;//how much does it heal per second
    public float AutoHealThershold;//we wont autoheal to full

    private Rigidbody2D rb;
    private bool takenDamageDuringTimer;
    private bool takenDamageDuringAutoHeal;
    bool invencible=false;
    [SerializeField]float invencibilityDuration=0.7f;
    private void Start()
    {
        CurrentHp = GameState.instance.currentState.stats.hp;
        AutoHealAmount=GameState.instance.currentState.stats.autoheal;
        DodgeChance=GameState.instance.currentState.stats.dodgeChance;
        rb = GetComponent<Rigidbody2D>();

        OriginColor = new Color[ObjectSprite.Length];
        if (ObjectSprite != null)
        {
            for (int i = 0; i < ObjectSprite.Length; i++)
            {
                OriginColor[i] = ObjectSprite[i].color;
            }
        }
    }

    private void Update()
    {
        if (Dead)
            return;

        //limit the current hp from going over the max hp and update the values
        if (CurrentHp > MaxHp)
            CurrentHp = MaxHp;

        if (HpSlider != null)
        {
            HpSlider.maxValue = MaxHp;
            HpSlider.value = CurrentHp;
        }
        
        //death code
        if (CurrentHp <= 0)
        {
            if (AfterDeath == DeathMode.DestroyObject)
            {
                if (DeathParticle != null)
                    Instantiate(DeathParticle, transform.position, DeathParticle.transform.rotation);

                Destroy(gameObject);
            }
            else if (AfterDeath == DeathMode.RestartGame)
            {
                SceneLoader.instance.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (AfterDeath == DeathMode.Nothing)
            {
                Dead = true;
            }
        }
    }

    public void ChangeMaxHealth(float number)
    {
        if (Dead)
            return;

        MaxHp = number;
    }
    IEnumerator startAutoHealing(){
        float healedHP=0f;
        while ((healedHP>=AutoHealThershold)&&(!takenDamageDuringAutoHeal)){
            Heal(AutoHealAmount);
            healedHP+=AutoHealAmount;
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator startAutoHealTimer(){
        yield return new WaitForSeconds(AutoHealTimer);
        if (!takenDamageDuringTimer) StartCoroutine(startAutoHealing());
    }

    public void takeDamage(float number=1)
    {
        if (invencible)return;
        if(Random.Range(1,100)<DodgeChance) return;
        StartCoroutine(startAutoHealTimer());
        if (Dead)
            return;
        CurrentHp -= number;

        if (ObjectSprite != null)
            damageFlash();

        if (_Audio != null)
            _Audio.PlayOneShot(Hit);
        invencible=true;
        Invoke(nameof(depleteIframes),invencibilityDuration);
    }
    void depleteIframes(){
        invencible=false;
    }

    public void Heal(float number = 1)
    {
        if (Dead)
            return;

        CurrentHp += number;

        if (ObjectSprite != null)
            HealFlash();
    }

    [ContextMenu("damage by one")]
    void damageByOne()
    {
        takeDamage(1);
    }

    [ContextMenu("Heal by one")]
    void HealByOne()
    {
        Heal(1);
    }

    void damageFlash()
    {
        for (int i = 0; i < ObjectSprite.Length; i++)
        {
            ObjectSprite[i].color = Color.red;
        }
        Invoke(nameof(ResetColor), damageFlashDuration);
    }

    void HealFlash()
    {
        for (int i = 0; i < ObjectSprite.Length; i++)
        {
            ObjectSprite[i].color = Color.green;
        }
        Invoke(nameof(ResetColor), HealFlashDuration);
    }

    void ResetColor()
    {
        for (int i = 0; i < ObjectSprite.Length; i++)
        {
            ObjectSprite[i].color = OriginColor[i];
        }
    }
    
    public void ApplyKnockback(Vector2 direction)
    {
        if (rb != null)
        {
            rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
