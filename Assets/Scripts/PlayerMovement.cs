using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 50.0f;

    public Transform vrCamera; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 inputLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 inputRight = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        
        Vector3 moveDirection = vrCamera.forward * inputLeft.y + vrCamera.right * inputLeft.x;
        moveDirection.y = 0; 
        moveDirection.Normalize();

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        
        
        float yRotation = inputRight.x * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, yRotation, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}