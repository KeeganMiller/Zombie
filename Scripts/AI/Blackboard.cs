using System;
using System.Collections.Generic;
using Godot;

public class Blackboard
{
    private List<BlackboardProperty> _Properties = new List<BlackboardProperty>();

    public void SetValueAsInt(string key, int value)
    {
        foreach (var prop in _Properties)
        {
            if (prop == key)
            {
                if (prop is PropertyInt propInt)
                {
                    propInt.Value = value;
                    return;
                }
            }
        }
        
        _Properties.Add(new PropertyInt(key, value));
    }

    public int GetValueAsInt(string key)
    {
        foreach (var prop in _Properties)
            if(prop == key)
                if (prop is PropertyInt propInt)
                    return propInt.Value;

        return 0;
    }

    public void SetValueAsFloat(string key, float value)
    {
        foreach (var prop in _Properties)
        {
            if (prop == key)
            {
                if (prop is PropertyFloat propFloat)
                {
                    propFloat.Value = value;
                    return;
                }
            }
        }
        
        _Properties.Add(new PropertyFloat(key, value));
    }

    public float GetValueAsFloat(string key)
    {
        foreach(var prop in _Properties)
            if(prop == key)
                if (prop is PropertyFloat propFloat)
                    return propFloat.Value;

        return 0f;
    }

    public void SetValueAsBool(string key, bool value)
    {
        foreach (var prop in _Properties)
        {
            if (prop == key)
            {
                if (prop is PropertyBool propBool)
                {
                    propBool.Value = value;
                    return;
                }
            }
        }

        _Properties.Add(new PropertyBool(key, value));
    }

    public bool GetValueAsBool(string key)
    {
        foreach(var prop in _Properties)
            if(prop == key)
                if (prop is PropertyBool propBool)
                    return propBool.Value;

        return false;
    }

    public void SetValueAsVector2(string key, Vector2 value)
    {
        foreach (var prop in _Properties)
        {
            if (prop == key)
            {
                if (prop is PropertyVector2 propVec)
                {
                    propVec.Value = value;
                    return;
                }
            }
        }
        
        _Properties.Add(new PropertyVector2(key, value));
    }

    public Vector2 GetValueAsVector2(string key)
    {
        foreach(var prop in _Properties)
            if(prop == key)
                if (prop is PropertyVector2 propVec)
                    return propVec.Value;

        return Vector2.Zero;
    }

    public void SetValueAsNode(string key, Node2D value)
    {
        foreach (var prop in _Properties)
        {
            if (prop == key)
            {
                if (prop is PropertyNode propNode)
                {
                    propNode.Value = value;
                    return;
                }
            }
        }
        
        _Properties.Add(new PropertyNode(key, value));
    }

    public Node2D GetValueAsNode(string key)
    {
        foreach(var prop in _Properties)
            if(prop == key)
                if (prop is PropertyNode propNode)
                    return propNode.Value;

        return null;
    }
}

public class BlackboardProperty
{
    public string Key;

    public BlackboardProperty(string key)
    {
        Key = key;
    }

    public static bool operator ==(BlackboardProperty a, string b)
    {
        return a.Key == b;
    }

    public static bool operator !=(BlackboardProperty a, string b)
    {
        return !(a == b);
    }
}

public class PropertyInt : BlackboardProperty
{
    public int Value;

    public PropertyInt(string key, int value) : base(key)
    {
        Value = value;
    }
}

public class PropertyFloat : BlackboardProperty
{
    public float Value;

    public PropertyFloat(string key, float value) : base(key)
    {
        Value = value;
    }
}

public class PropertyBool : BlackboardProperty
{
    public bool Value;

    public PropertyBool(string key, bool value) : base(key)
    {
        Value = value;
    }
}

public class PropertyVector2 : BlackboardProperty
{
    public Vector2 Value;

    public PropertyVector2(string key, Vector2 value) : base(key)
    {
        Value = value;
    }
}

public class PropertyNode : BlackboardProperty
{
    public Node2D Value;

    public PropertyNode(string key, Node2D value) : base(key)
    {
        Value = value;
    }
}