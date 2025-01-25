using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneParry : MonoBehaviour
{
    Vector3 startScale;
    float time = 0f;
    private void Start()
    {
        startScale = transform.localScale;
        
    }
    private void Update()
    {
        if(time > 1f)
        {
            Destroy(gameObject);
        }
        transform.localScale = Vector2.Lerp(startScale, startScale * 5f, time);
        time += Time.deltaTime * 3f;

    }
}
