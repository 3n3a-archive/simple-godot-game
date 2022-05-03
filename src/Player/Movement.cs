using Godot;
using System;

public class Movement : KinematicBody2D
{
	[Export] public int RunSpeed = 200;
	[Export] public int JumpSpeed = -500;
	[Export] public int Gravity = 1200;

	Vector2 velocity = new Vector2();
	Vector2 oldVelocity = new Vector2();
	bool jumping = false;
	
	[Signal]
	public delegate void GameOver();

	public void getInput()
	{
		velocity.x = 0;
		bool right = Input.IsActionPressed("ui_right");
		bool left = Input.IsActionPressed("ui_left");
		bool jump = Input.IsActionPressed("ui_select");

		if (jump && IsOnFloor())
		{
			jumping = true;
			velocity.y = JumpSpeed;
		}
		if (right)
		{
			velocity.x += RunSpeed;
		}
		if (left)
		{
			velocity.x -= RunSpeed;
		}
	} 
	
	public void detectFall()
	{
		if (
			velocity.y > 0
			&& (velocity.y > oldVelocity.y)
			&& (velocity.y > 1500)
		) {
			GD.Print("Player died falling down");
			//QueueFree();
			EmitSignal(nameof(GameOver));
			// Show Game Over
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		oldVelocity = velocity;
		getInput();
		velocity.y += Gravity * delta;
		if (jumping && IsOnFloor())
		{
			jumping = false;
		}
		detectFall();
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
	}
	
	private void _on_Goal_body_entered(object body)
	{
		var global = GetNode<Global>("/root/Global");
		global.GotoScene("res://Win.tscn");
	}
}


