using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : ObjectPooler
{
	#region Variables & Properties

	// No specific variables or properties defined in this class

	#endregion

	#region Methods

	/// <summary>
	/// Initializes and spawns an obstacle from the object pooler using the specified Obstacle object.
	/// </summary>
	/// <param name="obstacle">The Obstacle object to initialize and spawn.</param>
	public void InitObstacle(Obstacle obstacle)
	{
		// Spawn an obstacle object from the object pooler
		GameObject obstacleObj = SpawnObjectPoolable<ObstaclePoolable>();
		// Initialize the obstacle object with the specified Obstacle data
		obstacleObj.GetComponent<ObstaclePoolable>().Init(obstacle);
	}

	#endregion
}
