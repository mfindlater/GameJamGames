using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string sceneName;

    private FadeScene fadeScene;

    void Awake()
    {
        fadeScene = GetComponent<FadeScene>();
    }

	public void Change()
    {
        StartCoroutine(Do());
    }

    IEnumerator Do()
    {
        float t = fadeScene.BeginFade(1);
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneName);
    }
}
