using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public int maxKeycardPieces = 4;
    private int collectedKeycards = 0;

    public UIManager uiManager;

    void Start()
    {
        uiManager.UpdateHealth(health);
        uiManager.UpdateKeycards(collectedKeycards, maxKeycardPieces);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        uiManager.UpdateHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void CollectKeycard()
    {
        collectedKeycards++;
        uiManager.UpdateKeycards(collectedKeycards, maxKeycardPieces);
    }

    public bool HasAllKeycards()
    {
        return collectedKeycards >= maxKeycardPieces;
    }

    void Die()
{
    uiManager.ShowDeathScreen();

    GetComponent<CharacterController>().enabled = false;
    
    GetComponent<PlayerMovement>().enabled = false;

    this.enabled = false;
}

    public int GetCurrentHealth()
    {
        return health;
    }
}

