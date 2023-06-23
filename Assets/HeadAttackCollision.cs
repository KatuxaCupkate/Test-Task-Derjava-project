using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAttackCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Use collision for head Attack damage
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<GoulHealth>().TakeDamage(100);
            collision.rigidbody.velocity = new Vector2(collision.gameObject.GetComponent<Goul>().isFlipped ? -15 : 15, collision.transform.position.y);
           
        }
    }

}
