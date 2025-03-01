using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFloat : DebugBase<float>
{
    private void Update()
    {
        if(Input.GetKeyDown(_key))
        {
            CallDebug();
        }
    }

    public override void CallDebug()
    {
        _onDebug.Invoke(_value);
    }
}
