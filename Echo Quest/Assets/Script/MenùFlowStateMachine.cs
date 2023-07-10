
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;
using UnityGamesToolkit.Runtime;

public class MenùFlowStateMachine : Singleton<MenùFlowStateMachine>
{

	// The Animator component used to control the menu flow
    public Animator animator;

    // The hash code of the current state
    private int currentStateHash = 0;

	// The state machine for managing menu states
    [SerializeField] private State stateMachine;

    // The collector of menu states
    [SerializeField] private ScriptableStructMenùState menuStateCollector;

    // The table of gestures and their corresponding numbers
    [SerializeField] private ScriptableGestureTable gestureTable;

    // Flag indicating if menu input is allowed
    private bool pressInput = true;

    // The currently active menu state
    private ScriptableMenùState currentState = null;

    // The current narrator voice audio clip
    private S_Audio narratorVoice;

    // The dictionary mapping state hash codes to state names
    private Dictionary<int, string> stateHashNameDict = new Dictionary<int, string>();

    [SerializeField] private S_AudioCluster obstaclesInGame;

    private bool narratorVoicInExecuting = false;

    [SerializeField] private float waitingTimeBeforeDialogue = 16f;

    private ScriptableMenùState lastState = null;

    private S_Audio exNarratorVoice;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        EventManager.EndClip += EndNarratorVoice;
        EventManager.StartGame += InitAnimator;
        EventManager.onCheckTutorialDone += CheckTutorialEverDone;
        EventManager.onCheckLanguageEverSetted += CheckLanguageAlreadySetted;
        UnityGamesToolkit.Runtime.EventManager.OnStopAudioCluster += EndClusterVoice;
    }

    private void OnDisable()
    {
        EventManager.EndClip -= EndNarratorVoice;
        EventManager.StartGame -= InitAnimator;
        EventManager.onCheckTutorialDone -= CheckTutorialEverDone;
        EventManager.onCheckLanguageEverSetted -= CheckLanguageAlreadySetted;
        UnityGamesToolkit.Runtime.EventManager.OnStopAudioCluster -= EndClusterVoice;
    }

    private void Start()
    {
    }

    private void Update()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).nameHash);

        Singletonn.Instance.CurrentState.text = "Current State: " + stateMachine.currentState.menùState.name;

        if ((pressInput && GetNumberByTable() != 0) || menuStateCollector.menùStateListWhereCanBePressedInput.Contains(stateMachine.currentState))
        {
            SetGestureNumber(GetNumberByTable());
        }

        // If the current state has changed, trigger the corresponding events
        int newStateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (newStateHash != currentStateHash)
        {

            animator.SetInteger("Gesture", 0);
            NewState(stateHashNameDict[newStateHash], stateHashNameDict[currentStateHash]);
            currentStateHash = newStateHash;
            Debug.Log("New state: " + stateHashNameDict[currentStateHash]);
            stateMachine.SetCurrentState(stateHashNameDict[currentStateHash]);
        }

    }

    /// <summary>
    /// Checks if the tutorial has been completed and updates the animator parameter accordingly.
    /// </summary>
    private void CheckTutorialEverDone()
    {
        animator.SetBool("Tutorial_Ever_Done", GameManager.Instance.saveData.tutorialEverDone);
    }

    /// <summary>
    /// Checks if the language has been already set and updates the animator parameter accordingly.
    /// </summary>
    private void CheckLanguageAlreadySetted()
    {
        animator.SetBool("Already_Selected_A_Language", GameManager.Instance.saveData.languageAlreadySetted);
    }

    /// <summary>
    /// Gets the gesture number based on the current gesture from the gesture table.
    /// </summary>
    /// <returns>The gesture number corresponding to the current gesture.</returns>
    private int GetNumberByTable()
    {
        GestureType gesture = GestureDetector.Instance.gesture.GetCurrentGesture();

        foreach (GestureTable table in gestureTable.gestureTable)
        {
            if (gesture == table.gestureType)
            {
                return table.reference;
            }
        }

        return 0;
    }

    /// <summary>
    /// Initializes the animator and sets up the state machine.
    /// </summary>
    private void InitAnimator()
    {
        animator = GetComponent<Animator>();

#if UNITY_EDITOR
        InitializeStateDictionary();
#endif

        CreateStateHashNameDictionary();
        SetCurrentStateHash();
    }

#if UNITY_EDITOR
    /// <summary>
    /// Initializes the state machine by adding states from the animator controller to the state machine.
    /// </summary>
    private void InitializeStateDictionary()
    {
        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

        foreach (AnimatorControllerLayer layer in animatorController.layers)
        {
            foreach (ChildAnimatorState state in layer.stateMachine.states)
            {
                stateMachine.AddState(state.state.nameHash, state.state.name);
            }
        }
    }
