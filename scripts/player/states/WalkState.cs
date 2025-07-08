using Godot;
using System;

namespace PlayerFSM;

public partial class WalkState : PlayerState
{
    public override void OnPhysicsUpdate(float delta)
    {
        if (!PlayerNode.IsTouchingFloor())
        {
            fsm.Transition<JumpState>();
        }

        if (PlayerNode.inputDir.Length() > 0)
        {
            PlayerNode.HandleMovement(delta);
        }
        else
        {
            fsm.Transition<IdleState>();
        }
    }

    public override void OnHandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
        {
            if (PlayerNode.AttemptJump())
            {
                fsm.Transition<JumpState>();
            }
        }
    }
}