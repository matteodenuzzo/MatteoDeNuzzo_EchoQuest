using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scriptable object that represents a collection of scriptable biomes.
/// </summary>
[CreateAssetMenu(menuName = "New Scriptable Biome Struct")]
public class ScriptableBiomeStruct : ScriptableObject
{
	#region Variables & Properties

	[SerializeField] public List<ScriptableBiome> allBiomes;

	#endregion
}
