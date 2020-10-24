using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool  vertical;
    Rigidbody2D  rigidbody2d;

    public bool  broken = true;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer < 0){
            direction = -direction;
            timer = changeTime;
        }

        
    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;
        //position.x += position.x * speed * Time.deltaTime;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyControl player = other.gameObject.GetComponent<RubyControl>();
        if(player != null)
        {
            player.ChangeHealth(-1);
        }

        Debug.Log("OnCollisionEnter2D enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D enemy");
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
