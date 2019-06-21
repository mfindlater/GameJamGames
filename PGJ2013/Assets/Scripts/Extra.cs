using UnityEngine;
using System.Collections;

public class Extra : MonoBehaviour {
    public float duration = 1;
    public float magnitude = 3;

    public GameObject LMecha;
    public GameObject RMecha;
    public Camera LCamera;
    public Camera RCamera;
    

	// Use this for initialization
	void Start () {
        var lMechaHealth = LMecha.GetComponent<Health>();
        lMechaHealth.TookDamage += OnLCam;
        lMechaHealth.Dead += OnRWin;

        var rMechaHealth = RMecha.GetComponent<Health>();
        rMechaHealth.TookDamage += OnRCam;
        rMechaHealth.Dead += OnLWin;
	}

    void OnLWin()
    {
        GameManager.Winners = string.Empty;
        int playerCount = 0;
        for (int i = 0; i < 2; i++)
        {
            if(GameManager.ActivePlayers[i])
            {
                playerCount++;
                GameManager.Winners += " P" + (i + 1) + " ";
               
            }
        }

        if(playerCount > 1)
        {
            GameManager.DetermineSync(GameManager.PlayerActions[0], GameManager.PlayerActions[1]);
            GameManager.DetermineRank();
        }
    }

    void OnRWin()
    {
        GameManager.Winners = string.Empty;
        int playerCount = 0;

        for (int i = 2; i < 4; i++)
        {
            if (GameManager.ActivePlayers[i])
            {
                playerCount++;
                GameManager.Winners += " P" + (i + 1) + " ";
            }
        }

        if (playerCount > 1)
        {
            GameManager.DetermineSync(GameManager.PlayerActions[2], GameManager.PlayerActions[3]);
            GameManager.DetermineRank();
        }
    }

    void OnLCam()
    {
        StartCoroutine(Shake(LCamera));
    }

    void OnRCam()
    {
        StartCoroutine(Shake(RCamera));
    }


    
    IEnumerator Shake(Camera cam)
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = cam.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);


            // map noise to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            cam.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            yield return null;
        }

        cam.transform.position = originalCamPos;
    }




 


}
