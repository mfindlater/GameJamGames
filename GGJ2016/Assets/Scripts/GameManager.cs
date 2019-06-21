using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    private GameObject battle;
    private Camera main;
    private GameObject hud;
    private GameObject player;

    public AudioClip battleMusic;
    public AudioClip storeMusic;
    public AudioClip roomMusic;

    private AudioSource audioSource;

	void Start () {
        DontDestroyOnLoad(gameObject);

        audioSource = GameObject.Find("UI").GetComponent<AudioSource>();
        audioSource.clip = roomMusic;
        audioSource.Play();
	}

    public void StartBattle()
    {
        audioSource.clip = battleMusic;
        audioSource.Play();
        hud = GameObject.Find("PlayerHUD");
        player = GameObject.Find("Player");
        hud.SetActive(false);
        player.SetActive(false);
        battle.SetActive(true);
        main = Camera.main;
        Camera.main.enabled = false;
    }

    public void EndBattle(bool win)
    {
        hud.SetActive(true);
        player.SetActive(true);
        if(win) player.GetComponent<Player>().ClearItem();
        battle.SetActive(false);
        main.enabled = true;
        audioSource.clip = roomMusic;
        audioSource.Play();
    }
	
	void Update () {
        if(!battle)
        {
            var b = GameObject.Find("Battle");
            if(b)
            {
                battle = b;
                battle.SetActive(false);
            }
        }
	}



    public void EnterStore(GameObject player)
    {
        var cam = Camera.main;
        cam.transform.position = new Vector3(14, 0, -10);
        player.transform.position = new Vector3(9, -3, 0);
        audioSource.clip = storeMusic;
        audioSource.Play();
    }

    public void EnterRoom(GameObject player)
    {
        var cam = Camera.main;
        cam.transform.position = new Vector3(0, 0, -10);
        player.transform.position = new Vector3(-6, -3, 0);
        audioSource.clip = roomMusic;
        audioSource.Play();
    }
}
