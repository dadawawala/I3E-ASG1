using UnityEngine;

public class KeycardPiece : MonoBehaviour
{
    public KeycardDoor targetDoor;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectKeycard();
                
                if (targetDoor != null)
                {
                    targetDoor.AddKeycard();
                }
                
                Destroy(gameObject);
            }
        }
    }
}