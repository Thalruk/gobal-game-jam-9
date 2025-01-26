using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBubble : Bubble
{
    [SerializeField] GameObject stoneParryPrefab;
    public override void Init()
    {
        type = 1;
        ammoCost = 2;
        damage = 2;
        hp = 4;
        speed = 3;
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
        base.ChargedAttack(enemy);
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

    protected override void ShieldParry(GameObject spike)
    {
        base.ShieldParry(spike);
        GameObject stoneParry = Instantiate(stoneParryPrefab, transform.position, Quaternion.identity);
        stoneParry.transform.localScale = transform.localScale;

    }

    private void Start()
    {
       
    }
    private void Update()
    {
        
    }
}
