using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private Goul goul;
    private Animator animator;

    private float attackCooldown=1f;
    public float speed = 240f;
    public float nextWayPointDistance = 3f;
    private float cooldownTimer = Mathf.Infinity;

    Path path;
    int currentWaypoint = 0;
    int hitCount = 0;
    bool reachEndOfPath = false;
    
    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        goul = GetComponent<Goul>();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }
    // Update is called once per frame
    void Update()
    {
        goul.LookAtPlayer();
        FollowTheTarget();

        cooldownTimer += Time.deltaTime;
        if (reachEndOfPath&&hitCount<2)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("Attack");
                hitCount++;
                
            }
        }
        else
        {
            animator.ResetTrigger("Attack");
            
        }

       if (reachEndOfPath&&hitCount>=2)
        {
            animator.SetTrigger("TwoHandAttack");
            hitCount = 0;
        }
       

    }

    void FollowTheTarget()
    {

        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }
        else
        {
            reachEndOfPath = false;
        }


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }


}
