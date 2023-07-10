using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class Biome : IClonable<Biome>
{
    #region Variables & Properties

    // Duration of the biome in seconds
    [SerializeField] public float secondsInGame;

    // Sound configuration for the biome
    [SerializeField] public SoundStruct sounds;

    // List of all possible clusters for the biome
    [SerializeField] public List<ScriptableCluster> allPossibleClusters;

    // Difficulty level of the biome
    [SerializeField] public Difficulty difficulty;

    // Time before the biome starts
    [SerializeField] public float timeBeforeBiomeStarts;


    [Header("Spawn Obstacle System")]

    // Time interval for spawning a cluster of obstacles
    [SerializeField] public float secondsEveryTimeCouldSpawnACluster;

    // Percentage chance for a cluster to spawn
    [SerializeField] public float percentageClusterCouldSpawn;

    [Header("No Cluster List")]
    // List of possible obstacle numbers for generating a cluster
    [SerializeField] private List<int> possibleObstacleNumberForGeneratingCluster;

    // List of possible obstacles for generating a cluster
    [SerializeField] private List<ScriptableObstacle> possibleObstacleForGeneratingCluster;

    #endregion

    #region Methods

    /// <summary>
    /// Clones the biome instance.
    /// </summary>
    /// <returns>The cloned biome instance.</returns>
    public Biome Clone()
    {
        Biome clone = new Biome();
        clone.secondsInGame = secondsInGame;
        clone.timeBeforeBiomeStarts = timeBeforeBiomeStarts;
        clone.sounds = sounds;
        foreach (ScriptableCluster cluster in allPossibleClusters)
        {
            clone.allPossibleClusters.Add(cluster);
        }

        clone.secondsEveryTimeCouldSpawnACluster = secondsEveryTimeCouldSpawnACluster;
        clone.percentageClusterCouldSpawn = percentageClusterCouldSpawn;
        clone.difficulty = difficulty;
        return clone;
    }

    /// <summary>
    /// Generates a cluster of obstacles at runtime.
    /// </summary>
    /// <returns>The queue of obstacles in the generated cluster.</returns>
    public Queue<ScriptableObstacle> GenerateClusterRuntime()
    {
        Queue<ScriptableObstacle> obstacleQueue = new Queue<ScriptableObstacle>();

        int obstacleNumber = possibleObstacleNumberForGeneratingCluster[Random.Range(0, possibleObstacleNumberForGeneratingCluster.Count)];

        for (int i = 0; i < obstacleNumber; i++)
        {
            obstacleQueue.Enqueue(possibleObstacleForGeneratingCluster.RandomElement());
        }

        return obstacleQueue;
    }

    /// <summary>
    /// Generates a cluster of obstacles based on the pre-defined clusters or generates a runtime cluster if no pre-defined clusters exist.
    /// </summary>
    /// <returns>The queue of obstacles in the generated cluster.</returns>
    public Queue<ScriptableObstacle> GenerateCluster()
    {
        if (allPossibleClusters.Count == 0)
        {
            return GenerateClusterRuntime();
        }

        return ConvertListToQueue(allPossibleClusters[Random.Range(0, allPossibleClusters.Count)].cluster.obstacleList);
    }

    /// <summary>
    /// Converts a queue to a list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    /// <param name="queue">The queue to convert.</param>
    /// <returns>The converted list.</returns>
    public static List<T> ConvertQueueToList<T>(Queue<T> queue)
    {
        return new List<T>(queue);
    }

    /// <summary>
    /// Converts a list to a queue.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to convert.</param>
    /// <returns>The converted queue.</returns>
    public static Queue<T> ConvertListToQueue<T>(List<T> list)
    {
        return new Queue<T>(list);
    }

    #endregion
}
