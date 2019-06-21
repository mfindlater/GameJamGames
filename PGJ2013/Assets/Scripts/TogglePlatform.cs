using UnityEngine;
using System.Collections;

public class TogglePlatform : MonoBehaviour {

	public float timeOffset;
	public float cycleTime;
	public float displayTime;

	
	private float elapsedTime;
	private const float flashTime = 0.1f;
	private const float fadeInTime = 0.25f;

    public GameObject platformMesh;
	
	// Use this for initialization
	void Start () {
	
		//platformMesh = transform.FindChild("platformMesh").gameObject;
		renderer.material.color = new Color(1.0f,1.0f,1.0f,0.0f);
		
		elapsedTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		elapsedTime += Time.deltaTime;
		
		if(UnityEngine.Time.realtimeSinceStartup % cycleTime < displayTime)
		{
			renderer.enabled = true;
			collider.isTrigger = false;
			
			platformMesh.renderer.enabled = true;
			platformMesh.collider.isTrigger = false;
		}
		else
		{
			renderer.enabled = false;
			collider.isTrigger = true;
			
			platformMesh.renderer.enabled = false;
			platformMesh.collider.isTrigger = true;
		}
		
		if(((elapsedTime > timeOffset) && (elapsedTime < (timeOffset + displayTime))) || elapsedTime < (timeOffset + displayTime) - cycleTime)
		{
			//Platform is enabled
			
			renderer.material.color = new Color(1.0f,1.0f,1.0f,0.0f);
			
			//Fade out the white flash
			if(elapsedTime > (timeOffset) && elapsedTime < (timeOffset + flashTime))
			{
				//Color colorStart = Color.white;
				//Color colorEnd = new Color(1.0f,1.0f,1.0f,0.0f);
				
				//float lerp = Mathf.Lerp (1.0f, 0.0f, 1.0f - (elapsedTime - (timeOffset - fadeInTime)));
				//renderer.material.color = Color.Lerp (colorStart, colorEnd, lerp);
				
				renderer.material.color = Color.white;
			}
			
			//Fade out the white flash
			if(elapsedTime > (timeOffset + flashTime ))
			{
				Color colorStart = Color.white;
				Color colorEnd = new Color(1.0f,1.0f,1.0f,0.0f);
				
				float lerp = Mathf.Lerp (1.0f, 0.0f, 1.0f - (elapsedTime - (timeOffset + fadeInTime)));
				renderer.material.color = Color.Lerp (colorStart, colorEnd, lerp);
				
				//renderer.material.color = Color.red;
			}
			
			
			
			
			renderer.enabled = true;
			collider.isTrigger = false;
			
			platformMesh.renderer.enabled = true;
			platformMesh.collider.isTrigger = false;
			
		}
		else
		{
			//Platform is disabled
			
			renderer.enabled = false;
			collider.isTrigger = true;
			
			platformMesh.renderer.enabled = false;
			platformMesh.collider.isTrigger = true;
			
		}
		
		
		

		if(elapsedTime >= cycleTime)
		{
			elapsedTime = 0.0f;	
		}
		
		
		
		
	}
}
