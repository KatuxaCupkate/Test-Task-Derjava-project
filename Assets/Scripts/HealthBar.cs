using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private Image[] currentHealthBar;

    private int maxHealth = 300;
    private int segments = 3;
    private float segmentHealth;

    private void Start()
    {
        segmentHealth = maxHealth / segments;
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float currentHealth = playerLife.health;
        for (int i = 0; i < currentHealthBar.Length; i++)
        {
            float fillAmount = Mathf.Clamp01((currentHealth - segmentHealth * i) / segmentHealth);
            currentHealthBar[i].fillAmount = fillAmount;
        }
    }
}



