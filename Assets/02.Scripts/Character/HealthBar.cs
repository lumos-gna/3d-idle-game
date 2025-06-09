using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private StatHandler statHandler;

    private Image _barImage;
    private Camera _camera;

    private void Awake()
    {
        _barImage = GetComponent<Image>();
    }

    private void Start()
    {
        _camera = Camera.main;

        statHandler.OnStatModified += UpdateBar;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + (_camera.transform.rotation * Vector3.forward));
    }

    private void UpdateBar(Stat stat)
    {
        if (stat.type == StatType.Health)
        {
            _barImage.fillAmount = stat.curValue / stat.maxValue;
        }
    }
}
