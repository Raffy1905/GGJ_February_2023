using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Rigidbody2D bullet;
    protected Vector2 direction;
    public float bulletSpeed;
    private readonly float lifespan = 3;
    
    protected abstract void OnWallCollision();
    protected abstract void OnEnemyCollision();
    protected abstract void Move();

    protected void Start()
    {
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        direction = (worldMousePosition - (Vector2)this.transform.position).normalized;
        Destroy(this.gameObject, lifespan);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            OnWallCollision();
        }
        //if (collision.collider.CompareTag("Enemy"))
        //{
        //    OnEnemyCollision();
        //}
    }

    void FixedUpdate()
    {
        Move();
    }
}
