using SubNemesis.Submarine;
using UnityEngine;

namespace SubNemesis.States
{
    public class SubmarineDashState : SubmarineBaseState
    {
        public override void OnCollisionEnterState(SubmarineController submarineController, Collision collision)
        {
        }

        public override void UpdateState(SubmarineController submarineController)
        {
            submarineController.ResetMovement();
        }
    }
}