using Godot;
using System;

public partial class Cannon : Node2D
{

	[Export] public PackedScene BulletScene { get; set; } // Deixar sempre os parametros de forma personalizavel no inspetor
	[Export] public Marker2D ShootPos { get; set; }
	[Export] public float CooldownTime { get; set; } = 1.0f;

	private bool _canShoot = true; // 
	private Timer _cooldownTimer;

	public override void _Ready()
	{
		base._Ready();

		// Timer único para cooldown (filho do cannon)
		_cooldownTimer = new Timer
		{
			WaitTime = CooldownTime,
			OneShot = true,
			Autostart = false
		};

		AddChild(_cooldownTimer);
		_cooldownTimer.Timeout += () => _canShoot = true;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Input.IsActionJustPressed("Shoot"))
		{
			Shoot();
		}
	}

	public void Shoot()
	{
		// Verificação iniciais (fail-fast)
		if (BulletScene is null || ShootPos is null)
		{
			GD.PrintErr("BulletScene ou ShootPos não definidos no inspector!");
			return;
		}

		if (!_canShoot) return; // Evita tiros durante cooldown

		_canShoot = false;
		_cooldownTimer.Start(); // Reinicia o cooldown

		// Instancia e posiciona o bullet
		Node2D bullet = BulletScene.Instantiate<Node2D>();
		GetTree().Root.AddChild(bullet);
		bullet.GlobalPosition = ShootPos.GlobalPosition;
		bullet.GlobalRotation = ShootPos.GlobalRotation - Mathf.Pi / 2; // Usa constante Godot
	}
	/*    -------- FEITO POR MIM COM POUCA CONSULTA ---------

		[Export] public PackedScene BulletScene { get; set; }
		[Export] public Marker2D ShootPos { get; set; }
		public bool CanShoot { get; set; } = true;
		private Timer timer;

		public override void _Process(double delta)
		{
			base._Process(delta);
			if (Input.IsActionJustPressed("Shoot"))
			{
				Shoot();
			}
		}


		public void Shoot()
		{

			timer = new Timer
			{
				WaitTime = 1.0,
				Autostart = true
			};

			timer.Timeout += () =>
			{
				CanShoot = true;
			};

			if (BulletScene is null || ShootPos is null)
			{
				GD.PrintErr("BulletScene or/and ShootPos was not defined!");
				return;
			}
			if (CanShoot == true)
			{
				Node2D bullet = BulletScene.Instantiate<Node2D>();
				GetTree().Root.AddChild(bullet);
				bullet.GlobalPosition = ShootPos.GlobalPosition;
				bullet.GlobalRotation = ShootPos.GlobalRotation + -(float)Math.PI / 2;
				_ = !CanShoot;
				timer.Start();
			}
		} */

	//     ------ CORREÇÃO FEITA POR IA: (NVIDIA: Nemotron 3 Super) ------
}