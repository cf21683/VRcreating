using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class doorInteraction : MonoBehaviour
{
    public Transform playerTransform;
    public Image doorLoading;
    private bool isPressing = false;
    
    public GameObject inquiryPanel;
    public GameObject blackScreenPanel;
    
    void Start()
    {   
        // if (blackScreenPanel != null)
        // {
        //     blackScreenPanel.SetActive(false);
        // }
        // if (inquiryPanel != null)
        // {
        //     inquiryPanel.SetActive(false);
        // }
    }

    void Update()
    {
        
        if (!playerTransform) 
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject)
            {
                playerTransform = playerGameObject.transform;
            }
        }
        
        if (inquiryPanel.activeSelf && playerTransform != null)
        {
            
            float distanceInFrontOfPlayer = 2.0f;
            Vector3 panelPosition = playerTransform.position + playerTransform.forward * distanceInFrontOfPlayer;
        
            
            float heightOffset = 0.0f; 
            panelPosition.y += heightOffset;
        
            
            inquiryPanel.transform.position = panelPosition;
        
            
            inquiryPanel.transform.LookAt(playerTransform);
            
            inquiryPanel.transform.Rotate(0, 180f, 0);
        }
        
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                isPressing = true;
            }

            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                isPressing = false;
            }

            if (isPressing)
            {
                    isPressing = false;
                    inquiryPanel.SetActive(true);
                    generateRoom();
            }
        
        
    }
    
    public void ConfrimButton()
    {
        inquiryPanel.SetActive(false);
        blackScreenPanel.SetActive(true);
        doorLoading.gameObject.SetActive(true);
        generateRoom();
    }

    public void CancelButton()
    {
        inquiryPanel.SetActive(false);
        blackScreenPanel.SetActive(true);
        generateRoom();
    }
    
    void generateRoom()
    {
        Debug.Log("Room generated");
    }
}