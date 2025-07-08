using Godot;
using System;

namespace PlayerFSM;

public partial class JumpState : PlayerState
{
    public override void OnPhysicsUpdate(float delta)
    {
        if (PlayerNode.IsTouchingFloor())
        {
            if (PlayerNode.inputDir.Length() > 0)
            {
                fsm.Transition<IdleState>();
            }
            else
            {
                fsm.Transition<WalkState>();
            }
        }
        else
        {
            PlayerNode.HandleMovement(delta);
        }
    }

    public override void OnHandleInput(InputEvent @event)
    {
        // TODO: Possible coyote timing feature
        if (@event.IsActionPressed("jump"))
        {
            if (PlayerNode.AttemptJump())
            {
                fsm.Transition<JumpState>();
            }
        }
    }
}