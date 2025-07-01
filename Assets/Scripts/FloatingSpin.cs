using UnityEngine;

public class FloatingSpin : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatAmount = 0.5f;
    public float spinSpeed = 50f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = startPos + new Vector3(0, yOffset, 0);

        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }
}
