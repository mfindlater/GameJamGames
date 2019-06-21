using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject Player;
	public GameObject Spinner;
	public GameObject SpawnPoint;
	public GameObject Center;
	public GameObject Cam;
	GameObject sfx;
	public GameObject MusicPlayer;
	public bool IsInvincible = true;
	public float SpinnerRotation = 0;	
	private float RotationSpeed = -100;
	public float MovementSpeed = 8;
	
	float invincibilityElapsed;
	public float InvincibilityDuration = 100;
	
	public int MaxHealth = 100;
	public int CurrentHealth;
	
	public float Distance = 10f;
	
	//float seconds = 30;
	//float secondsElapsed = 0;

	// Use this for initialization
	void Start () {
		Spinner = GameObject.Find("spinner");
		Player = GameObject.Find("player_ball");
		SpawnPoint = GameObject.Find("spawn_location");
		Center = GameObject.Find("Center");
		Cam = GameObject.Find("Main Camera");
		sfx = GameObject.Find("sfxPlayer");
		MusicPlayer = GameObject.Find("MusicPlayer");
		Spawn(true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.UpArrow) || 
			Input.GetKey(KeyCode.W) || 
			Input.GetButton("RB_1") || 
			Input.GetAxis("TriggersR_1") < 0)
		{
			Distance += Time.deltaTime * MovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.DownArrow) || 
			Input.GetKey(KeyCode.S) || 
			Input.GetButton("LB_1") || 
			Input.GetAxis("TriggersR_1") > 0)
		{
			Distance -= Time.deltaTime *  MovementSpeed;
		}
		
		Distance = Mathf.Clamp(Distance,4,16);
		
		if(CurrentHealth > MaxHealth)
		{
			CurrentHealth = MaxHealth;
		}
		
		if(CurrentHealth <= 0)
		{
			Application.LoadLevel("GameOver");
			GameObject game = GameObject.Find("Game");
			game.GetComponent<Game>().Score = Cam.GetComponent<HighscoreManager>().GetScore();
		}
		
		invincibilityElapsed += Time.deltaTime;
		if(invincibilityElapsed >= InvincibilityDuration)
		{
			if(IsInvincible)
				EndInvincibility();
		}
	}
	
	float angle;
	void FixedUpdate() {
	
		
		angle -= 1;
		if(angle > 360)
			angle = 0;
		if(angle < 0)
			angle = 360;
		
		float a = 0;
		a	= (Mathf.Deg2Rad * angle);
		
		Player.transform.position = new Vector3(
			Center.transform.position.x + (Distance * Mathf.Sin(a)),
			Player.transform.position.y,
			Center.transform.position.z + (Distance * Mathf.Cos(a)));
		
		SpinnerRotation += ( RotationSpeed * Time.deltaTime);
		Spinner.transform.eulerAngles = new Vector3(0, SpinnerRotation, 0);
		
		Vector3 forceVector = Vector3.Normalize(Player.transform.position);
		Player.rigidbody.AddForce((forceVector * 50) * Time.deltaTime);
	}
	
	void OnGUI()
	{
		GUILayout.Label("Rotation Speed");
		GUILayout.Label(RotationSpeed.ToString());
		RotationSpeed = GUILayout.HorizontalSlider(RotationSpeed,-500,500);
	}
	
	void Spawn(bool restoreHealth) {
		if(restoreHealth)
		{
			CurrentHealth = MaxHealth;
		}
		Player.rigidbody.isKinematic = true;
		Player.rigidbody.transform.position = SpawnPoint.transform.position;
		Player.rigidbody.isKinematic = false;
		Player.rigidbody.velocity = Vector3.zero;
	}
	
	void Respawn() {
		Spawn(false);
	}
	
	public void BeginInvincibility()
	{
		invincibilityElapsed = 0;
		IsInvincible = true;
		Player.rigidbody.mass = 200;
		Player.GetComponent<ParticleSystem>().enableEmission = true;
		MusicPlayer.GetComponent<MusicPlayer>().Play(8);
		GameObject Spawner = GameObject.Find("Spawner");
		Spawner.GetComponent<SpawnManager>().Probs[2] = 0;	
	}
	
	public void EndInvincibility()
	{
		IsInvincible = false;
		Player.GetComponent<ParticleSystem>().enableEmission = false;
		MusicPlayer.GetComponent<MusicPlayer>().PlayPrev();
		GameObject Spawner = GameObject.Find("Spawner");
		Spawner.GetComponent<SpawnManager>().Probs[2] = 1;
	}
	
	void TakeDamage(int damageAmount) {
		if(!IsInvincible)
		{
			Cam.GetComponent<HighscoreManager>().Clear();
			Cam.GetComponent<HighscoreManager>().Multiplier = 1;
			CurrentHealth -= damageAmount;
		}
	}
	
}
