using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyAttack : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float range = 3f;

    [SerializeField]
    private float timeBetweenAttacks = 2f;

    private Animator anim;
    private GameObject player;

    private bool playerInRange;

    private BoxCollider[] weaponColliders;
    private EnemyHealth enemyHealth;

    #endregion




    #region UnityFunctions
    // Use this for initialization
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());

        // Startcoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }


    #endregion

    IEnumerator Attack()
    {
        if(playerInRange && !GameManager.instance.GameOver && enemyHealth.IsAlive)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(Attack());
    }

    public void EnemyBeginAttack()
    {
        foreach(var weapon in weaponColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EnemyEndAttack()
    {
        foreach(var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }




} // EnemyAttack class
