using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 2.0f;
    public float rotationSpeed = 50.0f;

    public Transform vrCamera;
    void Update()
    {
        
        Vector2 inputLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 inputRight = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        
        Vector3 moveDirection = vrCamera.forward * inputLeft.y + vrCamera.right * inputLeft.x;
        moveDirection.y = 0; 
        moveDirection.Normalize(); 

        
        transform.Translate(moveDirection * movespeed * Time.deltaTime, Space.World);
        
        float rotationAmount = inputRight.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }
}    