#endif

    /// <summary>
    /// Creates a dictionary mapping state hash codes to state names.
    /// </summary>
    private void CreateStateHashNameDictionary()
    {
        foreach (StateIdentifier identifier in stateMachine.statesList)
        {
            stateHashNameDict.Add(identifier.hashCode, identifier.name);
        }
    }

    /// <summary>
    /// Sets the hash code of the current state based on the initial state defined in the state machine.
    /// </summary>
    private void SetCurrentStateHash()
    {
        foreach (StateIdentifier identifier in stateMachine.statesList)
        {
            if (identifier.name == stateMachine.firstState.menùState.name)
            {
                currentStateHash = stateMachine.GetHashByState(stateMachine.firstState);
            }
        }
    }

    /// <summary>
    /// Handles the transition to a new state, triggering the corresponding events and updating the narrator voice.
    /// </summary>
    /// <param name="_newState">The name of the new state.</param>
    /// <param name="_lastState">The name of the previous state.</param>
    private void NewState(string _newState, string _lastState)
    {

	    lastState = null;

        if (narratorVoicInExecuting == true)
        {
	        StopAllCoroutines();
        }

        foreach (ScriptableMenùState menùState in menuStateCollector.menùStateList)
        {

	        if (_newState == menùState.menùState.name)
            {
                currentState = menùState;
            }

            if (_lastState == menùState.menùState.name)
            {
                lastState = menùState;
            }
        }

        if (currentState.menùState.entryDialogue == null)
        {
            pressInput = true;
        }
        else
        {
	        narratorVoice = currentState.menùState.entryDialogue;
            pressInput = false;
        }

        lastState.menùState.OnEndState.Invoke();
        currentState.menùState.OnStartState?.Invoke();
        narratorVoicInExecuting = false;
    }

    public void SkipState()
    {
	    pressInput = true;
	    EventManager.SetAnimationTrigger(animator, "AudioFinished");
    }

    /// <summary>
    /// Handles the end of the narrator voice clip, allowing menu input to be registered again.
    /// </summary>
    /// <param name="clip">The audio clip that ended.</param>
    private void EndNarratorVoice(S_Audio clip)
    {
        if (clip == narratorVoice && clip !=null)
        {
            pressInput = true;
            EventManager.SetAnimationTrigger(animator, "AudioFinished");

            if (currentState.repeatDialogueOnEnd)
            {
	            narratorVoicInExecuting = true;
	            StopAllCoroutines();
	            StartCoroutine(WaitForAnOtherTimeDialogue(currentState));
            }
        }
    }

    private IEnumerator WaitForAnOtherTimeDialogue(ScriptableMenùState state)
    {

	    ScriptableMenùState myLastState = state;

	    float elapsedTime = 0f;

	    while (elapsedTime < waitingTimeBeforeDialogue && narratorVoicInExecuting)
	    {
		    elapsedTime += Time.deltaTime;

		    yield return null;
	    }

	    if (narratorVoicInExecuting)
	    {
		    narratorVoicInExecuting = false;
		    if (currentState.repeatDialogueOnEnd && myLastState==currentState)
		    {
			    UnityGamesToolkit.Runtime.EventManager.OnPlayAudio?.Invoke(narratorVoice);
		    }
	    }
    }

    /// <summary>
    /// Sets the gesture number parameter in the animator.
    /// </summary>
    /// <param name="gesture">The gesture number to set.</param>
    private void SetGestureNumber(int gesture)
    {
        animator.SetInteger("Gesture", gesture);
    }

    public void UpgradeAudioObstaclesInGameAndPlay()
    {/*
	    obstaclesInGame.list.Clear();
	    Dictionary<BiClass<string, S_Audio>> myBiClassList = GameManager.Instance.saveData.mainLanguage.language.clusterEntryDialogue;

	    obstaclesInGame.list.Add(GameManager.Instance.saveData.mainLanguage.language.incipitAudio);

	    foreach (var obstacleStruct in GameManager.Instance.saveData.obstacleStructBool)
	    {
		    if (!obstacleStruct.everDoneInTutorial)
		    {
			    obstaclesInGame.list.Add(myBiClassList.FindCorrespondingElementTo(obstacleStruct.obstacleName));
		    }
	    }

	    if (obstaclesInGame.list.Count == 1)
	    {
		    obstaclesInGame.list.Clear();
		    obstaclesInGame.list.Add(GameManager.Instance.saveData.mainLanguage.language.allObstacleCompletedAudio);
	    }

	    UnityGamesToolkit.Runtime.EventManager.OnPlayAudioCluster?.Invoke(obstaclesInGame);

	    pressInput = false;
	    */
    }

    public void EndClusterVoice(S_AudioCluster cluster)
    {
	    if (cluster == obstaclesInGame)
	    {
		    pressInput = true;
		    EventManager.SetAnimationTrigger(animator, "ClusterFinished");
	    }
    }
}
