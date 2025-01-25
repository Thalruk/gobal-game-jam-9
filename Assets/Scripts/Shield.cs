using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] Sprite shield;
    [SerializeField] Sprite[] decals;
    [SerializeField] int shieldType;
    [SerializeField] bool shooted = false;
    [SerializeField] Gate gate;


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Bubble>().type == shieldType && !shooted)
        {
            gate.IncreaseShootedTargets();
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = decals[Random.Range(0, decals.Length)];
            shooted = true;
        }
    }
}
