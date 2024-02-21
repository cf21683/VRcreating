using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotationSpeed = 100.0f;
    
    void Update()
    {
        
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        moveDirection = transform.TransformDirection(moveDirection);
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        float rotation = input.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }
}    