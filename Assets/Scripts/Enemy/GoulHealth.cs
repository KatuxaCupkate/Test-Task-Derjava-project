using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulHealth : MonoBehaviour
{
    [SerializeField] private int health = 500;

    [SerializeField] private GameObject deathEffect;
    [SerializeField] GameObject goulPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform spawnPos;

    
    public bool getDamage;

    public void TakeDamage(int damage)
    {
        GetComponent<Animator>().SetTrigger("GetHert");
        health -= damage;
        getDamage = true;
       
        if (health <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Invoke("SpawnNewGoul", 2);
        gameObject.SetActive(false);
    }

    private void SpawnNewGoul()
    {
        Instantiate(goulPrefab, spawnPos.position, spawnPos.rotation);
        Destroy(gameObject);
    }
}
