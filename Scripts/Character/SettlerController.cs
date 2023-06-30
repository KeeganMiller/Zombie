using Godot;

namespace Zombie.Scripts.Character;

public class SettlerController : BaseCharacterController
{
    protected override void CreateBlackboard()
    {
        base.CreateBlackboard();
        _Blackboard.SetValueAsInt("MovementState", (int)EMovementState.FOLLOW_PATH);
        _Blackboard.SetValueAsInt("CurrentPathIndex", 0);
        _Blackboard.SetValueAsBool("HasPathPoint", false);
        _Blackboard.SetValueAsBool("IsWaiting", false);
        _Blackboard.SetValueAsFloat("WaitTime", 5.0f);
    }
}