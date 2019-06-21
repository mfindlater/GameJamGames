using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;
using UnityEngine.Events;

[Serializable]
public class PlayerEvent : UnityEvent<PlayerBehavior> { }

[Serializable]
public class BatteryPercentEvent : UnityEvent<float> { }

[RequireComponent(typeof(AudioSource))]
public class PlayerBehavior : MonoBehaviour {
	public int playerNumber;
	public float speed;
	public float turnSpeed = 60.0f;
	private Vector3 targetVector;
	public Light flashLightBulb;

    public AudioClip footstepClip;
    public AudioClip flashlightClickClip;
    public AudioClip flashlightHumClip;
    public AudioClip deadClip;
    public AudioClip deadBatteryClip;

    private float distanceLastFrame;
    private float batteryPercentLastFrame;
    public PlayerState playerState;
	public FlashlightState flashLightState;
    private Transform flashLightTransform;
    private AudioSource m_audioSource;
    private AudioSource m_flashlightAudioSource;

    //private CharacterController m_charaController;
    private Rigidbody m_rigidBody;
    private Animator m_animator;

    public PlayerEvent PlayerKilled = new PlayerEvent();
    public PlayerEvent PlayerReady = new PlayerEvent();
    public BatteryPercentEvent BatteryPercentChanged = new BatteryPercentEvent();
    public UnityEvent PlayerWon;

    public float lightRange = 10;

    public float intensity_stage1 = 3f;
    public float intensity_stage2 = 4f;
    public float intensity_stage3 = 5f;
    public float intensity_stage4 = 8f;

    void OnReady(PlayerBehavior player)
    {
        if (PlayerReady != null)
            PlayerReady.Invoke(this);
    }


	// Use this for initialization
	void Awake () {
        m_audioSource = GetComponent<AudioSource>();
        flashLightTransform = transform.FindChild("Player").FindChild("flashlight");
        m_flashlightAudioSource = flashLightTransform.GetComponent<AudioSource>();
        m_animator = transform.FindChild("Player").GetComponent<Animator>();
        playerState.IsReady = false;
        m_rigidBody = GetComponent<Rigidbody>();
        GameManager.instance.SetupPlayer(this);
        //m_charaController = GetComponent<CharacterController>();
    }

    public bool StartedWalking(float currentDistance)
    {
        return distanceLastFrame == 0 && currentDistance > 0;
    }

    IEnumerator PlayFlashlightSounds(bool hum)
    {
        if(flashlightClickClip != null)
        {
            m_flashlightAudioSource.loop = false;
            m_flashlightAudioSource.PlayOneShot(flashlightClickClip);
            yield return new WaitForSeconds(flashlightClickClip.length);
        }

        if(flashlightHumClip != null)
        {
            m_flashlightAudioSource.loop = true;
            m_flashlightAudioSource.clip = flashlightHumClip;
            m_flashlightAudioSource.Play();
        }

        while(flashLightState.On)
        {
            yield return null;
        }
    }

    public void PlayFootsteps()
    {
        if(footstepClip != null)
        {
            m_audioSource.volume = 1.0f;

            if (!m_audioSource.isPlaying)
            {
                m_audioSource.clip = footstepClip;
                m_audioSource.Play();
                m_audioSource.loop = true;
            }
        }
    }

    public void PlayDeadBattery()
    {
        if(deadBatteryClip)
        {
            m_audioSource.PlayOneShot(deadBatteryClip);
        }
    }

    bool BatteryOut()
    {
        return (batteryPercentLastFrame > 0 && flashLightState.BatteryPecentage <= 0);
    }

