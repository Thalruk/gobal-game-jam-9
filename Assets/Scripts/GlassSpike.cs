using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GlassSpike : MonoBehaviour
{
    private Vector2 finalPosition;
    private Vector2 startPosition;
    float time = 0f;
    public float longDist = 1f;
    void Start()
    {
        startPosition = transform.position;
        finalPosition = startPosition + (Vector2)transform.up * 3f * longDist;
    }

    void Update()
    {
        transform.position = Vector2.Lerp(startPosition, finalPosition, time);
        time += Time.deltaTime;
        if ((Vector2)transform.position == finalPosition)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(1);
            Destroy(gameObject);
        }
    }
}
