using SubNemesis.Enemy;
using SubNemesis.Submarine;
using UnityEngine;

namespace SubNemesis.States
{
    public class SubmarineNormalState : SubmarineBaseState
    {
        public override void OnCollisionEnterState(SubmarineController submarineController, Collision collision)
        {
            if (collision.gameObject.GetComponent<FishEnemy>())
            {
                if (submarineController.onTakeDamageAction != null)
                {
                    submarineController.onTakeDamageAction.Invoke(10f);
                }
            }

            else if (collision.gameObject.CompareTag("Environment"))
            {
                submarineController.onTakeDamageAction.Invoke(10f);
            }
        }

        public override void UpdateState(SubmarineController submarineController)
        {
            submarineController.GetComponent<UbhShotCtrl>().StartShotRoutine();
            submarineController.MovementHandler();
            submarineController.ShootHandler();
            if (submarineController.DashHandler())
            {
                submarineController.SwitchState(submarineController.DashState);
            }
        }
    }
}