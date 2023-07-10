using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a scriptable object for an obstacle category.
/// </summary>
[CreateAssetMenu(menuName = "Obstacle Category")]
public class ScriptableObstacleCategory : ScriptableObject
{
	#region Variables & Properties

	// The gesture associated with the obstacle category
	[SerializeField] public GestureType gesture;

	// The list of key codes associated with the obstacle category
	[SerializeField] public List<KeyCode> keyList;

	#endregion
}
