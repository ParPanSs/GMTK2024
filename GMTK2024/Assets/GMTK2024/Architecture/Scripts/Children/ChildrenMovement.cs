using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenMovement : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> children;
    [SerializeField] private float jumpForce;
    
    public IEnumerator Jump()
    {
        for (int i = children.Count - 1; i >= 0; i--)
        {
            children[i].AddForce(new Vector2(children[i].velocity.x, jumpForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);
        }
    }
}
