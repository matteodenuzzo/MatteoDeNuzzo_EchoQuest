using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a scriptable object for an obstacle.
/// </summary>
[CreateAssetMenu(menuName = "Obstacle")]
public class ScriptableObstacle : ScriptableObject
{
	#region Variables & Properties

	// The obstacle data
	[SerializeField] public Obstacle obstacle;

	#endregion
}
