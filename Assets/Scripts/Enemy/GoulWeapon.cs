using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulWeapon : MonoBehaviour
{
    
    [SerializeField] private float attackDamage = 50f;
    [SerializeField] private float enragedAttackDamage = 150f;
    [SerializeField] private float attackRange = 0.92f;
    [SerializeField] private float pushForce = 15f;
   
    [SerializeField] private float pushForceSmall = 7f; // Small push force for regular enemy hit
    

    public Vector3 attackOffset;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerLife>().GetDamage(attackDamage, "GetDamage");
            colInfo.attachedRigidbody.velocity = new Vector2(colInfo.GetComponent<PlayerControllerChat>().m_FacingRight ?  -pushForceSmall : pushForceSmall, colInfo.transform.position.y);
        }
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerLife>().GetDamage(enragedAttackDamage, "GetFullDamage");
            colInfo.attachedRigidbody.velocity = new Vector2(colInfo.GetComponent<PlayerControllerChat>().m_FacingRight ?  -pushForce : pushForce, colInfo.transform.position.y);
        }
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);

    }

    
}
