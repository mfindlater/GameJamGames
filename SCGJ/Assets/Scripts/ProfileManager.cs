using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ProfileManager : MonoBehaviour {

	public Profile playerProf;
	Menu mMenu;

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}
	// Use this for initialization
	void Start () {
		mMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Menu>();
		if (File.Exists(Application.dataPath+"/profile.scgj"))
		{
			LoadProfile();
		}
		else
		{
			MakeNewProfile();
		}
	}
	
	// Update is called once per frame
	private void MakeNewProfile()
	{
		mMenu.OnNewProfile();
	}

	public void NewProfile(string pName)
	{
		playerProf = new Profile();
		playerProf.name = pName;
		playerProf.highScore = 00000;
		FileStream stream = new FileStream(Application.dataPath+"/profile.scgj", FileMode.Create);
		BinaryFormatter bFormatter = new BinaryFormatter();
 
        bFormatter.Serialize(stream, playerProf);
        stream.Close();
	}

	public void SaveProfile()
	{
		FileStream stream = new FileStream(Application.dataPath+"/profile.scgj", FileMode.Create);
		BinaryFormatter bFormatter = new BinaryFormatter();
 
        bFormatter.Serialize(stream, playerProf);
        stream.Close();
	}

	public void LoadProfile()
	{
		FileStream stream = new FileStream(Application.dataPath+"/profile.scgj", FileMode.Open);
		BinaryFormatter bFormatter = new BinaryFormatter();
 		playerProf = (Profile)bFormatter.Deserialize(stream);	
        stream.Close();
	}
}
