using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class DialogueLanguageStruct
{
	#region Variables & Properties

	// The name of the dialogue state
	[SerializeField] public string nameState;

	// The audio clip for the entry dialogue
	[SerializeField] public S_Audio entryDialogue;
	#endregion
}
