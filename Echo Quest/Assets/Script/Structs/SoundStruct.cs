using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class SoundStruct
{
	#region Variables & Properties

	// Background sound for the scene
	[SerializeField] public S_Audio backGroundSound;

	// Sound for player footsteps
	[SerializeField] public S_Audio footStepsSound;

	[Header("Narrative dialogue that change depending on language: DONT CHANGE IN INSPECTOR")]
	// Active narrative dialogue based on language
	[SerializeField] public S_Audio activeDialogueNarrator;

	#endregion
}
