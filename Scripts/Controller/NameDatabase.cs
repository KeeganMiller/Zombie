using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class NameDatabase : Node2D
{
    private const string FILE_LOCATION = "res://Database/Names.json";
    private List<string> _MaleNames = new List<string>();
    private List<string> _FemaleNames = new List<string>();
    private List<string> _LastNames = new List<string>();

    public override void _Ready()
    {
        base._Ready();
        LoadNames();
        
        foreach(var name in _MaleNames)
            GD.Print(name);
    }

    private void LoadNames()
    {
        var file = Godot.FileAccess.Open(FILE_LOCATION, Godot.FileAccess.ModeFlags.Read);
        if (file != null)
        {
            if (file.IsOpen())
            {
                string text = file.GetAsText();
                var objects = JArray.Parse(text);

                foreach (var obj in objects)
                {
                    JToken token = obj;
                    NameData name = JsonConvert.DeserializeObject<NameData>(token.ToString());

                    if (name != null)
                    {
                        if (name.Type == "Male")
                        {
                            _MaleNames.Add(name.Name);
                        } else if (name.Type == "Female")
                        {
                            _FemaleNames.Add(name.Name);
                        } else if (name.Type == "LastName")
                        {
                            _LastNames.Add(name.Name);
                        }
                        else
                        {
                            GD.PrintErr($"#NameDatabase::LoadNames - Failed to load name {name.Name}, {name.Type}");
                        }
                    }
                }
                
                file.Close();
            }
        }
    }
}

public class NameData
{
    public string Name;
    public string Type;
}