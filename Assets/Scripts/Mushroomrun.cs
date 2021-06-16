using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroomrun : StateMachineBehaviour
{
    public float distance;
    public float speed = 2f;
    public float attackRange = 3f;

    Transform player;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= distance)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else {
            animator.SetBool("canWalk", false);
        }
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        if (player.position.x > rb.position.x)
        {
            rb.transform.localScale = new Vector3(-Mathf.Abs(rb.transform.localScale.x), rb.transform.localScale.y, rb.transform.localScale.z);
        }
        else {
            rb.transform.localScale = new Vector3(Mathf.Abs(rb.transform.localScale.x), rb.transform.localScale.y, rb.transform.localScale.z);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
