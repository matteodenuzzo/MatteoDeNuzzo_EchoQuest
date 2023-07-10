using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolable : ObjectPoolable
{
    #region Variables & Properties

    private Obstacle obstacle;   // The Obstacle data associated with this poolable obstacle

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the obstacle with the specified Obstacle data.
    /// </summary>
    /// <param name="_obstacle">The Obstacle data to initialize the obstacle.</param>
    public void Init(Obstacle _obstacle)
    {
        obstacle = _obstacle.Clone();   // Create a clone of the Obstacle data to avoid modifying the original data
        StartCoroutine(TimerToCallToAction());   // Start the timer for the obstacle action
    }

    /// <summary>
    /// Timer for the obstacle action. Waits for a specific time and checks for player input or gestures.
    /// </summary>
    private IEnumerator TimerToCallToAction()
    {
        Debug.Log(obstacle.name + " CallToAction");

        LevelManager.Instance.AddToQueueCTAFeedback(obstacle.cTAFeedback);   // Add the Call To Action feedback to the Level Manager's queue

        float time = 0f;
        bool endTimer = false;
        while (time < obstacle.cTATimer && !endTimer)
        {
            time += Time.deltaTime;

            // Check for player input
            foreach (KeyCode key in obstacle.category.keyList)
            {
                if (Input.GetKeyDown(key))
                {
                    endTimer = true;
                }
            }

            // Check for player gesture
            if (GestureDetector.Instance.gesture.GetCurrentGesture() == obstacle.category.gesture)
            {
                endTimer = true;
            }

            yield return null;
        }

        if (endTimer)
        {
            Debug.Log(obstacle.name + " Passed!");
            UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(obstacle.passedFeedback);   // Play the passed feedback audio
            LevelManager.Instance.AddPoints(obstacle.points);   // Add points to the player's score
            yield return DeactiveAfterTime(0);   // Deactivate the obstacle after a specific time
        }
        else
        {
            Debug.Log("Oh no! The " + obstacle.name + " hit you! You Lost!");
            UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(obstacle.hitFeedback);   // Play the hit feedback audio
            LevelManager.Instance.Lost();   // Call the Lost() method in the Level Manager
        }
    }

    #endregion
}
