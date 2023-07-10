using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a scriptable object for menu states.
/// </summary>
[CreateAssetMenu(menuName = "Menu States")]
public class ScriptableStructMenùState : ScriptableObject
{
	#region Variables & Properties

	// List of menu states
	[SerializeField] public List<ScriptableMenùState> menùStateList;

	// List of menu states where input is enabled
	[SerializeField] public List<ScriptableMenùState> menùStateListWhereCanBePressedInput;

	#endregion
}
