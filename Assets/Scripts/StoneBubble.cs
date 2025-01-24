using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBubble : Bubble
{
    protected override void Init()
    {
        type = 1;
        ammoCost = 2;
        damage = 2;
        hp = 4;
        speed = 1;
        windSensitivity = 0.1f;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    protected override void NormalAttack(Enemy enemy)
    {
        enemy.GetDamage(damage);

        Destroy(gameObject);
    }
    protected override void ChargedAttack(Enemy enemy)
    {
        enemy.GetDamage(damage);
        PushBack(enemy);

        Destroy(gameObject);
    }

    protected void PushBack(Enemy enemy)
    {
        Vector2 enemyStartPosition = enemy.transform.position;
        Vector2 pushDirection = GetComponent<Rigidbody>().velocity.normalized;
        Vector2 enemyFinalPosition = enemy.transform.position + pushDirection;

        enemy.transform.position = Vector3.Lerp(enemyStartPosition, enemyFinalPosition, Time.deltaTime);
        if (enemy.transform.position == enemyFinalPosition)
        {
            Destroy(gameObject);
        }
    }
}
