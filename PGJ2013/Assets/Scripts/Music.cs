using UnityEngine;
using System.Collections;

public class Music : Singleton<Music> {
    public AudioClip musicTheme;
    public AudioClip musicGameOver;
    public AudioClip musicFight;

    private AudioSource source;

    private Menu.GameState recordedState;

    void Start()
    {
        source = Instance.GetComponent<AudioSource>().audio;
        source.clip = musicTheme;
        source.Play();
        recordedState = Menu.GameState.Title;
    }

    void Update()
    {
        if (recordedState != GameState.State)
        {
            bool previouslyTheme = recordedState == Menu.GameState.PlayerSelect || recordedState == Menu.GameState.Title;
            recordedState = GameState.State;
            switch (recordedState)
            {
                case Menu.GameState.PlayerSelect:
                case Menu.GameState.Title:
                    if (!previouslyTheme)
                    {
                        source.clip = musicTheme;
                        source.Play();
                    }
                    break;
                case Menu.GameState.Game:
                    source.clip = musicFight;
                    source.Play();
                    break;
                case Menu.GameState.GameResults:
                    source.clip = musicGameOver;
                    source.Play();
                    break;
            }
        }
    }
}
