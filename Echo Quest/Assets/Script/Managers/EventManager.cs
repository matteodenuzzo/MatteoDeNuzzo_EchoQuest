using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityGamesToolkit.Runtime;

public static class EventManager
{
	#region Variables & Properties

	public delegate void Evt();
	public delegate void Evt<T>(T obj);
	public delegate void Evt<T, T1>(T obj, T1 obj1);

	// Event triggered when an audio clip ends
	public static Evt<S_Audio> EndClip;
	// Event triggered when a new biome starts
	public static Evt<Biome> StartBiome;
	// Event triggered when a biome ends
	public static Evt EndBiome;
	// Event triggered when a new language is set
	public static Evt<ScriptableLanguage> OnSetNewLanguage;
	// Event triggered to check if the tutorial is done
	public static Evt onCheckTutorialDone;
	// Event triggered to check if the language is already set
	public static Evt onCheckLanguageEverSetted;
	// Event triggered when the game starts
	public static Evt StartGame;
	// Event triggered to set an animation trigger
	public static Evt<Animator, string> SetAnimationTrigger;

	#endregion

	[RuntimeInitializeOnLoadMethod]
	private static void Init()
	{
		SetAnimationTrigger += OnActiveTrigger;
		UnityGamesToolkit.Runtime.EventManager.OnStopAudio += EndClipFunction;
	}

	/// <summary>
	/// Sets an animation trigger on the specified animator.
	/// </summary>
	/// <param name="animator">The animator to set the trigger on.</param>
	/// <param name="trigger">The name of the trigger.</param>
	private static void OnActiveTrigger(Animator animator, string trigger)
	{
		animator.SetTrigger(trigger);
	}

	/// <summary>
	/// Invokes the EndClip event when an audio clip ends.
	/// </summary>
	/// <param name="audio">The audio clip that ended.</param>
	private static void EndClipFunction(S_Audio audio)
	{
		EndClip.Invoke(audio);
	}
}
