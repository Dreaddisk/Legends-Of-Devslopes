using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private LayerMask layerMask;

    private CharacterController characterController;

    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;

    private BoxCollider[] swordcolliders;

    private GameObject fireTrail;
    private ParticleSystem fireTrailParticles;

    #endregion

    #region UnityFunctions

    // Use this for initialization
    void Start()
    {
        fireTrail = GameObject.FindWithTag("Fire") as GameObject;
        fireTrail.SetActive(false);

        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordcolliders = GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

        if (moveDirection == Vector3.zero)
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("DoubleChop");
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.Play("SpinAttack");
        }
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    public void BeginAttack()
    {
        foreach(var weapon in swordcolliders)
        {
            weapon.enabled = true;
        }
    }

    public void EndAttack()
    {
        foreach(var weapon in swordcolliders)
        {
            weapon.enabled = false;
        }
    }

    public void SpeedPowerUp()
    {
        StartCoroutine(FireTrailRoutine());
    }

    IEnumerator FireTrailRoutine()
    {
        fireTrail.SetActive(true);
        moveSpeed = 10f;
        yield return new WaitForSeconds(10f);

        moveSpeed = 6f;
        fireTrailParticles = fireTrail.GetComponent<ParticleSystem>();
        var em = fireTrailParticles.emission;
        em.enabled = false;
        yield return new WaitForSeconds(3f);

        em.enabled = true;
        fireTrail.SetActive(false);
    }




} // PlayerController class
