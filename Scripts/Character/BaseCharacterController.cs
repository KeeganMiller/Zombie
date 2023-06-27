using Godot;
using System;

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

	protected void CreateBlackboard()
	{
		_Blackboard = new Blackboard();
		_Blackboard.SetValueAsNode("Self", this);
		_Blackboard.SetValueAsBool("HasMoveToLocation", false);
		_Blackboard.SetValueAsVector2("MoveToLocation", Vector2.Zero);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		
	}

	public Blackboard GetBlackboard() => _Blackboard;
}
