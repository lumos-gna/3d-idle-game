using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private StatHandler statHandler;

    private Image _barImage;
    private Camera _camera;

    private Stat _currentHealth;
    private Stat _maxHealth;

    private void Awake()
    {
        _barImage = GetComponent<Image>();
    }

    private void Start()
    {
        _camera = Camera.main;

        if (statHandler.TryGetStat(StatType.CurHealth, out Stat currentHealth))
        {
            _currentHealth = currentHealth;

            _currentHealth.OnStatModified += UpdateBar;
        }

        if (statHandler.TryGetStat(StatType.MaxHealth, out Stat maxHealth))
        {
            _maxHealth = maxHealth;
        }
    }

    private void OnDestroy()
    {
        _currentHealth.OnStatModified -= UpdateBar;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + (_camera.transform.rotation * Vector3.forward));
    }

    private void UpdateBar()
    {
        _barImage.fillAmount = _currentHealth.value / _maxHealth.value;
    }
}
