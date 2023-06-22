using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goul : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;
    public float seenRange = 10f;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if (Vector2.Distance(player.position, rb.position) <= seenRange)
        {
            animator.SetTrigger("PlayerSeeGopnik");
        }
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
     

}
