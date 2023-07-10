using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a scriptable cluster of obstacles.
/// </summary>
[CreateAssetMenu(menuName = "New Cluster")]
public class ScriptableCluster : ScriptableObject
{
	#region Variables & Properties

	[SerializeField] public Cluster cluster;

	#endregion

	/// <summary>
	/// Creates a deep copy of the cluster.
	/// </summary>
	/// <returns>A clone of the cluster.</returns>
	public Cluster GetClone()
	{
		Cluster clone = new Cluster();
		clone.obstacleList = cluster.obstacleList;
		return clone;
	}
}
