using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an identifier for a state in a state machine.
/// </summary>
[System.Serializable]
public class StateIdentifier
{
	#region Variables & Properties

	// The name of the state
	[SerializeField] public string name;

	// The hash code of the state
	[SerializeField] public int hashCode;

	#endregion
}
