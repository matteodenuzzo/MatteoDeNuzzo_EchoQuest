using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a state machine for menu states.
/// </summary>
[CreateAssetMenu(menuName = "New State Machine")]
public class State : ScriptableObject
{
	#region Variables & Properties

	// The current menu state
	[SerializeField] public ScriptableMenùState currentState;

	// The first menu state
	[SerializeField] public ScriptableMenùState firstState;

	// All menu states
	[SerializeField] public ScriptableStructMenùState allStates;

	// List of state identifiers
	[SerializeField] public List<StateIdentifier> statesList;

	#endregion

	#region Methods

	/// <summary>
	/// Adds a new state to the state machine.
	/// </summary>
	/// <param name="hashCode">The hash code of the state.</param>
	/// <param name="name">The name of the state.</param>
	public void AddState(int hashCode, string name)
	{
		bool found = false;

		foreach (StateIdentifier identifier in statesList)
		{
			if (identifier.name == name && identifier.hashCode == hashCode)
			{
				found = true;
			}
		}

		if (found)
		{
			foreach (StateIdentifier identifier in statesList)
			{
				if (identifier.name == name && identifier.hashCode == hashCode)
				{
					// Perform some action
				}
			}
		}

		if (!found)
		{
			StateIdentifier identifier = new StateIdentifier();
			identifier.name = name;
			identifier.hashCode = hashCode;
			statesList.Add(identifier);
		}
	}

	/// <summary>
	/// Sets the current state of the state machine.
	/// </summary>
	/// <param name="name">The name of the state.</param>
	public void SetCurrentState(string name)
	{
		foreach (ScriptableMenùState state in allStates.menùStateList)
		{
			if (state.menùState.name == name)
			{
				currentState = state;
			}
		}
	}

	/// <summary>
	/// Gets the hash code of a given state.
	/// </summary>
	/// <param name="state">The menu state.</param>
	/// <returns>The hash code of the state.</returns>
	public int GetHashByState(ScriptableMenùState state)
	{
		foreach (StateIdentifier identifier in statesList)
		{
			if (state.menùState.name == identifier.name)
			{
				return identifier.hashCode;
			}
		}

		return 0;
	}

	#endregion
}
