using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool fly = false, pushBack = false;
    float timeFly = 0f, timePush = 0f;
    Vector2 startPosition, endPosition, pushVector;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        pushVector = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            rigidbody2D.gravityScale = 0f;
            //transform.position = new Vector2(transform.position.x + pushVector.x, transform.position.y + pushVector.y);
            //pushVector.y = Mathf.Lerp(0.01f, 0f, timeFly);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Lerp(10f, 0f, timeFly));
            timeFly += Time.deltaTime;
            if (rigidbody2D.velocity.y == 0f)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                fly = false;
                pushVector.y = 0f;
            }
        }
        if (pushBack)
        {
            //transform.position = new Vector2 (transform.position.x + pushVector.x, transform.position.y + pushVector.y);
            //pushVector.x = Mathf.Lerp(0.5f, 0f, timePush);
            timePush += Time.deltaTime;
            if (rigidbody2D.velocity.x == 0f)
            {
                pushBack = false;
                pushVector.x = 0f;
            }
        }
    }

    public void GetDamage(int damage) { }
    public void FlyAway()
    {
        timeFly = 0f;
        pushVector.y += 10f;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, pushVector.y));

        fly = true;
    }

    public void PushBack(Vector2 _pushVector)
    {
        timePush = 0f;
        pushVector.x += _pushVector.x * 200f;
        startPosition = transform.position;
        endPosition = startPosition + pushVector;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(pushVector.x, 0f));

        pushBack = true;
        //print(pushVector);
    }
}
