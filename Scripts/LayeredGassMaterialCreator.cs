using Godot;
using System;

public class LayeredGassMaterialCreator : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {        
        // add grass to the terrain  ? 
        /*var matList = new SpatialMaterial[NumGrassLayers + 1];
        // add grass layers
        for (int i = 1; i < NumGrassLayers + 1; i++)
        {
            var mat = (SpatialMaterial)GrassLayersMaterial.Duplicate(true);            
            mat.Uv1Scale = new Vector3(GrassTextureScale, GrassTextureScale, 1);
            //mat.Uv1Offset = new Vector3(GrassGrow * i,0, 0);
            mat.ParamsGrowAmount = GrassGrow * i;
            matList[i] = mat;
            mat.NextPass = matList[i - 1];
        }
        MaterialOverride.NextPass = matList[NumGrassLayers];*/
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
