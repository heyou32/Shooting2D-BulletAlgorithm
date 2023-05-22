 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSequenceToBeFinishedOnDelay : SequenceBase
{
    public float delayTime = 0;
    public override void OnSequencerUpdate()
    {
        if (RegisterBoss.count == 0)
            Invoke("MakeDisable", delayTime);
    }
    void MakeDisable()
    {
            enabled = false;
    }
}
