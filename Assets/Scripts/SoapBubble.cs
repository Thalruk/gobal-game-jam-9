using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : Bubble
{
    protected override void Init()
    {
        type = 0;
        ammoCost = 1;
        damage = 1;
        hp = 1;
        speed = 4;
        windSensitivity = 1f;
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
        FlyAway(enemy);

        Destroy(gameObject);
    }

    protected void FlyAway(Enemy enemy)
    {
        Vector2 enemyStartPosition = enemy.transform.position;
        Vector2 enemyFinalPosition = enemy.transform.position + new Vector2(0f, 1f);

        enemy.transform.position = Vector3.Lerp(enemyStartPosition, enemyFinalPosition, speed * Time.deltaTime);
        if (enemy.transform.position == enemyFinalPosition)
        {
            Destroy(gameObject);
        }
    }
}
