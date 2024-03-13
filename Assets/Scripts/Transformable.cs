using System.Collections;
using Oculus.Interaction;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class Transformable : MonoBehaviour
{
    
    void Start()
    {   
    }

    
    void Update()
    {
        
    }
    
    public void ActivateTransformable()
    {
        GameObject[] transformableObjects = GameObject.FindGameObjectsWithTag("Transformable");
        foreach (GameObject obj in transformableObjects)
        {
            foreach (Transform child in obj.transform)
            {   
                Rigidbody rigidbody = child.GetComponent<Rigidbody>();
                if (rigidbody == null)
                { 
                    rigidbody = child.gameObject.AddComponent<Rigidbody>();
                }
                
                Collider collider = child.GetComponent<Collider>();
                if (collider == null)
                {
                    collider = child.gameObject.AddComponent<BoxCollider>(); 
                }
                
                
                TwoGrabFreeTransformer twoGrabFreeTransformer = child.GetComponent<TwoGrabFreeTransformer>();
                if (twoGrabFreeTransformer == null)
                {
                    twoGrabFreeTransformer = child.gameObject.AddComponent<TwoGrabFreeTransformer>();
                }
                
                OneGrabFreeTransformer oneGrabFreeTransformer = child.GetComponent<OneGrabFreeTransformer>();
                if (oneGrabFreeTransformer == null)
                {
                    oneGrabFreeTransformer = child.gameObject.AddComponent<OneGrabFreeTransformer>();
                }
                
                Grabbable grabbable = child.GetComponent<Grabbable>();
                if (grabbable == null)
                {
                    grabbable = child.gameObject.AddComponent<Grabbable>();
                    grabbable.InjectOptionalOneGrabTransformer(oneGrabFreeTransformer);
                    grabbable.InjectOptionalTwoGrabTransformer(twoGrabFreeTransformer);
                }
                
                PhysicsGrabbable physicsGrabbable = child.GetComponent<PhysicsGrabbable>();
                if (physicsGrabbable == null)
                {
                    physicsGrabbable = child.gameObject.AddComponent<PhysicsGrabbable>();
                    physicsGrabbable.InjectRigidbody(rigidbody);
                    physicsGrabbable.InjectGrabbable(grabbable);
                }
                
                GrabInteractable grabInteractable = child.GetComponent<GrabInteractable>();
                if (grabInteractable == null)
                {
                    grabInteractable = child.gameObject.AddComponent<GrabInteractable>();
                    grabInteractable.InjectRigidbody(rigidbody);
                }
                
                HandGrabInteractable handGrabInteractable = child.GetComponent<HandGrabInteractable>();
                if (handGrabInteractable == null)
                {
                    handGrabInteractable = child.gameObject.AddComponent<HandGrabInteractable>();
                    handGrabInteractable.InjectRigidbody(rigidbody);
                }
            }
        }
    }
}
