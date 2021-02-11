
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    // Use this for initialization
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        GameManager.instance.RegisterPowerUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerHealth.PowerUpHealth();
            Destroy(gameObject);
        }
    }
}
