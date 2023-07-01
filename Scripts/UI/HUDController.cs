using Godot;
using System;

public partial class HUDController : CanvasLayer
{
	[ExportGroup("Toggle Mode Buttons")]
	[Export] private Texture2D _PlayTexture;
	[Export] private Texture2D _BuildTexture;
	[Export] private NodePath _TogglePath;

	[Export] private Control _BuildingUI;

	private TextureButton _ToggleButton;

	public override void _Ready()
	{
		base._Ready();
		_ToggleButton = GetNode<TextureButton>(_TogglePath);
	}
	public void OnModeTogglePressed()
	{
		BuildModeController buildMode = GetNode<BuildModeController>("/root/BuildModeController");
		if (buildMode != null)
		{
			if (buildMode.IsBuildMode)
			{
				buildMode.IsBuildMode = false;

				if (_ToggleButton != null)
					_ToggleButton.TextureNormal = _BuildTexture;

				if (_BuildingUI != null)
					_BuildingUI.Visible = false;
			}
			else
			{
				buildMode.IsBuildMode = true;

				if (_ToggleButton != null)
					_ToggleButton.TextureNormal = _PlayTexture;


				if (_BuildingUI != null)
					_BuildingUI.Visible = true;
			}
		}
	}
}
