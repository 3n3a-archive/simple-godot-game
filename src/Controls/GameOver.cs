using Godot;
using System;

public class GameOver : Node2D
{
	private void _on_Button_pressed()
	{
		// Restart Game
		var global = GetNode<Global>("/root/Global");
		global.GotoScene("res://Level1.tscn");
	}
}
