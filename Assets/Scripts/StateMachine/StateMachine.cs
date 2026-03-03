
public class StateMachine
{
    public EntityState CurrentState { get; private set; }
    public bool CanChangeState;
    public void Initialize(EntityState startState)
    {
        CanChangeState = true;
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if (!CanChangeState) return;

        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdateActiveState()
    {
        CurrentState.Update();        
    }
    public void SwitchOffStateMachine() => CanChangeState = false;
}
