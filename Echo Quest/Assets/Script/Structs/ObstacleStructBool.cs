using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleStructBool
{
	#region Variables & Properties

	// Name of the obstacle
	[SerializeField] public string obstacleName;

	// Indicates if the obstacle has ever been completed in the tutorial
	[SerializeField] public bool everDoneInTutorial;

	#endregion
}
