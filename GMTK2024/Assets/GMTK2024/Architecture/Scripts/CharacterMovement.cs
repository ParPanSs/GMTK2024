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
    [SerializeField] private DuckAbility garryAbility;
    private float _horizontal;
    private float _startSpeed;
    private bool _canJump = true;
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        _startSpeed = speed;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        Collider2D ray = Physics2D.OverlapCircle(legs.transform.position, .05f, whatIsGround);
        if (ray != null)
        {
            speed = _startSpeed;
        }
        else
        {
            speed = 7f;
        }
        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            _canJump = false;
            StartCoroutine(Jump());
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
                // Vector3 localScale = rigidbody.transform.localScale;
                // localScale.x *= -1f;
                // rigidbody.transform.localScale = localScale;
                rigidbody.transform.rotation = Quaternion.Euler(0f, rigidbody.transform.rotation.y + 180 * _isFacingRight.GetHashCode(), 0f);
            }
            _isFacingRight = !_isFacingRight;
            StartCoroutine(ReverseArray());
        }
    }

    private IEnumerator ReverseArray()
    {
        if (!_canJump)
        {
            yield return new WaitUntil(() => _canJump);
        }
        _rbs.Reverse();
        duckAnimators.Reverse();
    }

    private IEnumerator Jump()
    {
        var normalJump = jumpForce;
        for (int i = 0; i < _rbs.Count; i++)
        {
            if (_rbs[i].name == "Duck" || garryAbility.gameObject == _rbs[i].gameObject)
                jumpForce += 2f;
            else
                jumpForce = normalJump;
            duckAnimators[i].SetBool("isJump", true);
            _rbs[i].AddForce(new Vector2(_rbs[i].velocity.x, jumpForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);
        }
        jumpForce = normalJump;
        _canJump = true;
    }
}
