using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : Blocks
{
    protected override void Init()
    {
        type = 1;
    }
    private void Start()
    {
        Init();
    }
}
