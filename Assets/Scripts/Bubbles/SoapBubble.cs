using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : Bubble
{
    public override void Init()
    {
        type = 0;
        ammoCost = 1;
        damage = 1;
        hp = 1;
        speed = 4;
        windSensitivity = 1f;
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
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        enemy.GetComponent<Enemy>().GetDamage(damage);
        enemy.GetComponent<Enemy>().FlyAway();

        speed = 0;
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.transform.position = enemy.transform.position;
        gameObject.transform.localScale = new Vector2(3f, 3f);
        gameObject.transform.SetParent(enemy.transform);
    }

    protected void FlyAway(GameObject enemy)
    {
        float time = 0f;
        print("Fly");
        Vector2 enemyStartPosition = enemy.transform.position;
        Vector2 enemyFinalPosition = enemyStartPosition + new Vector2(0f, 1f);

       
        enemy.transform.position = Vector3.Lerp(enemyStartPosition, enemyFinalPosition, time += Time.deltaTime);
        if ((Vector2)enemy.transform.position == enemyFinalPosition)
        {
            Destroy(gameObject);
        }
    }

    protected override void ShieldParry(GameObject spike)
    {
        base.ShieldParry(spike);
        //Player.Instance.ammo = Mathf.Clamp(Player.Instance.ammo + 5, 0, 10);
        Player.Instance.ChangeAmmo(5);
    }

    private void Start()
    {
       
    }
}
