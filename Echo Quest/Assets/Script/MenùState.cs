using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class MenùState
{
	#region Variables & Properties

	// Name of the menu state
	public string name;
	[HideInInspector]
	// Entry dialogue audio for the menu state
	public S_Audio entryDialogue;

	// Event triggered when the menu state starts
	[SerializeField] public UnityEvent OnStartState;

	// Event triggered when the menu state is updated
	[SerializeField] public UnityEvent OnUpdateState;

	// Event triggered when the menu state ends
	[SerializeField] public UnityEvent OnEndState;

	#endregion
}
