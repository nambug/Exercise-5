using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Code
{
	/// <summary>
	/// Just a simple component to keep track of the bounds of the arena
	/// and propose locations to respawn objects
	/// </summary>
	public class WallManager : MonoBehaviour
	{

		// Fill in within editor with walls defining the boundaries of the arena
		public GameObject LeftWall;
		public GameObject RightWall;
		public GameObject TopWall;
		public GameObject BottomWall;

		// Note the bounds of the arena.
		internal void Start ()
		{
			Camera cam = Camera.main;
			LeftWall.transform.position = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
			RightWall.transform.position = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
			TopWall.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
			BottomWall.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
		}
	}
}

