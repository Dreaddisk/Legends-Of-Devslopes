using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{
    #region Varaibles
    [SerializeField]
    private int startingHealth = 200;

    [SerializeField]
    private float timeSinceLastHit = 2f;

    [SerializeField]
    private Slider healthSlider;

    private float timer = 0f;

    private int currentHealth;

    private CharacterController characterController;
    private AudioSource audioSource;
    private Animator anim;
    private ParticleSystem blood;

    #endregion

    public int CurrentHealth
    {
        get { return currentHealth; }

        set
        {
            if(value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }


    #region UnityFucntions

    private void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audioSource = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }
    #endregion


    void TakeHit()
    {
        if(currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audioSource.PlayOneShot(audioSource.clip);
            blood.Play();
        }

        if (currentHealth <= 0)
            KillPlayer();
    }

    void KillPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        audioSource.PlayOneShot(audioSource.clip);
        blood.Play();
    }

    public void PowerUpHealth()
    {
        if (currentHealth <= 70)
        {
            CurrentHealth += 30;
        }
        else if (currentHealth < startingHealth)
        {
            CurrentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
    }
}




} // PlayerHealth class
