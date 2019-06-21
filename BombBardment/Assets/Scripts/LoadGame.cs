using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	private AudioSource audioSource;
    private FadeScene fade;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fade = GetComponent<FadeScene>();
    }

	// Use this for initialization
	IEnumerator Start () {
        while (Input.GetKeyDown(KeyCode.Escape) || !Input.anyKeyDown)
        {
            yield return null;
        }
        audioSource.Play();
        float fadeTime = fade.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Main");
    }
}
