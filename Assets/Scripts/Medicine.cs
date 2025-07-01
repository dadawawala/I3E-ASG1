using UnityEngine;

public class Medicine : MonoBehaviour
{
    [Header("Medicine Settings")]
    public float rotationSpeed = 50f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip collectSound;
    
    private Vector3 startPosition;
    private UIManager uiManager;
    
    void Start()
    {
        startPosition = transform.position;
    
        GameObject uiCanvasObject = GameObject.FindGameObjectWithTag("UIManager");
        if (uiCanvasObject != null)
        {
            uiManager = uiCanvasObject.GetComponent<UIManager>();
        }
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectMedicine(other.gameObject);
        }
    }
    
    void CollectMedicine(GameObject player)
    {
        Debug.Log("Medicine collected! Game won!");
        
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        
        int playerHealth = 100;
        
        if (uiManager != null)
        {
            uiManager.ShowWinScreen(playerHealth);
        }
        
        Destroy(gameObject);
    }
}