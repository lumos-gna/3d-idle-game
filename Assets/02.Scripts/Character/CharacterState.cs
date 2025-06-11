public abstract class CharacterState
{
    protected CharacterController _controller;
    
    public CharacterState(CharacterController controller)
    {
        _controller = controller;
    }

    public abstract CharacterStateType StateType { get; }

    public abstract void OnEnter();
    public abstract void OnUpdate();
}