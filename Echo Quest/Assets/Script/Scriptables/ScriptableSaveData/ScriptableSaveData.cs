using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object that holds the save data for the game.
/// </summary>
[CreateAssetMenu(menuName = "New Save Data")]
public class ScriptableSaveData : ScriptableObject
{
	#region Variables & Properties

	[Header("Scores")]
	[SerializeField] public int maxScoreNumber;

	[Header("Main Settings")]
	[SerializeField] public ScriptableLanguage mainLanguage;

	[Header("Ever Done")]
	[SerializeField] public bool tutorialEverDone;
	[SerializeField] public bool languageAlreadySetted;

	[Header("Tutorial")]
	[SerializeField] public List<ObstacleStructBool> obstacleStructBool;

	#endregion

	#region Methods

	/// <summary>
	/// Resets the save data by setting all obstacle flags to false and resetting the scores.
	/// </summary>
	public void Reset()
	{
		foreach (ObstacleStructBool m_struct in obstacleStructBool)
		{
			m_struct.everDoneInTutorial = false;
		}

		maxScoreNumber = 0;
		tutorialEverDone = false;
		languageAlreadySetted = false;
	}

	/// <summary>
	/// Sets the languageAlreadySetted flag to true.
	/// </summary>
	public void SetTrueAlreadySelectedALanguage()
	{
		languageAlreadySetted = true;
	}

	/// <summary>
	/// Sets the everDoneInTutorial flag for the specified obstacle to true.
	/// </summary>
	/// <param name="obstacleName">The name of the obstacle</param>
	public void SetObstaclePassedInTutorial(string obstacleName)
	{
		foreach (ObstacleStructBool obstacle in obstacleStructBool)
		{
			if (obstacle.obstacleName == obstacleName)
			{
				obstacle.everDoneInTutorial = true;
				break;
			}
		}
	}

	/// <summary>
	/// Checks if every obstacle has been passed in the tutorial.
	/// </summary>
	/// <returns>True if every obstacle has been passed, false otherwise</returns>
	public bool EveryObstaclePassed()
	{
		bool passed = true;
		foreach (ObstacleStructBool obstacle in obstacleStructBool)
		{
			if (!obstacle.everDoneInTutorial)
			{
				passed = false;
			}
		}
		return passed;
	}

	/// <summary>
	/// Sets the mainLanguage to the specified newLanguage.
	/// </summary>
	/// <param name="newLanguage">The new language</param>
	public void NewLanguage(ScriptableLanguage newLanguage)
	{
		mainLanguage = newLanguage;
	}

	/// <summary>
	/// Updates the maxScoreNumber if the specified points is higher.
	/// </summary>
	/// <param name="points">The new score</param>
	public void NewScore(int points)
	{
		if (points > maxScoreNumber)
		{
			maxScoreNumber = points;
		}
	}
	#endregion
}
