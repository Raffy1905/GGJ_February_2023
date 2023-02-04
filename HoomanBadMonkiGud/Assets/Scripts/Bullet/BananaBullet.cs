using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBullet : Bullet
{

    Vector2 originalPosition;
    public float backSpeed;


    private new void Start()
    {
        base.Start();
        originalPosition = transform.position;
    }

    protected override void Move()
    {
        transform.Translate(Time.deltaTime * bulletSpeed * direction);
        Vector2 vectorBack = (originalPosition - (Vector2)transform.position).normalized * backSpeed;
        bullet.AddForce(vectorBack);
    }

    protected override void OnEnemyCollision()
    {
        Destroy(this.gameObject);
    }

    protected override void OnWallCollision()
    {
        Destroy(this.gameObject);
    }
}
