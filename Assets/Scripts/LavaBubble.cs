using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBubble : Bubble{

    [SerializeField] protected GameObject lavaSpillPrefab;
    protected override void Init()
    {
        type = 3;
        ammoCost = 4;
        damage = 4;
        hp = 3;
        speed = 2;
        windSensitivity = 0.3f;
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
        SpillOnFloor();

        Destroy(gameObject);
    }

    protected void SpillOnFloor() // spill lava on floor witch deal damage over time
    {
        GameObject lavaSpill = Instantiate(lavaSpillPrefab);
    }
}
