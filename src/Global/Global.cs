using Godot;
using System;

public class Global : Node
{
	public Node CurrentScene { get; set; }
	
	public override void _Ready() 
	{
		Viewport root = GetTree().GetRoot();
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
	}
	
	public void GotoScene(string path)
	{
		CallDeferred(nameof(DeferredGotoScene), path);
	}

	public void DeferredGotoScene(string path)
	{
		CurrentScene.Free();

		var nextScene = (PackedScene)GD.Load(path);

		CurrentScene = nextScene.Instance();
		GetTree().GetRoot().AddChild(CurrentScene);
		GetTree().SetCurrentScene(CurrentScene);
	}
}
