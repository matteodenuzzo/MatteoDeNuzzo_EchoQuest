using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scriptable object that represents a biome in the game.
/// </summary>
[CreateAssetMenu(menuName = "New Biome")]
public class ScriptableBiome : ScriptableObject
{

	#region Variables & Properties

	[SerializeField] public Biome biome;

	#endregion

}
