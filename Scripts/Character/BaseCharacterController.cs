using Godot;
using System;
using System.Collections.Generic;

public enum EMovementState
{
	FOLLOW_PATH = 0,
	WANDER = 1
}

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
	protected BehaviorTree _BTree;
	public BehaviorTree BTree => _BTree;
	public NavAgent Agent => _Agent;
	
	// === FOLLOW PATH SETTINGS === //
	[Export] protected NodePath _PathControllerNode;
	private PathController _FollowPath;
	public PathController FollowPath => _FollowPath;
	[Export] public bool CirclePath;
	public EFollowDirection FollowDirection = EFollowDirection.FORWARDS;

		// ;=== MOVEMENT SETTINGS === //
	[Export] protected float _GeneralMovementSpeed = 100f;
	
	// === Character Body === //
	private Sprite2D _CharacterSprite;						// Reference to the characters sprite

	[ExportGroup("Character Directions")]
	[Export] private Texture2D _Front;
	[Export] private Texture2D _Back;
	[Export] private Texture2D _Left;
	[Export] private Texture2D _Right;

	private float delta;

	public override void _Ready()
	{
		base._Ready();
		CreateBlackboard();
		_FollowPath = GetNode<PathController>(_PathControllerNode);
		_Agent = GetNode<NavAgent>("Agent");


		_CharacterSprite = GetNode<Sprite2D>("Sprite2D");

		Callable.From(ActorSetup).CallDeferred();
	}
	
	protected virtual void CreateBlackboard()
	{
		_Blackboard = new Blackboard();
		_Blackboard.SetValueAsNode("Self", this);
		_Blackboard.SetValueAsBool("HasMoveToLocation", false);
		_Blackboard.SetValueAsVector2("MoveToLocation", Vector2.Zero);
		
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if(_BTree != null)
			_BTree.Update((float)delta);
		
		UpdateCharacterSprite();
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		this.delta = (float)delta;
		if (!_Agent.HasPath || _Agent.HasReachPath)
			return;

		
	}

	public void MoveToLocation()
	{
		Velocity = _Agent.GetMovementDirection() * _GeneralMovementSpeed;
		MoveAndSlide();
	}

	public Blackboard GetBlackboard() => _Blackboard;

	protected virtual async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
	}

	protected virtual void UpdateCharacterSprite()
	{
		if (this.Velocity.Y > 0)
			_CharacterSprite.Texture = _Front;
		if (this.Velocity.Y < 0)
			_CharacterSprite.Texture = _Back;

		if (this.Velocity.X > 0)
			_CharacterSprite.Texture = _Right;
		if (this.Velocity.X < 0)
			_CharacterSprite.Texture = _Left;
	}
}
