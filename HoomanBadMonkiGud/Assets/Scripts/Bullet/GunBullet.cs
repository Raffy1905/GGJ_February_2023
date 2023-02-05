using UnityEngine;

public class GunBullet : Bullet
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
