public class StateMachine {
    public IState CurrentState { get; private set; }
    public void ChangeState(IState s) { CurrentState?.Exit(); CurrentState = s; CurrentState?.Enter(); }
    public void Update() => CurrentState?.Update();
    public void FixedUpdate() => CurrentState?.FixedUpdate();
}
