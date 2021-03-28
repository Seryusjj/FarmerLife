using Godot;


[Tool]
public class MultiMeshTerrain : MultiMeshInstance
{

	int _simultaneousSelection = 1;
	private int _selected;

	[Export]
	public int SimultaneousSelection
	{
		get
		{
			return _simultaneousSelection;
		}
		set
		{
			_simultaneousSelection = value;
			_selected = -1;
		}
	}

	public void SelectInstance(int instanceNum)
	{
		if (SimultaneousSelection == 1 && _selected != -1 && instanceNum != _selected)
			UnselectInstance(_selected);

		_selected = instanceNum;
		float selectionWidht = 0.55f;
		Multimesh.SetInstanceColor(instanceNum, new Color(selectionWidht, selectionWidht, selectionWidht));
	}

	public void UnselectInstance(int instanceNum)
	{
		Multimesh.SetInstanceColor(instanceNum, new Color(0, 0, 0, 0));
	}

	private void GenerateTerrain()
	{
		Multimesh.TransformFormat = MultiMesh.TransformFormatEnum.Transform3d;
		Multimesh.ColorFormat = MultiMesh.ColorFormatEnum.Color8bit;
		Multimesh.CustomDataFormat = MultiMesh.CustomDataFormatEnum.Data8bit;
		Multimesh.VisibleInstanceCount = Multimesh.InstanceCount;

		var length = (int)Mathf.Sqrt(Multimesh.VisibleInstanceCount);
		length = (int)(length * 0.5f);

		int count = 0;
		for (int i = -length; i < length; i++)
		{
			for (int j = -length; j < length; j++)
			{
				var translation = new Vector3(.5f + i, 0, .5f + j);
				Multimesh.SetInstanceTransform(count, new Transform(Basis.Identity, translation));
				// add shader info
				UnselectInstance(count);
				//add selection area
				this.AddChild(new TerrainArea(count, translation));
				count++;
			}
		}

		// generate the static body
		var body = new StaticBody();
		var collider = new CollisionShape();
		var box = new BoxShape();
		collider.Shape = box;
		box.Extents = new Vector3(length + 0.005f, 0.2f, length + 0.005f);
		body.CollisionLayer = 2; //floor -> layer 1 -> tag = 2¹;
		body.CollisionMask = 0; //player > layer 1 -> tag = 2⁰;        
		body.AddChild(collider);
		AddChild(body);
		collider.Translation = new Vector3(0, -0.19f, 0);
	}


	public override void _Ready()
	{
		// we do not want to save the in run time changes to the material so just clone it
		MaterialOverride = MaterialOverride != null ? (Material)MaterialOverride.Duplicate(true) : MaterialOverride;
		Multimesh = (MultiMesh)this.Multimesh.Duplicate(true);
		// generate the terrain with colliders and events
		GenerateTerrain();
	}
}
