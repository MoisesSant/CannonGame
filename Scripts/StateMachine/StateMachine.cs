using Godot;
using System.Collections.Generic;

public partial class StateMachine : Node
{
  [Export] public NodePath InitialState;

  private Dictionary<string, State> _states; // Armazena todos os nomes dos States em um dicionário
  private State _currentState;

  public override void _Ready()
  {
    base._Ready();
    _states = new Dictionary<string, State>();
    foreach (Node node in GetChildren())
    {
      if (node is State s)
      {
        _states[node.Name] = s;
        s.fsm = this;
        s.Ready();
        s.Exit(); // Reseta todos os States
      }
    }

    _currentState = GetNode<State>(InitialState);
    _currentState.Enter();
  }


  //       Sincronizando as funções e tornado-as funcionais
  public override void _PhysicsProcess(double delta)
  { base._PhysicsProcess(delta); _currentState.PhysicsUpdate(delta); }

  public override void _Process(double delta)
  { base._Process(delta); _currentState.Update(delta); }

  public override void _UnhandledInput(InputEvent @event)
  { base._UnhandledInput(@event); _currentState.HandleInput(@event); }


  //       Lógica de Transição
  public void TrasitionTo(string Key)
  {
    if (!_states.ContainsKey(Key) || _currentState == _states[Key]) // caso não tenha o mesmo nome ignora
      return;

    _currentState.Exit();
    _currentState = _states[Key];
    _currentState.Enter();
  }
}