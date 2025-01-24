using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MoveSpike : MonoBehaviour
{
    private Vector2 finalPosition;
    private Vector2 startPosition;
    float time = 0f;
    void Start()
    {
        startPosition = transform.position;
        finalPosition = startPosition + (Vector2)transform.up * 3f;
        print(transform.forward);
    }

    void Update()
    {
        transform.position = Vector2.Lerp(startPosition, finalPosition, time);
        time += Time.deltaTime;
        if ((Vector2)transform.position == finalPosition)
            Destroy(gameObject);
    }
}
