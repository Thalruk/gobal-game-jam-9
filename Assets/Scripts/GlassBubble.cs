using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBubble : Bubble
{
    protected int numberOfSpikes = 10;

    [SerializeField] protected GameObject glassSpikeslPrefab;
    protected override void Init()
    {
        type = 2;
        ammoCost = 3;
        damage = 3;
        hp = 2;
        speed = 3;
        windSensitivity = 0.5f;
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
        Explode();

        Destroy(gameObject);
    }

    protected void Explode() // create spikes around bubble that fly away and deal damage
    {
        for (int i = 0; i < numberOfSpikes; i++)
        {
            GameObject spike = Instantiate(glassSpikeslPrefab);
            spike.transform.rotation = Quaternion.Euler(0f, 0f, 360f / numberOfSpikes * i);
        }
    }
}
