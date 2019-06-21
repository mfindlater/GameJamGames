using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class ScoreEvent : UnityEvent<int>
{

} 

public class Player : MonoBehaviour
{
    public float Speed = 10f;

    private Rigidbody2D _rigidbody2d;
    private Vector2 _direction = Vector2.zero;

    private Animator _animator;
    private Animator _shellAnimator;

    private SpriteRenderer _spriteRenderer;

    private SpriteRenderer _shellRenderer;

    private Dictionary<string, RuntimeAnimatorController> controllers;

    private bool hasShell = true;

    public UnityEvent PlayerKilled;

    public bool Dead = false;

    public int secondsAlive = 0;

    private bool hiding = false;

    public float hideTime = 0.7f;
    public float hideCooldown = 0.7f;

    private bool _hideReady = true;

    private bool movedLastUpdate = false;
    private bool isMoving = false; 


    public AudioClip playerDeadClip;
    public AudioClip lostShellClip;
    public AudioClip pickupShellClip;
    public AudioClip walkClip;

    public AudioClip hideInShellClip; 

    private AudioSource _audioSource;

    public UnityEvent LostShell;

    IEnumerator GameTimer()
    {
        while(!Dead)
        {
            yield return new WaitForSeconds(1);
            secondsAlive +=1;

            UpdateScore.Invoke(secondsAlive);
        }
    }

    public ScoreEvent UpdateScore;

    public bool HasShell => hasShell;
    public bool Hiding => hiding;

    public void SetShell(string name)
    {
        _shellAnimator.runtimeAnimatorController = controllers[name];
        _shellAnimator.gameObject.SetActive(true);
        hasShell = true;
        StartCoroutine(PlayPickupSound());
    }

    IEnumerator PlayPickupSound()
    {
         var prevClip = _audioSource.clip;
        var loop = _audioSource.loop;

        _audioSource.clip = pickupShellClip;
        _audioSource.loop = false;
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);

        _audioSource.clip = prevClip;
        _audioSource.loop = loop;
    }

    void Awake()
    {

        _audioSource = GetComponent<AudioSource>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _shellAnimator = transform.Find("Shell").GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shellRenderer = transform.Find("Shell").GetComponent<SpriteRenderer>();

        controllers = new Dictionary<string, RuntimeAnimatorController>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        controllers.Add("Shell1", Resources.Load<RuntimeAnimatorController>("Animators/Shell1AnimController"));
        controllers.Add("Shell2", Resources.Load<RuntimeAnimatorController>("Animators/Shell2AnimController"));
        controllers.Add("Shell3", Resources.Load<RuntimeAnimatorController>("Animators/Shell3AnimController"));
        controllers.Add("Shell4", Resources.Load<RuntimeAnimatorController>("Animators/Shell4AnimController"));

        _shellAnimator.runtimeAnimatorController = controllers["Shell1"];

        _shellAnimator.gameObject.SetActive(false);
        hasShell = false;

        StartCoroutine("GameTimer");
    }

    // Update is called once per frame
    void Update()
    {

        if(Dead)
        {
            _audioSource.loop = false;
            return;
        }

        if(hiding)
        {
            return;
        }

        if(!isMoving)
        {
            _audioSource.loop = false;
        }
    
        SetBool("Up", false);
        SetBool("Down", false);
        SetBool("Left", false);
        SetBool("Right", false);

        _direction = Vector2.zero;

        if(Input.GetKey(KeyCode.W))
        {
            _direction += Vector2.up;
            SetBool("Up", true);
        }

        if(Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
            SetBool("Down", true);
        }

        if(Input.GetKey(KeyCode.A))
        {    
            FlipX(true);
            _direction += Vector2.left;
            SetBool("Left", true);
        }

        if(Input.GetKey(KeyCode.D))
        {
            FlipX(false);
            _direction += Vector2.right;
            SetBool("Right", true);
        }

        if(!hiding && _hideReady && hasShell && Input.GetKeyDown(KeyCode.K))
        {
             _direction = Vector2.zero;
            SetBool("Hide", true);
            SetBool("Up", false);
            SetBool("Down", false);
            SetBool("Left", false);
            SetBool("Right", false);

            _audioSource.clip = hideInShellClip;
            _audioSource.loop = false;
            _audioSource.Play();

            StartCoroutine(ShellDown());
        }

        isMoving = _direction.magnitude > 0;
        if(!movedLastUpdate && isMoving)
        {
            _audioSource.clip = walkClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }


      
      

        SetBool("IsMoving",isMoving);
        SetBool("Dead", Dead);

        movedLastUpdate = isMoving;
    }

    private void SetBool(string name, bool v)
    {
        if(_animator.gameObject.activeSelf)
        {
            _animator.SetBool(name, v);
        }
        
        if(_shellAnimator.gameObject.activeSelf)
        {
            _shellAnimator.SetBool(name, v);
        }
    }

    private void FlipX(bool v)
    {
        _spriteRenderer.flipX = v;
        _shellRenderer.flipX = v;
    }

    void FixedUpdate()
    {
        _rigidbody2d.MovePosition(_rigidbody2d.position + _direction * Speed * Time.fixedDeltaTime);
    }

    IEnumerator ShellDown()
    {
        _hideReady = false;
        hiding = true;
        yield return new WaitForSeconds(hideTime);
        hiding = false;
         SetBool("Hide", false);
        yield return StartCoroutine(HideCooldown());
    }

    IEnumerator HideCooldown()
    {
       
         yield return new WaitForSeconds(hideCooldown);
        _hideReady = true;
    }


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5);
        var go = GameObject.Find("UI");
        GameObject.Destroy(go);
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        //Debug.Log("Collision");
        if(c.collider.tag.Equals("Enemy"))
        {
            if(hasShell && hiding)
            {
                //stun enemy!
            }
            else if(hasShell && !hiding)
            {
                _shellAnimator.gameObject.SetActive(false);
                hasShell = false;
                _audioSource.clip = lostShellClip;
                _audioSource.loop = false;
                _audioSource.Play();
                LostShell.Invoke();
              
            }
            else if(!Dead)
            {
                Dead = true;
                _animator.SetTrigger("Die");
                SetBool("Dead",true); 
                SetBool("Up", false);
                SetBool("Down", false);
                SetBool("Left", false);
                SetBool("Right", false);
                
                _direction = Vector2.zero;

                 PlayerKilled.Invoke();
                _audioSource.clip = playerDeadClip;
                _audioSource.Play();
                StartCoroutine(ChangeScene());
            }

        }
    }
}
