using Godot;
using System;

[Tool]
public class PlayerCamera : InterpolatedCamera
{

	[Export]
	float distance = 2;
	[Export]
	float height = .5f;
	[Export]
	float rotationSpeed = 50;

	Vector2? prevCoor;
	static Vector3 GROUND_VECTOR = Vector3.Up;
	Spatial lookAtTarget;
	
	public override void _Ready()
	{
		lookAtTarget = GetParent().GetNode<Spatial>("LookAt");
		RotateCameraAroundTarget(0, 0);
	}

	public override void _Process(float delta)
	{
		if (Engine.EditorHint) 
		{
			RotateCameraAroundTarget(0, 0);
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		lookAtTarget = GetParent().GetNode<Spatial>("LookAt");
		if (Input.IsActionPressed("rotate_start"))
		{
			// rotate start
			if (!prevCoor.HasValue)
			{
				prevCoor = GetViewport().GetMousePosition();
			}
			else
			{
				// actual rotation
				var coor = GetViewport().GetMousePosition();
				var diff = (coor - prevCoor.Value) * rotationSpeed * delta;
				prevCoor = coor;
				RotateCameraAroundTarget(diff.x, 0);
			}
		}
		else
		{
			prevCoor = null;
		}
	}

	void RotateCameraAroundTarget(float deltaX, float deltaY)
	{
		var angle = deltaX;
		// rotate around Y
		var point = new Vector2(GlobalTransform.origin.x, GlobalTransform.origin.z);
		var center = new Vector2(lookAtTarget.GlobalTransform.origin.x, lookAtTarget.GlobalTransform.origin.z);

		angle = (angle) * (Mathf.Pi / 180); // Convert to radians

		var rotatedX = Mathf.Cos(angle) * (point.x - center.x) - Mathf.Sin(angle) * (point.y - center.y) + center.x;
		var rotatedZ = Mathf.Sin(angle) * (point.x - center.x) + Mathf.Cos(angle) * (point.y - center.y) + center.y;
		//Translation = new Vector3(rotatedX, Translation.y, rotatedZ);
		//Transform = new Transform(Transform.basis, new Vector3(rotatedX, Transform.origin.y, rotatedZ));
		var target = lookAtTarget.GlobalTransform.origin;
		var pos = new Vector3(rotatedX, Transform.origin.y, rotatedZ);
		var offset = pos - target;
		offset = offset.Normalized() * distance;
		offset.y = height;
		pos = target + offset;

		LookAtFromPosition(pos, target, GROUND_VECTOR);
	}
}
