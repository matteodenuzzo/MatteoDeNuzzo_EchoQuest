using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a scriptable object for defining difficulty levels.
/// </summary>
[CreateAssetMenu(menuName = "New Difficulty")]
public class Difficulty : ScriptableObject
{
	#region Variables & Properties

	[SerializeField] public EnumDifficult difficult;

	#endregion
}
