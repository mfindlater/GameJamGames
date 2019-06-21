using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeightsData : ScriptableObject {
	
	private Dictionary<string, float> _weights;
	
	public Dictionary<string, float> Weights
	{
		get 
		{
			return _weights;
		}
		
		set
		{
			_weights = value;
		}
	}
	
	public float TotalWeights
	{
		get 
		{
			float retval = 0f;
			
			foreach(var weight in _weights.Values)
			{
				retval += weight;
			}
			return retval;
		}
	}
	
	public float GetPercentage(string item)
	{
		float v = _weights[item];
		float retval = v / TotalWeights;
		return Mathf.Round(retval);
	}
	

}
