using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBlock : Blocks
{
    protected override void Init()
    {
        type = 0;
    }

    private void Start()
    {
        Init();
    }
}

