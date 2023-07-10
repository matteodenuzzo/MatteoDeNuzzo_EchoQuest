
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityGamesToolkit.Runtime;

[CreateAssetMenu(menuName = "New Menu State")]
public class ScriptableMenùState : ScriptableObject
{

#region Variables & Properties

[SerializeField] public MenùState menùState;
[SerializeField] public bool repeatDialogueOnEnd;

#endregion

#region Methods

public void ReproduceClusterSoundObstaclesInGame()
{
	MenùFlowStateMachine.Instance.UpgradeAudioObstaclesInGameAndPlay();
}

public void StopAudio(S_AudioCollector audios)
{
	foreach (S_Audio audio in audios.list)
	{
		UnityGamesToolkit.Runtime.EventManager.OnStopAudio?.Invoke(audio);
	}
}

public void DoVibration(S_Vibration vibration)
{
	UnityGamesToolkit.Runtime.EventManager.OnActiveVibration?.Invoke(vibration);
}

public void ResetAllData()
{
	TimerManager.Instance.ResetAllData();
	GameManager.Instance.saveData.Reset();
}

public void StartCount(S_Container_Data_Float data)
{
	TimerManager.Instance.StartCount(data);
}

public void EndCount(S_Container_Data_Float data)
{
	TimerManager.Instance.EndCount(data);
}

public void StartCountForRecord(S_Container_Data_Float data)
{
	TimerManager.Instance.StartCountForRecord(data);
}

public void EndCountForRecord(S_Container_Data_Float data)
{
	TimerManager.Instance.EndCountForRecord(data);
}

public void ReproduceEntryDialogue()
{
	UnityGamesToolkit.Runtime.EventManager.OnPlayAudio(menùState.entryDialogue);
}

public void StopEntryDialogue()
{
	if (AudioSystem.Instance.clipInExecution.ContainsKey(menùState.entryDialogue))
	{
		AudioSystem.Instance.clipInExecution[menùState.entryDialogue].Mute();
	}
}

public void OpenMenu()
{
	SceneManager.LoadScene("Menu");
}

public void OpenLevelGame()
{
	SceneManager.LoadScene("Game");
}

public void OpenLevelTutorial()
{
	SceneManager.LoadScene("Tutorial");
}

public void SetTutorialEverDone()
{
	GameManager.Instance.TutorialDone();
	EventManager.onCheckTutorialDone.Invoke();
}

public void SetLanguage(ScriptableLanguage newLanguage)
{
	EventManager.OnSetNewLanguage(newLanguage);
}

public void IncreaseAudioChannelVolume0Dot1(S_AudioChannel channel)
{
	channel.content.IncreaseVolumeOf(0.1f);
}

public void DecreaseAudioChannelVolume0Dot1(S_AudioChannel channel)
{
	channel.content.DecreaseVolumeOf(0.1f);
}

public void SetAlreadySelectedALanguage()
{
	GameManager.Instance.saveData.languageAlreadySetted = true;
	EventManager.onCheckLanguageEverSetted.Invoke();
}

public void ObstaclePassed(string obstacle)
{
	GameManager.Instance.saveData.SetObstaclePassedInTutorial(obstacle);
	if (GameManager.Instance.saveData.EveryObstaclePassed())
	{
		SetTutorialEverDone();
	}
}

	public void CloseApp()
{
	Application.Quit();
#if UNITY_EDITOR
	EditorApplication.ExitPlaymode();
#endif
}
#endregion

}
