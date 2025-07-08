using Godot;
using System;

namespace PlayerFSM;

public partial class PlayerState : State
{
    public Player PlayerNode;

    public override void _Ready()
    {
        PlayerNode = GetOwnerOrNull<Player>();
        if (PlayerNode == null)
        {
            GD.PrintErr("Player state node is not a descendant of a player class!");
            Free();
        }
    }
}