using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    protected int type; // bubble type: 0 - soap, 1 - stone, 2 - glass, 3 - lava
    public int ammoCost; // 1, 2, 3, 4
    public int damage; // 1, 2, 3, 4
    public int hp; // 1, 4, 2, 3
    protected int speed; //4, 1, 3, 2
    protected float windSensitivity; // how much is bubble pushed away or slowed by wind
    public bool charged;
    protected float shieldTime;
    public bool isShield;
    public Vector2 vel;
    public bool boostAttack;

    public virtual void Init() {
        GetComponent<Rigidbody2D>().velocity *= speed;
        isShield = false;
        damage = boostAttack ? damage + 1 : damage;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        if(collision.gameObject.tag == "enemy")
        {
            GameObject enemy = collision.gameObject;
            
            // call funtion on hitted enemy
            if (charged) // special attack
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
        if(collision.gameObject.tag == "Spike")
        {
            GameObject spike = collision.gameObject;
            hp -= spike.GetComponent<Spike>().damage;
            if (isShield && shieldTime < 0.2f)
            {
                ShieldParry(spike);
                shieldTime = 0f;
                Destroy(spike);
            }
            else
            {
                Destroy(spike);
            }
            if (hp < 0)
            {
                Destroy(gameObject);
                if (isShield)
                {
                    Player.Instance.RemoveShield();
                    isShield = false;
                }

            }
        }
    }

    protected virtual void ChargedAttack(GameObject enemy) { }
    protected virtual void NormalAttack(GameObject enemy) { }
    protected virtual void ShieldParry(GameObject spike) {
        print("Parry");
        Player.Instance.RemoveShield();
        isShield = false;
        Destroy(gameObject);
        Destroy(spike);
    }
    public virtual void Shield() {
    
        if (!isShield)
        {
            isShield = true;
            shieldTime = 0f;
            gameObject.tag = "Shield";
            gameObject.transform.localScale = new Vector2(5f, 5f);
        }
        else
        {
            shieldTime += Time.deltaTime;
        }
    }
    public virtual void RemoveShield()
    {
        isShield = false;
        Player.Instance.RemoveShield();
    }
}
