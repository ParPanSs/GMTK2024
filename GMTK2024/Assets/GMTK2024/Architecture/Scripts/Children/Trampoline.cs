using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Rigidbody2D _rb;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            _rb = other.gameObject.GetComponent<Rigidbody2D>();
            _rb.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
        }
    }
}
