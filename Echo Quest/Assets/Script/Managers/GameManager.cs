using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

public class GameManager : Singleton<GameManager>
{
	#region Variables & Properties

	// The scriptable struct representing the state of the menu.
	[SerializeField] public ScriptableStructMenùState menùStateStruct;

	// The scriptable struct representing the biome.
	[SerializeField] public ScriptableBiomeStruct biomeStruct;

	// The list of scriptable languages.
	[SerializeField] public List<ScriptableLanguage> languageList;

	// The scriptable save data.
	[SerializeField] public ScriptableSaveData saveData;

	// The maximum difficulty obtained.
	[SerializeField] public S_Container_Data_Float maxDifficultObtained;

	// The list of difficulty translators.
	[SerializeField] public List<BiClass<EnumDifficult, float>> difficultTranslator;

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
		// Invoke events related to language and game start.
		EventManager.OnSetNewLanguage.Invoke(saveData.mainLanguage);
		EventManager.StartGame?.Invoke();
		EventManager.onCheckTutorialDone.Invoke();
		EventManager.onCheckLanguageEverSetted.Invoke();
	}

	// Update is called once per frame
	void Update()
	{
		// Update logic here...
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventManager.OnSetNewLanguage += SetNewLanguage;
	}

	private void OnDisable()
	{
		EventManager.OnSetNewLanguage -= SetNewLanguage;
	}

	#endregion

	#region Methods

	// Summary:
	// Sets a new language and updates the corresponding dialogues.
	private void SetNewLanguage(ScriptableLanguage language)
	{
		// Update the main language in the save data.
		saveData.mainLanguage = language;

		// Update the entry dialogues for each menu state.
		foreach (DialogueLanguageStruct dialogue in language.language.menùStatesDialogues)
		{
			foreach (ScriptableMenùState menuState in menùStateStruct.menùStateList)
			{
				if (menuState.menùState.name == dialogue.nameState)
				{
					menuState.menùState.entryDialogue = dialogue.entryDialogue;
				}
			}
		}

		// Update the active dialogue narrator for each biome.
		foreach (BiomeEntryDialogueStruct dialogue in language.language.biomeEntryDialogue)
		{
			foreach (ScriptableBiome biome in biomeStruct.allBiomes)
			{
				if (biome == dialogue.biome)
				{
					biome.biome.sounds.activeDialogueNarrator = dialogue.biomeEntryDialogue;
				}
			}
		}
	}

	// Summary:
	// Marks the tutorial as done.
	public void TutorialDone()
	{
		saveData.tutorialEverDone = true;
	}

	// Summary:
	// Sets the new difficulty obtained based on the current difficulty level.
	public void SetNewDifficultyObtained(EnumDifficult currentDifficult)
	{
		// Find the corresponding element in the difficulty translator and set the new record.
		maxDifficultObtained.content.currentValue=(difficultTranslator.FindCorrespondingElementTo(currentDifficult));
	}

	#endregion
}
