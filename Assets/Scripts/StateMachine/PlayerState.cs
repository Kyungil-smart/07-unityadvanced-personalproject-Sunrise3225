using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player Character;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.Character = player;

        anim = player.Anim;
        rb = player.rb;
    }
    public override void Update()
    {
        base.Update();

        if (Character.Input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(Character.DashState);
    }
    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private bool CanDash()
    {
        if (Character.WallDetected) return false;
        if (stateMachine.CurrentState == Character.DashState) return false;

        return true;
    }
}
