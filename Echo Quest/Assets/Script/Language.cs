using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class Language
{
	#region Variables & Properties

	[Header("Menu Language Dialogue")]
	// List of dialogue structures for menu states
	public List<DialogueLanguageStruct> menùStatesDialogues;

	[Header("Biome Entry Dialogue")]
	// List of biome entry dialogue structures
	public List<BiomeEntryDialogueStruct> biomeEntryDialogue;

	public S_Audio allObstacleCompletedAudio;

	[Header("Audios if at least one obstacle not completed")]
	public S_Audio incipitAudio;

	// List of ClusterEntryDialogue entry dialogue structures
	public StateDictionary clusterEntryDialogue;

	#endregion
}
