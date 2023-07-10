using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLibrary : MonoBehaviour
{
	#region Variables & Properties

	// No variables or properties in this class

	#endregion

	#region Methods

	/// <summary>
	/// Opens the menu scene.
	/// </summary>
	public void OpenMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	/// <summary>
	/// Opens the game scene.
	/// </summary>
	public void OpenGame()
	{
		SceneManager.LoadScene("Game");
	}

	#endregion
}
