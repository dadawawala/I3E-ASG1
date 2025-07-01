using UnityEngine;

public class LavaDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LavaDrop trigger entered by: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Dealing damage to player!");
                player.TakeDamage(10);
            }
            else
            {
                Debug.Log("PlayerController component not found!");
            }
        }
    }
}