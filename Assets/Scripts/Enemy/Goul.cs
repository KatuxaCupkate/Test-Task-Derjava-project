using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Goul : MonoBehaviour
{
    public Transform player;
    [SerializeField] private CinemachineImpulseSource impulseSource;
   
    public bool isFlipped = false;
   

    void Start()
    {
      
        player = GameObject.FindGameObjectWithTag("Player").transform;

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
