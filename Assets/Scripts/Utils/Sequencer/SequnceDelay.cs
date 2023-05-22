using System.Collections;
using UnityEngine;
 

public class SequnceDelay : SequenceBase
{
    public float delayTime;
    bool onEnabed;
   
    public override void OnEnable()
    {
        if (onEnabed)
            StartCoroutine(StartDelay());
        onEnabed = true;
    }
    public override void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(delayTime);
        enabled=false;
    }
}
