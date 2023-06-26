using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SeenPointManager : MonoBehaviour
{
   [SerializeField] private Animator animator;
    [SerializeField] private CinemachineImpulseSource impulseSource;


    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerSeeGopnik");
            CameraShakeManager.instance.CameraShake(impulseSource);
            Destroy(gameObject);
        }

    }
   
}
