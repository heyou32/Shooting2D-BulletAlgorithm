 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : SequenceBase
{
    public GameScoreUI endUI;
    public override void OnSequencerUpdate()
    {
        if (Register.count == 0)
        {
            endUI.GameEndShow();
            enabled = false;
        }
    }
}
