using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadAdditive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void Unload(string name)
    {
        SceneManager.UnloadScene(name);
    }
}
