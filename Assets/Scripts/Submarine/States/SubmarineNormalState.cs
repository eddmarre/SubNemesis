using UnityEngine;

public class SubmarineNormalState : SubmarineBaseState
{
    public override void StartState(SubmarineController submarineController)
    {
    }

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
        submarineController.MovementHandler();
        submarineController.ShootHandler();
        if (submarineController.DashHandler())
        {
            submarineController.SwitchState(submarineController.DashState);
        }
    }
}