using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Rigidbody2D player;
    public int speed, jumpPower;
    public GameObject bullet;

    public float shootDelay;
    float lastTimeShot;
    bool grounded;

    void Start()
    {
        grounded = false;
        lastTimeShot = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            player.velocity = new Vector2(player.velocity.x, 0);
        }
    }

    private void Shoot()
    {
        if((Time.time - lastTimeShot) > shootDelay)
        {
            lastTimeShot = Time.time;
            Instantiate(bullet, player.transform.position, bullet.transform.rotation);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Time.deltaTime * speed * Input.GetAxis("Horizontal"), 0);
        if(Input.GetButton("Jump")&& grounded)
        {
            player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        transform.Translate(movement);
        
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        } 
    }
}
