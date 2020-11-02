﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyControl : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public float speed = 3.0f;
    public int maxHealth = 5;
    int currentHealth = 0;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;

    public int health { get { return currentHealth; } }

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
          horizontal = Input.GetAxis("Horizontal");
          vertical = Input.GetAxis("Vertical");

          Vector2 move = new Vector2(horizontal,vertical);
          
          if(!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y, 0.0f))
          {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
          }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f,
                lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                string name = hit.collider.gameObject.name;
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject + " " + name);
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        /*   Debug.Log(horizontal +    "=>" + "32312");
             Vector2 position = transform.position;
             position.x = position.x + 3.1f * horizontal * Time.deltaTime; ;
             position.y = position.y + 3.1f * vertical * Time.deltaTime; ;
             transform.position = position;*/
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount,0,maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        Debug.Log(currentHealth);
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject gameObject = Instantiate(projectilePrefab,
            rigidbody2d.position + Vector2.up * 0.5f,
            Quaternion.identity
        );

        Projectiles projectile = gameObject.GetComponent<Projectiles>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");

    }


    public void PlayerSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
