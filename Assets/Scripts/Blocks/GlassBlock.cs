using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBlock : Blocks
{
    protected override void Init()
    {
        type = 2;
    }
    private void Start()
    {
        Init();
    }
}
