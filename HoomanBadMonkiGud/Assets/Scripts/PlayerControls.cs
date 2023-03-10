using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{   
    public float shootDelay;
    float lastTimeShot;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private bool _ableToClimb;
    private bool _grounded, _collidedWithRightWall, _collidedWithLeftWall, _collidedWithRoof, _climbing, _climbingTopReached;
    private Dictionary<GameObject, CollisionDirection> collidingGround = new Dictionary<GameObject, CollisionDirection>();

    void Start()
    {
        _grounded = false;
        _collidedWithRightWall = false;
        _collidedWithLeftWall = false;
        _collidedWithRoof = false;
        _climbing = false;
        lastTimeShot = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Vector2 dir = collision.collider.ClosestPoint(collision.contacts[0].point) - Player.Instance.ownCollider.ClosestPoint(collision.contacts[0].point);
                dir = dir.normalized;
                if (dir == Vector2.up)
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Roof);
                    _collidedWithRoof = true;
                } else if (dir == Vector2.right)
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Left);
                    _collidedWithLeftWall = true;
                    Player.Instance.ownRigidbody.velocity = new Vector2(0, Player.Instance.ownRigidbody.velocity.y);
                }else if (dir == Vector2.left)
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Right);
                    _collidedWithRightWall = true;
                    Player.Instance.ownRigidbody.velocity = new Vector2(0, Player.Instance.ownRigidbody.velocity.y);
                }
                else
                {
                    collidingGround.Add(collision.gameObject, CollisionDirection.Ground);
                    _grounded = true;
                }
            }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Liane") && (Player.Instance.GetDevolutionState() == Player.DevolutionState.MONKE))
        {
            _ableToClimb = true;
        }
    }

    private void Shoot()
    {
        if((Time.time - lastTimeShot) > shootDelay)
        {
            Player.Instance.Shoot();

            lastTimeShot = Time.time;
            GameObject bulletInstance = Instantiate(Player.Instance.GetBullet(), Player.Instance.ownRigidbody.transform.position, Player.Instance.GetBullet().transform.rotation);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.bounds.max.y > Player.Instance.ownCollider.bounds.max.y) 
        {
            _climbingTopReached = false;
        }
        else
        {
            _climbingTopReached = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Liane"))
        {
            _ableToClimb = false;
            _climbing = false;
        }
    }

    private void Update()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("Attacking", false);
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attacking", true);
            Shoot();
        }
        if(Input.GetButtonDown("Jump"))
        {
            if (_grounded)
            {
                animator.SetBool("Jumping", true);
                Player.Instance.Jump();
                Player.Instance.ownRigidbody.AddForce(Vector2.up * Player.Instance.GetJumpPower(), ForceMode2D.Impulse);
            }
            if (_climbing && !_grounded)
            {
                _climbing = false;
                Player.Instance.ownRigidbody.gravityScale = 1;
                Player.Instance.ownRigidbody.AddForce(new Vector2(Player.Instance.GetJumpPower() * Input.GetAxis("Horizontal") / 4, 0), ForceMode2D.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        animator.SetInteger("state", Player.Instance.GetStateNumber());
        animator.SetFloat("Speed", 0);
        Vector2 movement = new Vector2(0, 0);
        if (((!_collidedWithRightWall && Input.GetAxis("Horizontal") < 0) || 
            (!_collidedWithLeftWall && Input.GetAxis("Horizontal") > 0)))
        {
            animator.SetFloat("Speed", 1);
            movement += new Vector2(Time.deltaTime * Player.Instance.GetWalkingSpeed() * Input.GetAxis("Horizontal"), 0);
        }

        spriteRenderer.flipX = Input.GetAxis("Horizontal") > 0;

        if ((_ableToClimb && Input.GetAxis("Vertical") != 0) || _climbing)
        {
            animator.SetFloat("Speed", 0);
            _climbing = true;
        }
        else
        {
            
            _climbing = false;
        }

        if (_climbing)
        {
            Player.Instance.ownRigidbody.gravityScale = 0;
            int axisFactor = Input.GetAxis("Vertical") != 0 ? (Input.GetAxis("Vertical") > 0 ? 1 : -1) : 0;
            movement = new Vector2(0, Time.deltaTime * Player.Instance.GetWalkingSpeed() / Player.Instance.climbingFactor * axisFactor);
            if ((movement.y < 0 && _grounded) || (_climbingTopReached && Input.GetAxis("Vertical") >= 0))
            {
                movement.y = 0;
            } 
        }
        else
        {
            Player.Instance.ownRigidbody.gravityScale = 1;
        }

        if (_grounded)
        {
            _climbing = false;
        }
        animator.SetBool("Climbing", _climbing);
        transform.Translate(movement);
    }
}
