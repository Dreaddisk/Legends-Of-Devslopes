using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    #region Varaibles
    [SerializeField]
    GameObject hero;

    [SerializeField]
    GameObject tanker;

    [SerializeField]
    GameObject soldier;

    [SerializeField]
    GameObject ranger;

    private Animator heroAnim;
    private Animator tankerAnim;
    private Animator soldierAnim;
    private Animator rangerAnim;

    #endregion


    #region UnityFunctions

    private void Awake()
    {
        Assert.IsNotNull(hero);
        Assert.IsNotNull(tanker);
        Assert.IsNotNull(soldier);
        Assert.IsNotNull(ranger);
    }
    // Use this for initialization
    void Start()
    {
        heroAnim = hero.GetComponent<Animator>();
        tankerAnim = tanker.GetComponent<Animator>();
        soldierAnim = soldier.GetComponent<Animator>();
        rangerAnim = ranger.GetComponent<Animator>();
    }



    #endregion

    IEnumerator showcase()
    {

        yield return new WaitForSeconds(1f);
        heroAnim.Play("SpinAttack");
        yield return new WaitForSeconds(1f);
        tankerAnim.Play("Attack");
        yield return new WaitForSeconds(1f);
        soldierAnim.Play("Attack");
        yield return new WaitForSeconds(1f);
        rangerAnim.Play("Attack");
        yield return new WaitForSeconds(1f);

        StartCoroutine(showcase());
    }

    public void Battle()
    {
        SceneManager.LoadScene("Level");

    }

    public void Quit()
    {
        Application.Quit();
    }



} // GameMenu class
