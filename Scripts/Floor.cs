using Godot;

[Tool]
public class Floor : MeshInstance
{


	[Export]
	ShaderMaterial TerrainMaterial { get; set; }

	[Export]
	public bool Selected { get; set; }



	public void OnAreaEnter(object area)
	{
		Selected = true;
		CheckSelected();
	}
	public void OnAreaExit(object area)
	{
		Selected = false;
		CheckSelected();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Selected = false;
		TerrainMaterial = (ShaderMaterial)TerrainMaterial.Duplicate(true);
		this.MaterialOverride = TerrainMaterial;
	}

	private void CheckSelected()
	{

		if (Selected)
		{
			TerrainMaterial.SetShaderParam("borderwidth0_1", .56f);
		}
		else
		{
			TerrainMaterial.SetShaderParam("borderwidth0_1", 0);
		}
	}
}





