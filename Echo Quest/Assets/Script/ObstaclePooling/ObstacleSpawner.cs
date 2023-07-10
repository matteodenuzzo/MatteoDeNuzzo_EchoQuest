using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	#region Variables & Properties

	// Pooler for managing obstacle objects
	[SerializeField] public ObstaclePooler pooler;

	#endregion

	#region Methods

	/// <summary>
	/// Spawns an obstacle using the specified Obstacle object.
	/// </summary>
	/// <param name="obstacle">The Obstacle object to spawn.</param>
	public void SpawnObstacle(Obstacle obstacle)
	{
		// Initialize and spawn the obstacle from the pooler
		pooler.InitObstacle(obstacle);
	}

	#endregion
}
