using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityGamesToolkit.Runtime;

public class LevelManager : Singleton<LevelManager>
{
	#region Variables & Properties

	[SerializeField] private ScriptableSaveData saveData;
	[SerializeField] public List<ScriptableBiome> AllPossiblesBiomes;
	[SerializeField] public int levelPassed = 0;
	[SerializeField] public ObstacleSpawner spawner;
	[SerializeField] public EnumDifficult currentDifficult;
	private bool inGame = true;
	[SerializeField] private S_VibrationDictionary vibrationDictionary;
	[SerializeField] private S_Vibration vibrationWhenLost;

	[Header("Numbers for next Biomes")]
	[SerializeField] public int biomeNumberAfterGoToNormal;
	[SerializeField] public int biomeNumberAfterGoToHard;
	private int currentPoints = 0;
	private Biome currentBiome;
	private float biomeTimePassed = 0;
	private bool obstaclePassed = false;
	private bool reproducingCTAFeedback = false;
	private Queue<S_Audio> cTAFeedbackQueue = new Queue<S_Audio>();
	private int clusterObstacleQuantity = 0;

	#endregion

	#region MonoBehaviour

	// Awake is called when the script instance is being loaded
	protected override void Awake()
	{
		base.Awake();
	}

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(NewBiome());
	}

	// Update is called once per frame
	void Update()
	{
		// Update logic here...
	}

	#endregion

	#region Methods

	private IEnumerator NewBiome()
	{
		currentBiome = RandomBiomeByDifficult(currentDifficult);
		yield return StartBiomeLevel(currentBiome);
	}

	private IEnumerator StartBiomeLevel(Biome biome)
	{
		UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(biome.sounds.activeDialogueNarrator);

		while (AudioSystem.Instance.IsAudioReproducing(biome.sounds.activeDialogueNarrator))
		{
			yield return null;
		}

		yield return new WaitForSeconds(biome.timeBeforeBiomeStarts);

		UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(biome.sounds.backGroundSound);
		UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(biome.sounds.footStepsSound);

		StartCoroutine(TimerBiomeLevel(biome));
		StartCoroutine(TimerObstacle(biome));
	}

	private IEnumerator TimerBiomeLevel(Biome biome)
	{
		biomeTimePassed = 0f;

		while (biomeTimePassed < biome.secondsInGame && inGame)
		{
			biomeTimePassed += Time.deltaTime;
			yield return null;
		}

		if (inGame)
		{
			EndGame(biome);
		}
	}

	private Queue<ScriptableObstacle> GetCluster(Biome biome)
	{
		return biome.GenerateCluster();
	}

	public static List<T> ConvertQueueToList<T>(Queue<T> queue)
	{
		return new List<T>(queue);
	}

	public static Queue<T> ConvertListToQueue<T>(List<T> list)
	{
		return new Queue<T>(list);
	}

	private IEnumerator TimerObstacle(Biome biome)
	{
		do
		{
			clusterObstacleQuantity = 0;
			reproducingCTAFeedback = false;

			yield return null;
			yield return null;
			yield return null;

			reproducingCTAFeedback = true;
			StartCoroutine(ReproduceCTAFeedbackQueue());

			List<S_Audio> clipGeneration = new List<S_Audio>();
			List<ScriptableObstacle> obstacleList = ConvertQueueToList(GetCluster(biome));
			clusterObstacleQuantity = obstacleList.Count;

			foreach (ScriptableObstacle m_obstacle in obstacleList)
			{
				clipGeneration.Add(m_obstacle.obstacle.gFeedback);
			}

			foreach (S_Audio clip in clipGeneration)
			{
				UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(clip);

				while (AudioSystem.Instance.IsAudioReproducing(clip))
				{
					yield return null;
				}
			}

			obstaclePassed = false;

			foreach (ScriptableObstacle m_obstacle in obstacleList)
			{
				spawner.SpawnObstacle(m_obstacle.obstacle);

				while (!obstaclePassed)
				{
					yield return null;
				}

				obstaclePassed = true;
			}

			UnityGamesToolkit.Runtime.EventManager.OnActiveVibration?.Invoke(vibrationDictionary.list.FindCorrespondingElementTo(clusterObstacleQuantity));

			yield return new WaitForSeconds(biome.secondsEveryTimeCouldSpawnACluster);

		} while (inGame && biome.secondsEveryTimeCouldSpawnACluster < (biome.secondsInGame - biomeTimePassed));

		Debug.Log("End Obstacles this Biome");
	}

	public void AddToQueueCTAFeedback(S_Audio clip)
	{
		cTAFeedbackQueue.Enqueue(clip);
	}

	private IEnumerator ReproduceCTAFeedbackQueue()
	{
		while (reproducingCTAFeedback)
		{
			while (cTAFeedbackQueue.Count > 0 && reproducingCTAFeedback)
			{
				S_Audio lastCTAFeedback = cTAFeedbackQueue.Dequeue();
				UnityGamesToolkit.Runtime.EventManager.OnPlayAudio?.Invoke(lastCTAFeedback);

				while (AudioSystem.Instance.IsAudioReproducing(lastCTAFeedback) && reproducingCTAFeedback)
				{
					yield return null;
				}
			}

			yield return null;
		}
	}

	public void Lost()
	{
		UnityGamesToolkit.Runtime.EventManager.OnStopAudio?.Invoke(currentBiome.sounds.activeDialogueNarrator);
		inGame = false;
		saveData.NewScore(currentPoints);
		GameManager.Instance.SetNewDifficultyObtained(currentDifficult);
		EventManager.SetAnimationTrigger(MenùFlowStateMachine.Instance.animator, "Lose");
		UnityGamesToolkit.Runtime.EventManager.OnActiveVibration?.Invoke(vibrationWhenLost);
		SceneManager.LoadScene("Menu");
	}

	private void EndGame(Biome biome)
	{
		IncreaseLevel();

		if (levelPassed == biomeNumberAfterGoToNormal && currentDifficult == EnumDifficult.Easy)
		{
			ResetLevelPassed();
			currentDifficult = EnumDifficult.Normal;
		}
		else if (levelPassed == biomeNumberAfterGoToHard && currentDifficult == EnumDifficult.Normal)
		{
			ResetLevelPassed();
			currentDifficult = EnumDifficult.Hard;
		}

		UnityGamesToolkit.Runtime.EventManager.OnStopAudio(biome.sounds.backGroundSound);
		UnityGamesToolkit.Runtime.EventManager.OnStopAudio(biome.sounds.footStepsSound);

		StartCoroutine(NewBiome());
	}

	private void ResetLevelPassed()
	{
		levelPassed = 0;
	}

	public void IncreaseLevel()
	{
		levelPassed++;
	}

	public void AddPoints(int points)
	{
		obstaclePassed = true;
		currentPoints += points;
	}

	private Biome RandomBiomeByDifficult(EnumDifficult difficult)
	{
		List<Biome> biomeList = new List<Biome>();

		foreach (ScriptableBiome biome in AllPossiblesBiomes)
		{
			if (biome.biome.difficulty.difficult == difficult)
			{
				biomeList.Add(biome.biome);
			}
		}

		if (currentBiome != null)
		{
			biomeList.Remove(currentBiome);
		}

		return biomeList[Random.Range(0, biomeList.Count)];
	}

	#endregion
}
