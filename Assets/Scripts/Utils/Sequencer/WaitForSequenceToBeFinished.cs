using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.Sequencer
{
    public class WaitForSequenceToBeFinished : SequenceBase
    {
        public override void OnSequencerUpdate()
        {
            if (Register.count == 0) enabled = false;
        }
    }
}