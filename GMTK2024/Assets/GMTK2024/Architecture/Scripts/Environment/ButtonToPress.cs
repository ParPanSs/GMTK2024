using System;
using System.Collections;
using UnityEngine;

public class ButtonToPress : MonoBehaviour
{
    [Serializable]
    public enum ButtonType
    {
        OnePress,
        Hold
    }
    [Serializable]
    public enum ButtonSide
    {
        Top,
        Bottom
    }
    
    [SerializeField] private GameObject whatToInteract;
    [SerializeField] private Vector3 positionToMoveObject;
    [SerializeField] private Vector3 positionToMoveButton;
    [SerializeField] private ButtonType type;
    [SerializeField] private ButtonSide side;
    
    private Vector3 _originalButtonPosition;
    private Vector3 _originalObjectPosition;

    private bool _isButtonPressed;

    private void Start()
    {
        _originalButtonPosition = transform.position;
        _originalObjectPosition = whatToInteract.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            if (side == ButtonSide.Bottom)
                other.transform.parent = transform;
            _isButtonPressed = true;
            StartCoroutine(ButtonPressed());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isButtonPressed = false;
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            if (type == ButtonType.Hold)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _originalButtonPosition, 1f);
                whatToInteract.transform.localPosition =
                    Vector3.Lerp(whatToInteract.transform.localPosition, _originalObjectPosition, 1f);
            }
            if (side == ButtonSide.Bottom)
                other.transform.parent = null;
        }
    }

    private IEnumerator ButtonPressed()
    {
        if (_isButtonPressed)
        {
            for (float i = 0; i < 1; i += Time.deltaTime / 2f)
            {
                transform.localPosition =
                    Vector3.Lerp(transform.localPosition, positionToMoveButton, i);
                whatToInteract.transform.localPosition =
                    Vector3.Lerp(whatToInteract.transform.localPosition, positionToMoveObject, i);
                yield return null;
            }
        }
    }
}
