using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform attackPointUlta;
    [SerializeField] private Transform attackPointHead;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ultaExpPrefab;
    [SerializeField] private CircleCollider2D headColl;
    private PlayerControllerChat player;
    private PlayerLife life;


    [SerializeField] private float attackRangeUlta = 1.89f;
    [SerializeField] private float attackRangeHead = 0.25f;
    [SerializeField] private float attackRange = 0.23f;
    [SerializeField] private float pushForceSmall = 4f;
    [SerializeField] public float borzotaMax = 300f;
    [SerializeField] public float borzota = 0; // current borzota
    [SerializeField] private int hitCounter; // counter of hits for refill PIVCO
    [SerializeField] private int currentPivo; // index for pivko Game Object

    [SerializeField] private PivoBar[] pivoBarScript; // pivko referens script

    private int smallDamage = 30;
    private int ultDamage = 200;

    public float borzotaIncr = 20;
    public float borzotaDecr = 100;

    private float attackHeadVel = 10f;

    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    private void Awake()
    {
        player = GetComponent<PlayerControllerChat>();
        life = GetComponent<PlayerLife>();
        borzota = borzotaMax;
        foreach (PivoBar pivoBarScript in pivoBarScript)
            pivoBarScript.SetPivoImage(PivoBar.PivoStatus.Full);

    }
    // Update is called once per frame
    void Update()
    {
        UpdateCurrentPivko(borzota);

        if (Time.time >= nextAttackTime && !life.isDead)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                animator.SetTrigger("AttackSmall");
                // Cooldown for small attack
                nextAttackTime = Time.time + 1f / attackRate;

            }


        }

        AttackHead();

        if (Input.GetButtonDown("Fire3") && borzota >= borzotaDecr && !life.isDead)
        {

            animator.SetTrigger("Ulta");
            pivoBarScript[currentPivo].SetPivoImage(PivoBar.PivoStatus.Empty);
            borzota -= borzotaDecr;
        }


    }
    //  Set at animation "Attack"
    void Attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<GoulHealth>().TakeDamage(smallDamage);

            //Push enemy when hit
            enemy.attachedRigidbody.velocity = new Vector2(enemy.GetComponent<Goul>().isFlipped ? -pushForceSmall : pushForceSmall, enemy.transform.position.y);

            // Fill Pivko
            if (borzota < borzotaMax)
            {
                borzota += borzotaIncr;
                hitCounter++;
                if (hitCounter == 6)
                    hitCounter = 0;
                pivoBarScript[currentPivo].SetPivoImage((PivoBar.PivoStatus)hitCounter);
            }
        }


    }

    void AttackHead()
    {
        if (Input.GetButtonDown("Fire2") && borzota >= borzotaDecr && !life.isDead)
        {
            pivoBarScript[currentPivo].SetPivoImage(PivoBar.PivoStatus.Empty);
            borzota -= borzotaDecr;
            animator.SetTrigger("AttackHead");

        }

    }

    // Ulta set in animation Ulta 
    void Ulta()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUlta.position, attackRangeUlta, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Instantiate(ultaExpPrefab, enemy.transform.position, attackPointUlta.rotation);
            enemy.GetComponent<GoulHealth>().TakeDamage(ultDamage);

        }

    }



    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPointUlta.position, attackRangeUlta);
    }

    //Dush movement for head attack (set in animation)
    public void Dush()
    {
        rb.velocity = new Vector2(player.m_FacingRight ? attackHeadVel : -attackHeadVel, rb.velocity.y);

    }

    // Update current pivko Index 
    private void UpdateCurrentPivko(float currBorzota)
    {

        if (currBorzota >= 200)
        {
            currentPivo = 0;
        }
        else if (currBorzota > 100)
        {
            currentPivo = 1;
        }
        else
        {
            currentPivo = 2;
        }

    }
}
