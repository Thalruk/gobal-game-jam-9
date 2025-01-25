using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : Blocks
{
    protected override void Init()
    {
        type = 3;
    }

    private void Start()
    {
        Init();
    }
}
