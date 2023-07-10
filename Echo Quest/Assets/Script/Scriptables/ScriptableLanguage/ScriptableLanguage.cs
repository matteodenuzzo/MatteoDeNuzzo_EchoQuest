using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a language as a scriptable object.
/// </summary>
[CreateAssetMenu(menuName = "New Language")]
public class ScriptableLanguage : ScriptableObject
{
	#region Variables & Properties

	[SerializeField] public Language language;

	#endregion
}
