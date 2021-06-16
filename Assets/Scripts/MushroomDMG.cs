using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDMG : MonoBehaviour
{
    Animator animator;
    Transform player;
    public float distance = 5;
    public int dmg;
    public int health;
    public Vector3 attackOffset;
    public float attackRange;
    public LayerMask attackMask;
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D collinfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (collinfo != null && collinfo.gameObject.CompareTag("Player"))
        {
            collinfo.gameObject.GetComponent<Health>().TakeDamage(dmg);
        }
    }
    public void TakeDamage(int damage)
    {
        if (damage >= health)
        {
            health = 0;
            animator.SetTrigger("death");
        }
        else
        {
            health -= damage;
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position) <= distance)
        {
            animator.SetBool("canWalk", true);
        }
    }
}
