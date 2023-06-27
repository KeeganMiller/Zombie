using Godot;
using System;
using System.Collections.Generic;

public partial class BaseCharacterController : CharacterBody2D
{
	[Export] public bool GenerateRandomCharacter = true;					// If we should generate a random character
	
	// === CHARACTER DETAILS === //
	private CharacterDetails _CharacterInformation;
	public CharacterDetails CharacterInformation => _CharacterInformation;
	
	// === CHARACTER ATTRIBUTES === //
	protected CharacterAttributes _Attributes;
	public CharacterAttributes Attributes => _Attributes;
	
	// === AI === //
	protected Blackboard _Blackboard;
	protected NavAgent _Agent;
	public NavAgent Agent => _Agent;
	
	// === MOVEMENT SETTINGS === //
	[Export] protected float _GeneralMovementSpeed = 100f;

	public override void _Ready()
	{
		base._Ready();
		CreateBlackboard();
		_Agent = GetNode<NavAgent>("Agent");
		
		Callable.From(ActorSetup).CallDeferred();
	}

	protected void CreateBlackboard()
	{
		_Blackboard = new Blackboard();
		_Blackboard.SetValueAsNode("Self", this);
		_Blackboard.SetValueAsBool("HasMoveToLocation", false);
		_Blackboard.SetValueAsVector2("MoveToLocation", Vector2.Zero);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (!_Agent.HasPath || _Agent.HasReachPath)
			return;

		Vector2 currentPos = this.GlobalPosition;
		Vector2 nextPathPoint = _Agent.GetNextPathPoint();

		Vector2 movementVelocity = (nextPathPoint - currentPos).Normalized();
		movementVelocity *= _GeneralMovementSpeed;
		this.Velocity = movementVelocity;
		
		MoveAndSlide();
	}

	public Blackboard GetBlackboard() => _Blackboard;

	protected virtual async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
		
		_Agent.SetTargetLocation(new Vector2(530, 374));
	}
}
