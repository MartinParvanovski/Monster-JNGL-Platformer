using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public int health;
    public int dmg;
    public float speed;
    public float distance;
    public Transform groundDetection;
    private Vector3 startPosition;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) >= distance)
        {
            startPosition = transform.position;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }

        transform.Translate((transform.localScale.x < 0 ? Vector2.left : Vector2.right) * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && health > 0) {

            collision.gameObject.GetComponent<Health>().TakeDamage(dmg);
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
}
