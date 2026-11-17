using Godot;
using Godot.Collections;

public partial class Global : Node
{
	[Export] private ProgressBar LevelBar { get; set; }
	[Export] public WaveComponent Wave { get; set; }
	[Export] private CanvasModulate Modulate { get; set; }
	[Export] private Label LevelNumText;
	public float PlayerXP { get; set; } = 0.0f;
	public float ExperienceRequired { get; set; } = 0;
	public Array<float> ExperienceData { get; set; } = new Array<float>();

	private int Level { get; set; } = 1;

	public override void _Ready()
	{
		base._Ready();

		GD.Print("LevelBar: ", LevelBar);
		ExperienceRequired = GetRequiredXP();

		if (LevelBar != null)
			LevelBar.MaxValue = ExperienceRequired;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		LevelSystem();
	}

	public void LevelSystem()
	{
		if (LevelBar == null)
			return;

		LevelBar.Value = PlayerXP;
		LevelBar.MaxValue = ExperienceRequired;
		if (PlayerXP >= ExperienceRequired)
		{
			LevelUp();
			ExperienceData.Add(ExperienceRequired);
			GD.Print("Level: ", Level);
			GD.Print("New Requisition: ", ExperienceRequired);
		}
		ExperienceData.Add(PlayerXP);
		ExperienceData.Add(ExperienceRequired);
	}

	public float GetRequiredXP()
	{
		return (float)Mathf.Round(Mathf.Pow(Level, 1.85) + Level * 4 + 10);
	}

	public float GetLevelPorcentage()
	{
		float porcentage = ExperienceRequired * PlayerXP / 100;
		if (porcentage <= 0) return 0;
		return porcentage;
	}

	public void LevelUp()
	{
		Level++;
		LevelNumText.Text = Level.ToString(); // atualiza 
		ExperienceRequired = GetRequiredXP();
		PlayerXP = 0;
	}
}
