using SubNemesis.Submarine;
using UnityEngine;

namespace SubNemesis.States
{
    public abstract class SubmarineBaseState
    {
        public abstract void OnCollisionEnterState(SubmarineController submarineController, Collision collision);
        public abstract void UpdateState(SubmarineController submarineController);
    }
}