using Godot;
using System;

namespace PlayerFSM;

public partial class IdleState : PlayerState
{
    public override void OnPhysicsUpdate(float delta)
    {
        if (!PlayerNode.IsTouchingFloor())
        {
            fsm.Transition<JumpState>();
        }

        if (PlayerNode.inputDir.Length() > 0)
        {
            fsm.Transition<WalkState>();
        }

        PlayerNode.HandleMovement(delta);
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