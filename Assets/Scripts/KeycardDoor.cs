using UnityEngine;
using System.Collections;

public class KeycardDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorTransform;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;
    
    [Header("Keycard Requirements")]
    public int requiredKeycards = 4;
    public int currentKeycards = 0;
    
    [Header("Door Collision")]
    public GameObject doorBarrier;
    public AudioSource audioSource;
    public AudioClip unlockSound;
    public AudioClip openSound;
    public AudioClip deniedSound;
    
    private Vector3 closedRotation;
    private Vector3 openRotation;
    private bool isOpening = false;
    private bool playerInRange = false;
    
    void Start()
    {
        Debug.Log("KeycardDoor script is starting!");
        
        if (doorTransform == null)
            doorTransform = transform;
            
        closedRotation = doorTransform.eulerAngles;
        openRotation = closedRotation + new Vector3(0, openAngle, 0);
        
        EnableDoorBarrier(true);
        
        Debug.Log("KeycardDoor setup complete!");
        Debug.Log($"Door has {GetComponents<Collider>().Length} colliders");
        
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log($"Collider {i}: IsTrigger = {colliders[i].isTrigger}");
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"E key pressed! Player in range: {playerInRange}, Current keycards: {currentKeycards}/{requiredKeycards}");
        }
        
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }
    
    public void AddKeycard()
    {
        if (currentKeycards < requiredKeycards)
        {
            currentKeycards++;
            
            Debug.Log($"Keycard collected! ({currentKeycards}/{requiredKeycards})");
            
            if (currentKeycards >= requiredKeycards)
            {
                Debug.Log("All keycards collected! Door can now be opened. Press E near the door.");
                PlaySound(unlockSound);
            }
        }
    }
    
    void TryOpenDoor()
    {
        if (isOpen || isOpening)
            return;
            
        if (currentKeycards >= requiredKeycards)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log($"Need {requiredKeycards - currentKeycards} more keycards to open this door.");
            PlaySound(deniedSound);
        }
    }
    
    void OpenDoor()
    {
        if (isOpening || isOpen)
            return;
            
        StartCoroutine(OpenDoorCoroutine());
        PlaySound(openSound);
    }
    
    IEnumerator OpenDoorCoroutine()
    {
        isOpening = true;
        float elapsedTime = 0;
        

    Vector3 pivotOffset = new Vector3(-0.5f, 0, 0);
    Vector3 pivotPoint = doorTransform.position + doorTransform.TransformDirection(pivotOffset);
    float startAngle = 0f;
    float targetAngle = openAngle;

    while (elapsedTime < 1f)
    {
        elapsedTime += Time.deltaTime * openSpeed;
        float currentAngle = Mathf.Lerp(startAngle, targetAngle, elapsedTime);
    
        doorTransform.rotation = Quaternion.Euler(closedRotation);
        doorTransform.RotateAround(pivotPoint, Vector3.up, currentAngle);
    
        yield return null;
    }

    doorTransform.rotation = Quaternion.Euler(closedRotation);
    doorTransform.RotateAround(pivotPoint, Vector3.up, targetAngle);
        
        doorTransform.eulerAngles = openRotation;
        isOpen = true;
        isOpening = false;
        
        EnableDoorBarrier(false);
    }
    
    void EnableDoorBarrier(bool enabled)
    {
        if (doorBarrier != null)
        {
            doorBarrier.SetActive(enabled);
            Debug.Log($"Door barrier is now: {(enabled ? "ACTIVE (blocking)" : "INACTIVE (open)")}");
        }
        else
        {
            Debug.LogError("Door barrier is not assigned! Create a cube, remove its renderer, and assign it to Door Barrier field.");
        }
    }
    
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Something entered door trigger: {other.name} with tag: {other.tag}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door trigger area!");
            playerInRange = true;
        }
        else
        {
            Debug.Log($"Not a player - tag is '{other.tag}', expected 'Player'");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        Debug.Log($"Something left door trigger: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left door trigger area!");
            playerInRange = false;
        }
    }
}