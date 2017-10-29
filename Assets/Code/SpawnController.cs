using UnityEngine;

/// <summary>
/// Just a simple component to keep track of the bounds of the arena
/// and propose locations to respawn objects
/// </summary>
public class SpawnController : MonoBehaviour
{

    // Fill in within editor with walls defining the boundaries of the arena
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject TopWall;
    public GameObject BottomWall;

	// Note the bounds of the arena.
	internal void Start ()
	{
		LeftWall.transform.position = new Vector3(0, 0, 0);
	}
}
