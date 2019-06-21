using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour {
    public float spawnTimeInSeconds = 2.0f;
    public GameObject buildingTop;
    public GameObject buildingPart;
    private GameObject top;
	// Use this for initialization
	IEnumerator Start () {
       top = (GameObject)Instantiate(buildingTop, transform.position, Quaternion.identity);
	   top.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2000));
	   yield return new WaitForSeconds(0.3f);
       yield return StartCoroutine("SpawnBuildingPart");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator SpawnBuildingPart()
    {
	    top.GetComponent<Rigidbody2D>().mass = 100;
		top.GetComponent<Rigidbody2D>().gravityScale = 40;
	    Instantiate(buildingPart, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimeInSeconds);
        
        //StartCoroutine("SpawnBuildingPart"); Dan's version
        yield return StartCoroutine("SpawnBuildingPart");
    }
}
