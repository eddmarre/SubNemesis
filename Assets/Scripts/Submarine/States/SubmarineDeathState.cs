using System;
using IndieMarc.EnemyVision;
using UnityEngine;

public class SubmarineDeathState : SubmarineBaseState
{
    public override void StartState(SubmarineController submarineController)
    {
    }

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