using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Rigidbody2D player;
    public BoxCollider2D playerCollider;
    public int speed, jumpPower;
    public GameObject bullet;
    
    public float shootDelay;
    float lastTimeShot;
    
    private bool _grounded, _collidedWithRightWall, _collidedWithLeftWall, _collidedWithRoof;
    private Dictionary<GameObject, CollisionDirection> collidingGround = new Dictionary<GameObject, CollisionDirection>();

    void Start()
    {
        _grounded = false;
        _collidedWithRightWall = false;
        _collidedWithLeftWall = false;
        _collidedWithRoof = false;
        lastTimeShot = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Vector2 dir = collision.collider.ClosestPoint(collision.contacts[0].point) - playerCollider.ClosestPoint(collision.contacts[0].point);
                dir = dir.normalized;
                if (dir == Vector2.up)
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Roof);
                    _collidedWithRoof = true;
                } else if (dir == Vector2.right)
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Left);
                    _collidedWithLeftWall = true;
                    player.velocity = new Vector2(0, player.velocity.y);
                }else if (dir == Vector2.left)
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Right);
                    _collidedWithRightWall = true;
                    player.velocity = new Vector2(0, player.velocity.y);
                }
                else
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Ground);
                    if (collision.collider.bounds.max.y > playerCollider.bounds.min.y)
                    {
                        player.position = new Vector2(player.position.x, collision.collider.bounds.max.y);
                    }
                    _grounded = true;
                }
            }
    }

    private void Shoot()
    {
        if((Time.time - lastTimeShot) > shootDelay)
        {
            lastTimeShot = Time.time;
            GameObject bulletInstance = Instantiate(bullet, player.transform.position, bullet.transform.rotation);
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), bulletInstance.GetComponent<Collider2D>());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CollisionDirection direction = collidingGround[collision.gameObject];
            switch (direction)
            {
                case CollisionDirection.Ground:
                {
                    _grounded = false;
                    break;
                }
                case CollisionDirection.Roof:
                {
                    _collidedWithRoof = false;
                    break;
                }
                case CollisionDirection.Left:
                {
                    _collidedWithLeftWall = false;
                    break;
                }
                case CollisionDirection.Right:
                {
                    _collidedWithRightWall = false;
                    break;
                }
            }

            collidingGround.Remove(collision.gameObject);
        }
    }
    
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(0, 0);
        if ((!_collidedWithRightWall && Input.GetAxis("Horizontal") < 0) || 
            (!_collidedWithLeftWall && Input.GetAxis("Horizontal") > 0))
        {
            movement += new Vector2(Time.deltaTime * speed * Input.GetAxis("Horizontal"), 0);
            transform.Translate(movement);
        }
    }
}
