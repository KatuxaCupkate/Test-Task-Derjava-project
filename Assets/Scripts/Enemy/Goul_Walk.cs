using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Goul_Walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    private float speed = 1f;
    private float attackRange = 2f;
    private float twoHandAttack = 3f;
   
    bool playerIsDead;

    Goul goul;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        goul = animator.GetComponent<Goul>();
        playerIsDead = player.GetComponent<PlayerLife>().isDead;

    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        goul.LookAtPlayer();

        //follow the Player

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(player.position, rb.position) > attackRange && !playerIsDead)
        {
            rb.MovePosition(newPos);
        }

        // Attack player 

        if (Vector2.Distance(player.position, rb.position) <= attackRange && !playerIsDead)
        {
            animator.SetTrigger("Attack");
        }
        if (Vector2.Distance(player.position, rb.position) <= twoHandAttack && !playerIsDead)
        {
            animator.SetTrigger("TwoHandAttack");
        }
        if (playerIsDead)
            animator.SetTrigger("GoulStop");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");

        animator.ResetTrigger("TwoHandAttack");

    }


}
