using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a gesture table entry.
/// </summary>
[System.Serializable]
public class GestureTable
{
	#region Variables & Properties

	[SerializeField] public int reference;
	[SerializeField] public GestureType gestureType;

	#endregion
}
