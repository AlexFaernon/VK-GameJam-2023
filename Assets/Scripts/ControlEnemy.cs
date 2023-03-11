using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    private bool _isMouseOver;
    private bool _inPlayerRange;
    private bool _controlledByPlayer;
    private SpriteRenderer _spriteRenderer;
    private CameraFollow _cameraFollow;
    private Transform _player;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        if (_controlledByPlayer)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ReleaseControl();
            }
            return;
        }
        
        if (_isMouseOver && _inPlayerRange)
        {
            _spriteRenderer.color = Color.yellow;
            if (Input.GetMouseButtonDown(1))
            {
                TakeControl();
            }
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }

    private void TakeControl()
    {
        gameObject.SendMessage("PlayerControl", true);
        _controlledByPlayer = true;
        _player = _cameraFollow.objectToFollow;
        _cameraFollow.objectToFollow = transform;
    }

    private void ReleaseControl()
    {
        gameObject.SendMessage("PlayerControl", false);
        _controlledByPlayer = false;
        _cameraFollow.objectToFollow = _player;
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            _inPlayerRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _inPlayerRange = false;
    }
}
