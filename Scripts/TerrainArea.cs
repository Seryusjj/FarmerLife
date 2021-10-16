using Godot;
using System;

public class TerrainArea : Area
{

	private MultiMeshTerrain SelectableTerrain { get; set; }
	int TerrainId { get; set; }

	public TerrainArea() : base() { }

	public TerrainArea(int terrainId, Vector3 traslation, int numTerrainUnits = 1)
		: base()
	{
		this.TerrainId = terrainId;
		this.Translation = traslation;
		var shape = new CollisionShape();
		var box = new BoxShape();
		shape.Shape = box;
		box.Extents = new Vector3(numTerrainUnits * 0.5f, 1, 0.5f * numTerrainUnits);
		this.AddChild(shape);
	}
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SelectableTerrain = GetParent() as MultiMeshTerrain;
		this.CollisionLayer = 4; //areas -> layer 2 -> tag = 2²;
		this.CollisionMask = 0; //player > layer 1 -> tag = 2⁰;   
		this.Connect("area_entered", this, nameof(OnAreaEnter));
	}

	private void OnAreaEnter(Area area)
	{
		GD.Print(string.Format("Area enter for terrain id {0}", this.TerrainId));
		SelectableTerrain.SelectInstance(this.TerrainId);

	}


}
