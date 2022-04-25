using System;
using IndieMarc.EnemyVision;
using SubNemesis.Submarine;
using UnityEngine;

namespace SubNemesis.States
{
    public class SubmarineDeathState : SubmarineBaseState
    {
        public override void OnCollisionEnterState(SubmarineController submarineController, Collision collision)
        {
        }

        public override void UpdateState(SubmarineController submarineController)
        {
            submarineController.GetComponent<VisionTarget>().visible = false;
            submarineController.GetComponent<BoxCollider>().enabled = false;
            try
            {
                submarineController.GetComponentInChildren<MakeSubmarineInvisible>().Disappear();
            }
            catch (Exception e)
            {
                String error = e.ToString();
            }
        }
    }
}