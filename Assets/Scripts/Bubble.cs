using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    protected int type; // bubble type: 0 - soap, 1 - stone, 2 - glass, 3 - lava
    protected int ammoCost; // 1, 2, 3, 4
    protected int damage; // 1, 2, 3, 4
    protected int hp; // 1, 4, 2, 3
    protected int speed; //4, 1, 3, 2
    protected float windSensitivity; // how much is bubble pushed away or slowed by wind
    protected bool charged;

    protected virtual void Init() {}

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        if(collision.gameObject.tag == "enemy")
        {
            GameObject enemy = collision.gameObject;
            // call funtion on hitted enemy
            if (!charged) // special attack
            {
                print("Charged attack");
                ChargedAttack(enemy);
            }
            else // normal attack
            {
                print("Normal attack");
                NormalAttack(enemy);
            }
        }
    }

    protected virtual void ChargedAttack(GameObject enemy) { }
    protected virtual void NormalAttack(GameObject enemy) { }
}
