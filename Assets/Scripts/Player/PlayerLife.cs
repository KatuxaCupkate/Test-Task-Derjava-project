using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float health { get; private set; }
    [SerializeField] public bool isDead = false;
    private BoxCollider2D colliderPlayer;

    [SerializeField] private CinemachineImpulseSource  impulseSource;
   
    private Rigidbody2D rb;
    private Animator animator;
    public float deathDelay = 1.0f;

    [SerializeField] private AudioSource dethAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliderPlayer = GetComponent<BoxCollider2D>();
       
        health = 300f;
    }



    private void Die()
    {
        // dethAudioSource.Play();
        isDead = true;
        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero; 
        colliderPlayer.enabled = false;
        CameraShakeManager.instance.CameraShake(impulseSource);
     
    }

    
    public void GetDamage(float damage, string damageType)
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        health -= damage;

        animator.SetTrigger(damageType);

        if (health <= 0)
        {
            Die();
        }
    }

}
