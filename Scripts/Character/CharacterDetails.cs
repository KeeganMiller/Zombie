using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class CharacterDetails
{
    private string _FirstName;
    private string _LastName;
    private float _Age;

    public string FirstName => _FirstName;
    public string LastName => _LastName;
    public float Age => _Age;
}