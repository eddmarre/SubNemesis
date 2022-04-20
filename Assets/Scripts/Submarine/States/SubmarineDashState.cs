using UnityEngine;

public class SubmarineDashState : SubmarineBaseState
{
    public override void StartState(SubmarineController submarineController)
    {
    }

    public override void OnCollisionEnterState(SubmarineController submarineController, Collision collision)
    {
    }

    public override void UpdateState(SubmarineController submarineController)
    {
        submarineController.ResetMovement();
    }
}