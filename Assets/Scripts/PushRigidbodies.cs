using UnityEngine;

public class PushRigidbodies : MonoBehaviour
{
    [Header("Push Settings")]
    public float pushSpeed = 3.0f;
    public float pushDistance = 0.1f;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.collider.CompareTag("Boxes"))
            return;
            
        if (hit.moveDirection.y < -0.3f) 
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z).normalized;
        
        Vector3 newPosition = hit.collider.transform.position + pushDir * pushDistance;
        
        Bounds boxBounds = hit.collider.bounds;
        if (!Physics.CheckBox(newPosition + boxBounds.center - hit.collider.transform.position, 
                             boxBounds.extents * 0.9f, 
                             hit.collider.transform.rotation,
                             ~(1 << hit.collider.gameObject.layer)))
        {
            hit.collider.transform.position = Vector3.MoveTowards(
                hit.collider.transform.position, 
                newPosition, 
                pushSpeed * Time.deltaTime
            );
        }
    }
}