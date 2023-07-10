using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Singletonn : Singleton<Singletonn>
{

#region Variables & Properties

[SerializeField] public TextMeshProUGUI CurrentState;
[SerializeField] public TextMeshProUGUI playTestData;

#endregion

public void SpawnData()
{
	TimerManager.Instance.SpawnData();
}

public void NextState()
{
	MenùFlowStateMachine.Instance.SkipState();
}

}
