using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBullet : Bullet
{
    protected override void Move()
    {
        transform.Translate(Time.deltaTime * bulletSpeed * direction);
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
