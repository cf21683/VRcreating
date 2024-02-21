using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; 

    void Update()
    {
        
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}    