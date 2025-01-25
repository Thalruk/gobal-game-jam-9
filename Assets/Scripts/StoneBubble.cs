using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBubble : Bubble
{
    public override void Init()
    {
        type = 1;
        ammoCost = 2;
        damage = 2;
        hp = 4;
        speed = 1;
        windSensitivity = 0.1f;
        base.Init();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void NormalAttack(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetDamage(damage);

        Destroy(gameObject);
    }
    protected override void ChargedAttack(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetDamage(damage);
        enemy.GetComponent<Enemy>().PushBack(GetComponent<Rigidbody2D>().velocity);

        Destroy(gameObject);
    }

    protected void PushBack(GameObject enemy)
    {
        
        Vector2 enemyStartPosition = enemy.transform.position;
        Vector2 pushDirection = GetComponent<Rigidbody>().velocity.normalized;
        Vector2 enemyFinalPosition = (Vector2)enemy.transform.position + pushDirection;

        enemy.transform.position = Vector3.Lerp(enemyStartPosition, enemyFinalPosition, Time.deltaTime);
        if ((Vector2)enemy.transform.position == enemyFinalPosition)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       
    }
    private void Update()
    {
        
    }
}
