using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;

public class AutoBindToWhenSelect : MonoBehaviour
{   
    private Transform colorCanvasTransform;
    void Start(){
        
        InteractableUnityEventWrapper interactableUnityEventWrapper = GetComponent<InteractableUnityEventWrapper>();
        
        ActiveSelf activeSelf = colorCanvasTransform.GetComponent<ActiveSelf>();
        UnityAction action = new UnityAction(activeSelf.ActivateGameObject);
        interactableUnityEventWrapper.WhenSelect.AddListener(action);
    }

    void Update(){
        
        if(colorCanvasTransform == null){
            colorCanvasTransform = transform.Find("ColorCanvas(Clone)");
        }
    }

}