public abstract class SubmarineBaseState
{
    public abstract void StartState(SubmarineController submarineController);
    public abstract void OnCollisionEnterState(SubmarineController submarineController);
    public abstract void UpdateState(SubmarineController submarineController);
}