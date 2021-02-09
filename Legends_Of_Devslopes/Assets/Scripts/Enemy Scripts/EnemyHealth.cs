using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int startingHealth = 20;

    [SerializeField]
    private float timeSinceLastHit = 0.5f;

    [SerializeField]
    private float dissapearSpeed = 2f;

    private AudioSource audio;
    private Animator anim;
    private NavMeshAgent nav;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private ParticleSystem blood;

    private float timer = 0;

    private bool isAlive;
    private bool dissapearEnemy = false;

    private int currentHealth;
    #endregion

    public bool IsAlive
    {
        get { return isAlive; }
    }


    #region UnityFunction
    // Use this for initialization
    void Start()
    {
        GameManager.instance.RegisterEnemy(this);
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isAlive = true;
        currentHealth = startingHealth;
        blood = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                TakeHit();
                blood.Play();
                timer = 0f;
            }
        }
    }

    #endregion

    void TakeHit()
    {
        if(currentHealth > 0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            currentHealth -= 10;
        }

        if(currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        GameManager.instance.KilledEnemy(this);
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidBody.isKinematic = true;
        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(4f);
        dissapearEnemy = true;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


} // EnemyHealth class
