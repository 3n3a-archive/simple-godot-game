using Godot;
using System;

public class LevelHelper : Node2D
{
	public override void _Ready()
	{
		GetNode("Player").Connect("GameOver", this, nameof(_on_Game_Over));
	}
	
	public void DisplayGameOver()
	{
		var global = GetNode<Global>("/root/Global");
		global.GotoScene("res://GameOver.tscn");
	}

	public void _on_Game_Over()
	{
		GD.Print("Game Over");
		QueueFree();
		DisplayGameOver();
	}
}