	// Update is called once per frame
	void Update () {


        if(!playerState.IsReady && (ReInput.players.Players[playerNumber].GetButtonDown("Flashlight")))
        {
            playerState.IsReady = true;
            OnReady(this);
        }

        if (!playerState.IsReady || playerState.IsDead) return;


        // Movement
        Vector3 targetVector = new Vector3(ReInput.players.Players[playerNumber].GetAxis("Horizontal"), 0, ReInput.players.Players[playerNumber].GetAxis("Vertical"));
        targetVector = targetVector.normalized;
        if (targetVector != Vector3.zero) {
            m_rigidBody.MovePosition(transform.position + transform.TransformDirection(targetVector) * speed * Time.deltaTime );
		}

        if (StartedWalking(targetVector.magnitude))
        {
            m_animator.SetBool("Moving", true);
            PlayFootsteps();
        }

        if (targetVector.magnitude == 0)
        {
            m_audioSource.volume = 0;
            m_animator.SetBool("Moving", false);
        }

        distanceLastFrame = targetVector.magnitude;

        //rotation
        transform.Rotate(0, ReInput.players.Players[playerNumber].GetAxis("Rotate") * turnSpeed * Time.deltaTime, 0);
        //m_rigidBody.MoveRotation(Quaternion.Euler(new Vector3(0, ReInput.players.Players[playerNumber].GetAxis("Rotate") * turnSpeed * Time.deltaTime, 0)));

        //look up/down
        float angle = -ReInput.players.Players[playerNumber].GetAxis("Look") * turnSpeed * Time.deltaTime;
        
        flashLightTransform.Rotate(angle, 0, 0);


        if (ReInput.players.Players[playerNumber].GetButtonDown("Flashlight")){
           
            if(flashLightState.ToggleSwitch ())
            {
                StartCoroutine(PlayFlashlightSounds(true));
            }
            else
            {
                StartCoroutine(PlayFlashlightSounds(false));
            }

            string sw = (flashLightState.On) ? "On" : "Off";
            Debug.Log(string.Format("Flashlight is toggled {0}", sw));
        };

		if (flashLightState.BatteryPecentage == 100.0f) {
			flashLightBulb.intensity = intensity_stage4;
            BatteryPercentChanged.Invoke(flashLightState.BatteryPecentage);
			//Debug.Log ("intensity = 100");
		} else if (flashLightState.BatteryPecentage <= 75.0f && flashLightState.BatteryPecentage > 50.0f) {
			flashLightBulb.intensity = intensity_stage3;
            BatteryPercentChanged.Invoke(flashLightState.BatteryPecentage);
            //Debug.Log ("intensity = 75");
        } else if (flashLightState.BatteryPecentage <= 50.0f && flashLightState.BatteryPecentage > 25.0f) {
			flashLightBulb.intensity = intensity_stage2;
            BatteryPercentChanged.Invoke(flashLightState.BatteryPecentage);
            //Debug.Log ("intensity = 50");
        } else if (flashLightState.BatteryPecentage <= 25.0f && flashLightState.BatteryPecentage > 0.0f) {
			flashLightBulb.intensity = intensity_stage1;
            BatteryPercentChanged.Invoke(flashLightState.BatteryPecentage);
            //Debug.Log ("intensity = 25");
        } else if (flashLightState.BatteryPecentage == 0.0f) {
			flashLightBulb.intensity = 0.0f;
            BatteryPercentChanged.Invoke(flashLightState.BatteryPecentage);
            //Debug.Log ("intensity = 0");
        }

		flashLightState.Update ();
		flashLightBulb.enabled = flashLightState.On;

        batteryPercentLastFrame = flashLightState.BatteryPecentage;

        if (BatteryOut())
        {
            PlayDeadBattery();
        }
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(flashLightTransform.position, flashLightTransform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, lightRange))
        {
            if (flashLightState.On)
            {
                var enemyPlayer = hit.transform.GetComponent<PlayerBehavior>();

                if ( enemyPlayer != null && enemyPlayer != this && !enemyPlayer.playerState.IsDead)
                {
                    enemyPlayer.Kill();
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (flashLightTransform != null)
        {
            Ray r = new Ray(flashLightTransform.position, flashLightTransform.forward);
            Gizmos.DrawLine(flashLightTransform.position, r.GetPoint(lightRange));
        }
    }

    public void Kill()
    {
        if (playerState.IsDead)
            return;

        playerState.IsDead = true;

        if(PlayerKilled != null)
            PlayerKilled.Invoke(this);

        if (deadClip != null)
        {
            Debug.Log("Killed");
            m_audioSource.PlayOneShot(deadClip);
        }
    }

    public void Won()
    {
        if (PlayerWon != null)
            PlayerWon.Invoke();
    }
}
