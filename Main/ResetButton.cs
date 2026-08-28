using Godot;

public partial class ResetButton : Button
{
	[Export] public PackedScene GamePath;
	private Button _btn;
	public override void _Ready()
	{
		base._Ready();
	}

}
