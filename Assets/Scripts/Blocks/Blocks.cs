using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blocks : MonoBehaviour
{

    protected int type;

    protected virtual void Init() { }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bubble")
        {
            print(type + " " + collision.gameObject.GetComponent<Bubble>().type);
            if(collision.gameObject.GetComponent<Bubble>().type == type)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
