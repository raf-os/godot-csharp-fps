using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export]
    float baseSpeed = 20.0f;
    [Export]
    float sprintSpeed = 10.0f;
    [Export]
    float acceleration = 100.0f;
    [Export]
    float jumpSpeed = 8.0f;
    [Export]
    float mouseSensitivity = 0.005f;
    [Export]
    float groundFriction = 100.0f;
    [Export]
    float airFriction = 5.0f;
    [Export]
    float airControl = 25.0f;

    public float gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");

    [ExportGroup("Nodes")]
    [Export]
    Node3D Head;
    [Export]
    Node3D Camera;

    private float _cameraX = 0f;
    private float _cameraY = 0f;
    public Vector2 inputDir { get; private set; } = Vector2.Zero;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateCameraRotation((float)delta);

        if (!IsTouchingFloor())
        {
            Velocity = new Vector3(
                x: Velocity.X,
                y: Velocity.Y - (gravity * 1.5f * (float)delta),
                z: Velocity.Z
            );
        }

        inputDir = Input.GetVector("moveLeft", "moveRight", "moveForwards", "moveBackwards");
        
        //HandleMovement((float)delta);

        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion)
        {
            _cameraX = Convert.ToSingle(-motion.Relative.X * mouseSensitivity);
            _cameraY = Convert.ToSingle(-motion.Relative.Y * mouseSensitivity);
        }
    }

    private void UpdateCameraRotation(float delta)
    {
        // Horizontal rotation
        Head.RotateY(_cameraX * delta);

        // Vertical rotation
        Vector3 currentRotation = Camera.Rotation;
        currentRotation.X += _cameraY * delta;
        currentRotation.X = Mathf.Clamp(currentRotation.X, Mathf.DegToRad(-90f), Mathf.DegToRad(90f));
        Camera.Rotation = currentRotation;

        _cameraX = 0f;
        _cameraY = 0f;
    }

    public bool AttemptJump()
    {
        if (IsTouchingFloor())
        {
            Velocity = new Vector3(
                Velocity.X,
                jumpSpeed,
                Velocity.Z
            );
            return true;
        }
        return false;
    }

    public void HandleMovement(float delta)
    {
        Vector3 moveDir = (Head.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (moveDir.Length() > 0)
        {
            if (IsTouchingFloor())
            {
                GroundMovement(delta, moveDir);
            }
            else
            {
                AirMovement(delta, moveDir);
            }
        }
        else
        {
            AttemptStop(delta);
        }
    }

    private void AirMovement(float delta, Vector3 moveDirection)
    {
        Vector3 desiredVelocity = moveDirection * baseSpeed;

        float moveX = desiredVelocity.X;
        float moveZ = desiredVelocity.Z;

        Velocity = Velocity.MoveToward(
            new Vector3(
                moveX,
                Velocity.Y,
                moveZ
            ),
            delta * airControl
        );
    }

    private void GroundMovement(float delta, Vector3 moveDirection)
    {
        Vector3 desiredVelocity = moveDirection * baseSpeed;

        Velocity = Velocity.MoveToward(
            new Vector3(
                desiredVelocity.X,
                Velocity.Y,
                desiredVelocity.Z
            ),
            delta * acceleration
        );
    }

    public void AttemptStop(float delta)
    {
        float speedMult = IsTouchingFloor() ? groundFriction : airFriction;
        Velocity = Velocity.MoveToward(
            new Vector3(0, Velocity.Y, 0),
            delta * speedMult
        );
    }

    public bool IsTouchingFloor()
    {
        return IsOnFloor();
    }
}
