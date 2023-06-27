using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class CharacterAttributes
{
    private const int MAX_LEVEL = 10;

    // === PROOPERTIES === //
    private int _Strength;
    private int _Dexterity;
    private int _Vitality;
    private int _Intelligence;
    private int _Charisma;
    
    // === GETTERS === //
    public int Strength => _Strength;
    public int Dexterity => _Dexterity;
    public int Vitality => _Vitality;
    public int Intelligence => _Intelligence;
    public int Charisma => _Charisma;


}