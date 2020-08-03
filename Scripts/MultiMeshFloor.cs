using Godot;
using System;

[Tool]
public class MultiMeshFloor : MultiMeshInstance
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Create the multimesh.
		// Multimesh = new MultiMesh();
		// Set the format first.
		Multimesh.TransformFormat = MultiMesh.TransformFormatEnum.Transform3d;
        //Multimesh.ColorFormat = (MultiMesh.ColorFormatEnum) 1; //COLOR_8BIT
        //Multimesh.CustomDataFormat = (MultiMesh.CustomDataFormatEnum)1;//CUSTOM_DATA_8BIT
        Multimesh.VisibleInstanceCount = Multimesh.InstanceCount;
        var length = Math.Sqrt(Multimesh.VisibleInstanceCount);
        // display in grid like approach
        int count = 0;		
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j< length; j++) 
			{
				Multimesh.SetInstanceTransform(count, new Transform(Basis.Identity, new Vector3(.5f+i, 0, .5f+j)));
                if (count == 0)
                {
                    Multimesh.SetInstanceColor(count, new Color(.55f, .55f, .55f, .55f));
                } else 
                {
                    Multimesh.SetInstanceColor(count, new Color(0, 0, 0, 0));
                }
                count++;

            }
			
		}
	}

    int i = 0;
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
        if (i > 0)
            Multimesh.SetInstanceColor(i-1, new Color(0, 0, 0, 0));
        else
            Multimesh.SetInstanceColor(Multimesh.VisibleInstanceCount-1, new Color(0, 0, 0, 0));
        Multimesh.SetInstanceColor(i, new Color(.55f, .55f, .55f, .55f));
        i++;
        i = i >= Multimesh.VisibleInstanceCount ? 0 : i;
    }
}
