using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpill : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage = 1;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 9);
        Physics2D.IgnoreLayerCollision(6, 9);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }*/
}
