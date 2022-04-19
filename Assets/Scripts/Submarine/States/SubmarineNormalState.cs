public class SubmarineNormalState : SubmarineBaseState
{
    public override void StartState(SubmarineController submarineController)
    {
    }

    public override void OnCollisionEnterState(SubmarineController submarineController)
    {
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