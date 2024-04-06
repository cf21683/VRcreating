using System.Collections;
using Oculus.Interaction;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Surfaces;

public class Transformable : MonoBehaviour
{
    
    void Start()
    {   
    }

    
    void Update()
    {
        
    }
    
    public void ActivateTransformable()  // create the following attributes to the object
    {
        GameObject[] transformableObjects = GameObject.FindGameObjectsWithTag("Transformable");
        foreach (GameObject obj in transformableObjects)
        {
            foreach (Transform child in obj.transform)
            {   
                // add rigidbody component
                Rigidbody rigidbody = child.GetComponent<Rigidbody>();
                if (rigidbody == null)
                { 
                    rigidbody = child.gameObject.AddComponent<Rigidbody>();
                }
                
                //add the collider component
                Collider collider = child.GetComponent<Collider>();
                if (collider == null)
                {
                    collider = child.gameObject.AddComponent<BoxCollider>(); 
                }
                
                //add the two grab script to obejct
                TwoGrabFreeTransformer twoGrabFreeTransformer = child.GetComponent<TwoGrabFreeTransformer>();
                if (twoGrabFreeTransformer == null)
                {
                    twoGrabFreeTransformer = child.gameObject.AddComponent<TwoGrabFreeTransformer>();
                }
                
                //add the one grab script to object
                OneGrabFreeTransformer oneGrabFreeTransformer = child.GetComponent<OneGrabFreeTransformer>();
                if (oneGrabFreeTransformer == null)
                {
                    oneGrabFreeTransformer = child.gameObject.AddComponent<OneGrabFreeTransformer>();
                }
                
                // add the grabbable script to object
                Grabbable grabbable = child.GetComponent<Grabbable>();
                if (grabbable == null)
                {
                    grabbable = child.gameObject.AddComponent<Grabbable>();
                    grabbable.InjectOptionalOneGrabTransformer(oneGrabFreeTransformer);
                    grabbable.InjectOptionalTwoGrabTransformer(twoGrabFreeTransformer);
                }
                
                //add the physcics grabbable script to the object
                PhysicsGrabbable physicsGrabbable = child.GetComponent<PhysicsGrabbable>();
                if (physicsGrabbable == null)
                {
                    physicsGrabbable = child.gameObject.AddComponent<PhysicsGrabbable>();
                    physicsGrabbable.InjectRigidbody(rigidbody);
                    physicsGrabbable.InjectGrabbable(grabbable);
                }
                
                //add the grab interactable script to the object
                GrabInteractable grabInteractable = child.GetComponent<GrabInteractable>();
                if (grabInteractable == null)
                {
                    grabInteractable = child.gameObject.AddComponent<GrabInteractable>();
                    grabInteractable.InjectRigidbody(rigidbody);
                }
                
                //add the hand grab script to the obecjt
                HandGrabInteractable handGrabInteractable = child.GetComponent<HandGrabInteractable>();
                if (handGrabInteractable == null)
                {
                    handGrabInteractable = child.gameObject.AddComponent<HandGrabInteractable>();
                    handGrabInteractable.InjectRigidbody(rigidbody);
                }
                
                //add the collider surface script to the object
                ColliderSurface colliderSurface = child.GetComponent<ColliderSurface>();
                if(colliderSurface == null){
                    colliderSurface = child.gameObject.AddComponent<ColliderSurface>();
                    colliderSurface.InjectCollider(collider);
                }

                //add the ray interactable script to the object
                RayInteractable rayInteractable = child.GetComponent<RayInteractable>();
                if(rayInteractable == null){
                    rayInteractable = child.gameObject.AddComponent<RayInteractable>();
                    rayInteractable.InjectSurface(colliderSurface);
                }

                //add the interactable unity event wrapper script to the object
                InteractableUnityEventWrapper interactableUnityEventWrapper = child.GetComponent<InteractableUnityEventWrapper>();
                if(interactableUnityEventWrapper == null){
                    interactableUnityEventWrapper = child.gameObject.AddComponent<InteractableUnityEventWrapper>();
                    interactableUnityEventWrapper.InjectInteractableView(rayInteractable);
                }

                //add the create canvas for object script to the object
                CreateCanvasForObject createCanvasForObject = child.GetComponent<CreateCanvasForObject>();
                if(createCanvasForObject == null){
                    createCanvasForObject = child.gameObject.AddComponent<CreateCanvasForObject>();
                }
                // Avoid the createcanvasforobject script from repeatedly generating canvas
                if(createCanvasForObject != null){
                    DestroyImmediate(createCanvasForObject);
                }
            }
        }
    }

    public void DeleteTransformable()
    {
        GameObject[] transformableObjects = GameObject.FindGameObjectsWithTag("Transformable");
        foreach (GameObject obj in transformableObjects)
        {
            foreach (Transform child in obj.transform)
            {
                Rigidbody rigidbody = child.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    DestroyImmediate(rigidbody);
                }
                
                TwoGrabFreeTransformer twoGrabFreeTransformer = child.GetComponent<TwoGrabFreeTransformer>();
                if (twoGrabFreeTransformer != null)
                {
                    DestroyImmediate(twoGrabFreeTransformer);
                }
                
                OneGrabFreeTransformer oneGrabFreeTransformer = child.GetComponent<OneGrabFreeTransformer>();
                if (oneGrabFreeTransformer != null)
                {
                    DestroyImmediate(oneGrabFreeTransformer);
                }
                
                Grabbable grabbable = child.GetComponent<Grabbable>();
                if (grabbable != null)
                {
                    DestroyImmediate(grabbable);
                }
                
                PhysicsGrabbable physicsGrabbable = child.GetComponent<PhysicsGrabbable>();
                if (physicsGrabbable != null)
                {
                    DestroyImmediate(physicsGrabbable);
                }
                
                GrabInteractable grabInteractable = child.GetComponent<GrabInteractable>();
                if (grabInteractable != null)
                {
                    DestroyImmediate(grabInteractable);
                }
                
                
                HandGrabInteractable handGrabInteractable = child.GetComponent<HandGrabInteractable>();
                if (handGrabInteractable != null)
                {
                    DestroyImmediate(handGrabInteractable);
                }

                ColliderSurface colliderSurface = child.GetComponent<ColliderSurface>();
                if (colliderSurface != null)
                {
                    DestroyImmediate(colliderSurface);
                }

                RayInteractable rayInteractable = child.GetComponent<RayInteractable>();
                if(rayInteractable != null){
                    DestroyImmediate(rayInteractable);
                }

                InteractableUnityEventWrapper interactableUnityEventWrapper = child.GetComponent<InteractableUnityEventWrapper>();
                if(interactableUnityEventWrapper != null){
                    DestroyImmediate(interactableUnityEventWrapper);
                }
                
                CreateCanvasForObject createCanvasForObject = child.GetComponent<CreateCanvasForObject>();
                if(createCanvasForObject != null){
                    DestroyImmediate(createCanvasForObject);
                }

                Transform colorCanvasTransform = child.Find("ColorCanvas(Clone)");
                 if (colorCanvasTransform != null)
                {
                    DestroyImmediate(colorCanvasTransform.gameObject);
                }

            }
        }
    }
}
