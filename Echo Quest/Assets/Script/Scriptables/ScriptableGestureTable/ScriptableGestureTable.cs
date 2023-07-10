using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a gesture table as a scriptable object.
/// </summary>
[CreateAssetMenu(menuName = "Gesture Table")]
public class ScriptableGestureTable : ScriptableObject
{
	#region Variables & Properties

	[SerializeField] public List<GestureTable> gestureTable;

	#endregion
}
