using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right,
}

public static class DirectionExtensions
{
    public static Direction Opposite(this Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                return Direction.None;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            default:
                return direction;
        }
    }
}

public class Enemy : MonoBehaviour
{

    public float Speed = 4;
    private bool _alive = true;
    private Animator _animator;

    private Vector2 _direction = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private Direction curDirection = Direction.None;

    private SpriteRenderer _spriteRenderer;

    private AudioSource _audioSource;
    public AudioClip enemyDeadClip;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Pattern");
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("IsMoving", _direction.magnitude > 0);
    }

    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * Speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.tag.Equals("Bounds") || c.collider.tag.Equals("Enemy"))
        {
            curDirection = curDirection.Opposite();

            Move(curDirection);

            StopCoroutine("Pattern");
            StartCoroutine("Pattern");
        }
        else if (c.collider.tag.Equals("Player"))
        {
            Player player = c.collider.GetComponent<Player>();

            if (player.Hiding)
            {
                _direction = Vector2.zero;

                _animator.SetTrigger("Die");
                _animator.SetBool("Dead", true);

                _audioSource.clip = enemyDeadClip;
                _audioSource.Play();
                
                StartCoroutine(PlaySound(enemyDeadClip, ()=> {
                    Destroy(gameObject);
                }));
            }
            else
            {
                curDirection = curDirection.Opposite();

                Move(curDirection);

                StopCoroutine("Pattern");
                StartCoroutine("Pattern");
            }
        }
    }

    void Move(Direction dir)
    {

        _direction = Vector2.zero;
        _animator.SetBool("Up", false);
        _animator.SetBool("Down", false);
        _animator.SetBool("Left", false);
        _animator.SetBool("Right", false);

        switch (dir)
        {
            case Direction.None:
                _direction = Vector2.zero;
                break;
            case Direction.Up:
                _animator.SetBool("Up", true);
                _direction += Vector2.up;
                break;
            case Direction.Down:
                _animator.SetBool("Down", true);
                _direction += Vector2.down;
                break;
            case Direction.Left:
                _animator.SetBool("Left", true);
                _direction += Vector2.left;
                _spriteRenderer.flipX = true;
                break;
            case Direction.Right:
                _animator.SetBool("Right", true);
                _direction += Vector2.right;
                _spriteRenderer.flipX = false;
                break;
        }
    }

    int index = 0;

    public float changeDirectionTIme = 2.5f;

    IEnumerator Pattern()
    {
        while (_alive)
        {
            yield return new WaitForSeconds(changeDirectionTIme);

            if(index > 4)
            {
                index = 0;
            }

            Direction dir = (Direction)index;
            curDirection = dir;

            Move(dir);

            index++;
        }
    }

    IEnumerator PlaySound(AudioClip clip, Action a)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
        yield return new WaitForSeconds(clip.length);

        if(a != null)
        {
            a();
        }
    }
}
