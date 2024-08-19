using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenMovement : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> children;
    [SerializeField] private float jumpForce;

    private bool _isFacingRight = true;
    
    public IEnumerator Jump()
    {
        for (int i = children.Count - 1; i >= 0; i--)
        {
            children[i].AddForce(new Vector2(children[i].velocity.x, jumpForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);
        }
    }
    
    public void Flip(bool isFacingRight, float _horizontal)
    {
        foreach (var duckling in children)
        {
            if (isFacingRight && _horizontal < 0 || !isFacingRight && _horizontal > 0)
            {
                Vector3 localScale = duckling.transform.localScale;
                _isFacingRight = !_isFacingRight;
                localScale.x *= -1f;
                duckling.transform.localScale = localScale;
            }
            duckling.GetComponent<FollowDad>().FollowTarget(!isFacingRight);
        }
    }
}
