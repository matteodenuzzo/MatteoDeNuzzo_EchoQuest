using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class Obstacle : IClonable<Obstacle>
{
	#region Variables & Properties

	[HideInInspector]
	// Name of the obstacle
	public string name;
	// Reference to the ScriptableObstacle
	[SerializeField] public ScriptableObstacle self;
	// Points awarded for overcoming the obstacle
	[SerializeField] public int points;
	// Category of the obstacle
	[SerializeField] public ScriptableObstacleCategory category;

	[Header("Timers")]
	// Timer for the gesture action
	[SerializeField] public float gTimer;
	// Timer for the Call To Action action
	[SerializeField] public float cTATimer;

	[Header("Clips")]
	// Feedback audio for the gesture action
	[SerializeField] public S_Audio gFeedback;
	// Feedback audio for the Call To Action action
	[SerializeField] public S_Audio cTAFeedback;
	// Feedback audio when the obstacle hits the player
	[SerializeField] public S_Audio hitFeedback;
	// Feedback audio when the obstacle is successfully passed
	[SerializeField] public S_Audio passedFeedback;

	#endregion

	#region Methods

	/// <summary>
	/// Creates a clone of the obstacle.
	/// </summary>
	/// <returns>A new instance of the Obstacle with the same values as the original.</returns>
	public Obstacle Clone()
	{
		Obstacle clone = new Obstacle();
		clone.name = self.ToString();
		clone.points = points;
		clone.category = category;
		clone.gFeedback = gFeedback;
		clone.cTAFeedback = cTAFeedback;
		clone.hitFeedback = hitFeedback;
		clone.passedFeedback = passedFeedback;
		clone.gTimer = gTimer;
		clone.cTATimer = cTATimer;
		return clone;
	}

	#endregion
}
