using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PlayerController : FallingObject
{
    [SerializeField] private float _maxMoveSpeed = 9;
    [SerializeField] private float _moveIncreaseSpeed = 25;
    [SerializeField] private float _jumpImpulse = 8;
    [SerializeField] private float _horiDrag = 35;
    [SerializeField] private float _quickTurnSped = 70;
    [SerializeField] private int _maxJumpCount = 1;
    [Space]
    [SerializeField] private Bubble _bubblePrefab;

    private bool _facingLeft;
    private int _currentJumps;
    private SpriteRenderer _spriteRenderer;

    private bool _shouldKillThisFrame;
    private bool _landedOnEnemyThisFrame;
    
    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    private new void Update()
    {
        GameManager.Instance.TimePassed += Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && _currentJumps < _maxJumpCount)
        {
            _currentJumps += 1;
            _velocity.y = _jumpImpulse;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Bubble bubbleInstance = Instantiate(_bubblePrefab, transform.position, Quaternion.identity);
            if (_facingLeft) bubbleInstance.FlipMomentum();
        }

        float moveX = Input.GetAxis("Horizontal");
        base.Update();

        _velocity.x += _moveIncreaseSpeed * moveX * Time.deltaTime;

        if (Mathf.Abs(moveX) > 0.1f)
        {
            _facingLeft = _velocity.x <= 0;
        }
        
        if (Mathf.Abs(_velocity.x) > 0.1 && Mathf.Abs(moveX) < 0.1f)
        {
            _velocity.x += _velocity.x > 0 ? _horiDrag * Time.deltaTime * -1 : _horiDrag * Time.deltaTime;
        }
        else if (Mathf.Abs(moveX) < 0.1f)
        {
            _velocity.x = 0f;
        }
        
        if (_velocity.x > 0.1 && moveX < -0.5)
        {
            _velocity.x -= _quickTurnSped * Time.deltaTime;
        }
        else if (_velocity.x < -0.1 && moveX > 0.5)
        {
            _velocity.x += _quickTurnSped * Time.deltaTime;
        }
        
        _velocity.x = Mathf.Clamp(_velocity.x, -_maxMoveSpeed, _maxMoveSpeed);

        if (hit && _velocity.y < 0)
        {
            _currentJumps = 0;
            _velocity.y = 0;
            transform.position = new Vector3(transform.position.x, hit.collider.transform.position.y + hit.collider.bounds.extents.y, transform.position.z);
        }
    }

    private new void LateUpdate()
    {
        base.LateUpdate();
        
        if (_shouldKillThisFrame && !_landedOnEnemyThisFrame)
        {
            Destroy(gameObject);
        }

        _shouldKillThisFrame = false;
        _landedOnEnemyThisFrame = false;
    }

    public void EnemyKillCheck(float amount, GameObject enemy, int score)
    {
        if (_velocity.y < 0f)
        {
            _velocity.y = amount;
            Destroy(enemy.gameObject);
            _landedOnEnemyThisFrame = true;
        }
    }

    public void PlayerKillCheck(float bottomPoint)
    {
        if (_velocity.y >= 0f || transform.position.y < bottomPoint)
        {
            _shouldKillThisFrame = true;
        }
    }
}
