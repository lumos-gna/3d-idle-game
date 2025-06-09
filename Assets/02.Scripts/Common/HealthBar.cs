using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image barImage;
    
    private void Awake()
    {
        barImage = GetComponent<Image>();
    }

    private void Start()
    {
        var cameraRotX = Camera.main.transform.localEulerAngles.x;

        transform.localEulerAngles = new Vector3(cameraRotX, 0, 0); 
    }

    public void UpdateBar(float fillAmount) => barImage.fillAmount = fillAmount;
}
