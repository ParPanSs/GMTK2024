using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject legs;
    [SerializeField] private List<Rigidbody2D> _rbs;
    [SerializeField] private List<Animator> duckAnimators;
    private float _horizontal;
    private float _startSpeed;
    private bool _canJump;
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        _startSpeed = speed;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        Collider2D ray = Physics2D.OverlapCircle(legs.transform.position, .2f, whatIsGround);
        if (ray != null)
        {
            _canJump = true;
            speed = _startSpeed;
        }
        else
        {
            speed = 7f;
        }
        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
            _canJump = false;
        }
        
        Flip();

        for (var i = 0; i < duckAnimators.Count; i++)
        {
            if (_horizontal != 0)
            {
                duckAnimators[i].SetBool("isWalk", true);
            }
            else
            {
                duckAnimators[i].SetBool("isWalk", false);
            }

            if (_rbs[i].velocity.y == 0)
            {
                duckAnimators[i].SetBool("isJump", false);
            }
        }
    }
    
    private void FixedUpdate()
    {
        foreach (var rigidbody in _rbs)
        {
            rigidbody.velocity = new Vector2(_horizontal * speed, rigidbody.velocity.y);
        }
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0 || !_isFacingRight && _horizontal > 0)
        {
            foreach (var rigidbody in _rbs)
            {
                Vector3 localScale = rigidbody.transform.localScale;
                localScale.x *= -1f;
                rigidbody.transform.localScale = localScale;
            }
            _isFacingRight = !_isFacingRight;
            _rbs.Reverse();
        }
    }

    private IEnumerator Jump()
    {
        var normalJump = jumpForce;
        for (int i = 0; i < _rbs.Count; i++)
        {
            if (_rbs[i].name == "Duck")
                jumpForce += 3f;
            else
                jumpForce = normalJump;
            duckAnimators[i].SetBool("isJump", true);
            _rbs[i].AddForce(new Vector2(_rbs[i].velocity.x, jumpForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);
        }
    }
}
