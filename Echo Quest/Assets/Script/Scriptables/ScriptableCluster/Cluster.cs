using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a cluster of obstacles.
/// </summary>
[System.Serializable]
public class Cluster
{
	#region Variables & Properties

	[SerializeField] public List<ScriptableObstacle> obstacleList;

	#endregion
}
