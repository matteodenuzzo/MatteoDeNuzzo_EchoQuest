using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class DialogueObstacleStruct
{
	#region Variables & Properties

	// Audio feedback for gesture action in obstacle dialogue
	[SerializeField] public S_Audio dialogueGFeedback;

	// Audio feedback for call-to-action action in obstacle dialogue
	[SerializeField] public S_Audio dialogueCTAFeedback;

	#endregion
}
