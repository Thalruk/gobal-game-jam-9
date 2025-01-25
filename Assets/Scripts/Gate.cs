using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [SerializeField] int numbersOfTarget;
    [SerializeField] int shootedTargets;
    [SerializeField] public int gateType;

   
    public void IncreaseShootedTargets()
    {
        shootedTargets++;
        if(shootedTargets == numbersOfTarget)
        {
            Destroy(gameObject);
        }
    }

}
