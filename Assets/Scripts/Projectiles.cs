using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    Rigidbody2D rigidbody2d;


    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Debug.Log("awake" +"  " + rigidbody2d +" " + this);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 100.0f){
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force) {
        Debug.Log("Launch" + " " + rigidbody2d + " " + this);
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Projectile Collision with " + other.gameObject);
        EnemyController e = other.gameObject.
            GetComponent<EnemyController>();
        if(e != null)
        {
            e.Fix();
        }

        Destroy(gameObject);
    }
}
