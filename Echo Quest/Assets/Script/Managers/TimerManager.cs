using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;
using Coroutine = UnityEngine.Coroutine;


public class TimerManager : Singleton<TimerManager>
{
    private Dictionary<S_Container_Data_Float, Coroutine> dataIsCounting = new Dictionary<S_Container_Data_Float, Coroutine>();
    private Dictionary<S_Container_Data_Float, float> dataRecord = new Dictionary<S_Container_Data_Float, float>();
    [SerializeField] private ScriptableCollectorData dataCollector;
    [SerializeField] private S_Container_Data_Float maxDifficultReached;
    private bool seeData = false;

    /// <summary>
    /// Spawns data and displays it on the screen.
    /// </summary>
    public void SpawnData()
    {
        if (seeData)
        {
            Singletonn.Instance.playTestData.text = " ";
        }
        else
        {
            Singletonn.Instance.playTestData.text += "MaxPoints Earned : " + GameManager.Instance.saveData.maxScoreNumber.ToString();
            Singletonn.Instance.playTestData.text += "\n";

            foreach (S_Container_Data_Float data in dataCollector.list)
            {
                Singletonn.Instance.playTestData.text += "\n";
                Singletonn.Instance.playTestData.text += data.content.nameVariable + " : ";

                if (data == maxDifficultReached)
                {
                    Singletonn.Instance.playTestData.text += GameManager.Instance.difficultTranslator
                        .FindCorrespondingElementTo(maxDifficultReached.content.currentValue).ToString();
                }
                else
                {
                    Singletonn.Instance.playTestData.text += data.content.currentValue.ToString();
                }
            }
        }

        seeData = !seeData;
    }

    /// <summary>
    /// Resets all data in the data collector.
    /// </summary>
    public void ResetAllData()
    {
        foreach (S_Container_Data_Float data in dataCollector.list)
        {
            data.content.CloneCurrentIntoDefault();
        }
    }

    /// <summary>
    /// Starts counting for the specified data.
    /// </summary>
    /// <param name="data">The data to count.</param>
    public void StartCount(S_Container_Data_Float data)
    {
        if (!dataIsCounting.ContainsKey(data))
        {
            dataIsCounting.Add(data, StartCoroutine(Count(data)));
        }
    }

    /// <summary>
    /// Stops counting for the specified data.
    /// </summary>
    /// <param name="data">The data to stop counting.</param>
    public void EndCount(S_Container_Data_Float data)
    {
        if (dataIsCounting.ContainsKey(data))
        {
            StopCoroutine(dataIsCounting[data]);
            dataIsCounting.Remove(data);
        }
    }

    private IEnumerator Count(S_Container_Data_Float data)
    {
        while (true)
        {
            data.content.currentValue += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Starts counting for the specified data to record.
    /// </summary>
    /// <param name="data">The data to count for record.</param>
    public void StartCountForRecord(S_Container_Data_Float data)
    {
        float current = 0f;
        if (!dataIsCounting.ContainsKey(data))
        {
	        dataRecord.Add(data, current);
	        dataIsCounting.Add(data, StartCoroutine(CountRecord(current)));
        }
    }

    /// <summary>
    /// Stops counting for the specified data and updates the record.
    /// </summary>
    /// <param name="data">The data to stop counting for record.</param>
    public void EndCountForRecord(S_Container_Data_Float data)
    {
        StopCoroutine(dataIsCounting[data]);
        data.content.currentValue=dataRecord[data];
        dataRecord.Remove(data);
    }

    private IEnumerator CountRecord(float current)
    {
        while (true)
        {
            current += Time.deltaTime;
            yield return null;
        }
    }
}
