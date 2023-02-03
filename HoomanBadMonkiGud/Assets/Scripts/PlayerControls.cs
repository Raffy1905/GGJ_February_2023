using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Rigidbody2D player;
    public int speed, jumpPower;

    bool grounded;

    void Start()
    {
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            player.velocity = new Vector2(player.velocity.x, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Time.deltaTime * speed * Input.GetAxis("Horizontal"), 0);
        if(Input.GetButtonDown("Jump") && grounded)
        {
            player.AddForce(Vector2.up * jumpPower);
        }


        transform.Translate(movement);
    }
}
