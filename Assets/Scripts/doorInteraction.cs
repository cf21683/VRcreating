using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class doorInteraction : MonoBehaviour
{
    public Image doorLoading;
    private bool isPressing = false;
    private float holdTime = 2.0f;
    private float timer = 0;


    void Update()
    {   
        if (doorLoading == null)
        {
            Debug.LogError("doorLoading is not assigned!");
            return;
        }
        
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            isPressing = true;
            timer = 0;
        }

        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            isPressing = false;
            doorLoading.fillAmount = 0;
        }

        if (isPressing)
        {
            timer += Time.deltaTime;
            doorLoading.fillAmount = timer / holdTime;

            if (timer >= holdTime)
            {
                isPressing = false;
                doorLoading.fillAmount = 0;
                generateRoom();
            }
        }
    }

    void generateRoom()
    {
        Debug.Log("Room generated");
    }
}