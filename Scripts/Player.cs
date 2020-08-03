using Godot;
using System;

public class Player : KinematicBody
{
	Vector3 linearVelocity = new Vector3();
	static Vector3 GROUND_VECTOR = Vector3.Up;


	Camera cam;

	Spatial armature;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cam = GetNode<Camera>("Camera");
		armature = GetNode<Spatial>("Armature");

	}
	// Horizontal velocity.
	Vector3 hv = Vector3.Zero;
	// Vertical velocity.
	float vv = 0;

	Vector3 hdir = Vector3.Back;
	float hspeed = 0;
	float walkSpwed = 100;
	// float downSpwed = -9.8f;



	public override void _PhysicsProcess(float delta)
	{
		//linearVelocity.y += delta * downSpwed;
		// Vertical velocity.
		vv = -delta;// linearVelocity.y;
		// Horizontal velocity.
		hv = new Vector3(linearVelocity.x, 0, linearVelocity.z);
		hspeed = walkSpwed * delta;// constant speed no accelaraion

		// player input (movement direction)
		var camBasis = cam.GlobalTransform.basis;
		var dir = new Vector3(); // Where does the player intend to walk to.
		dir = (Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left")) * camBasis.x;
		dir += (Input.GetActionStrength("move_backwards") - Input.GetActionStrength("move_forward")) * camBasis.z;
		dir.y = 0;
		dir = dir.Normalized();

		if (IsOnFloor())
		{
			// if is moving then calculate direction of movement
			if (dir.Length() > 0.001)
			{
				//hdir = hdir.LinearInterpolate(dir, .5f);
				// get angle between hdir and dir
				var yRot = AdjustFacingRadian(hdir, dir, GROUND_VECTOR);
				// if angle chages apply it
				if (yRot.HasValue)
				{
					//armature.Rotation.y
					armature.RotateY(yRot.Value);
				}
				hdir = dir;
			}
			else
			{
				hspeed = 0;
			}

			// move player
			hv = hdir * hspeed;
		}

		linearVelocity = hv + Vector3.Up * vv;
		linearVelocity = MoveAndSlide(linearVelocity, GROUND_VECTOR, true);

		// check collisions, for now the mask is set to only floor 

	}

	public float? AdjustFacingRadian(Vector3 currentFacing, Vector3 targetFacing, Vector3 ground)
	{
		var n = targetFacing; // Normal.							 
		var t = n.Cross(ground).Normalized();


		float x = n.Dot(currentFacing);
		float y = t.Dot(currentFacing);
		float ang = Mathf.Atan2(y, x);
		// to small diff, nothing to adjust
		if (Math.Abs(ang) < 0.001f)
		{
			return null;
		}

		//int s = Mathf.Sign(ang);
		//ang = ang;// * s;
		return ang;
	}
}
