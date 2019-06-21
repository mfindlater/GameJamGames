using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private List<BuildingController> buildingControllers = new List<BuildingController>();
    private float[] previousRates; 
    private DayNightCycler dayNightCycle;
    public float increasedGrowthRateInSeconds = 4f;
    public float firstDayRate = 2f;


	// Use this for initialization
	void Awake () {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("BuildingSpawner");
        foreach(var spawner in spawners)
        {
            buildingControllers.Add(spawner.GetComponent<BuildingController>());
        }
        previousRates = new float[buildingControllers.Count];
        dayNightCycle = GameObject.Find("Day_Night Controller").GetComponent<DayNightCycler>();
        dayNightCycle.TimeOfDayChanged += HandleTimeOfDayChanged;

        
	
	}

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(IncreaseRate(firstDayRate));
    }

    void HandleTimeOfDayChanged(TimeOfDay tod)
    {
        if (tod == TimeOfDay.DayTime)
        {
            StopAllCoroutines();
            StartCoroutine(IncreaseRate(increasedGrowthRateInSeconds));
        }
    }

    IEnumerator IncreaseRate(float newRate)
    {
        for (int i = 0; i < buildingControllers.Count; i++)
        {
            previousRates[i] = buildingControllers[i].spawnTimeInSeconds;
            buildingControllers[i].spawnTimeInSeconds = newRate;
            buildingControllers[i].StopAllCoroutines();
            buildingControllers[i].StartCoroutine("SpawnBuildingPart");
        }
        yield return new WaitForSeconds(dayNightCycle.dayTime);

        for (int j = 0; j < buildingControllers.Count; j++)
        {
            buildingControllers[j].spawnTimeInSeconds = previousRates[j];
            buildingControllers[j].StopAllCoroutines();
            buildingControllers[j].StartCoroutine("SpawnBuildingPart");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
