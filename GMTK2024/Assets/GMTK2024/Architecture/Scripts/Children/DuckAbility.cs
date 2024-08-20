using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DuckAbility : MonoBehaviour
{
    [SerializeField] private KeyCode keyToSetAbility;
    [SerializeField] private Trampoline trampoline;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator abilityAnimator;
    private Sequence _sequence;
    private Guid _uid;
    private bool _abilityStatus;
    private bool _isPulsing;
    private bool _isScaling;
    public bool isInState { get; private set; }

    private void Update()
    {
        if (!Input.GetKeyDown(keyToSetAbility)) return;
        if (_isScaling) return;
        SetAbilityStatus(keyToSetAbility, _abilityStatus);
        _abilityStatus = !_abilityStatus;
    }

    private void SetAbilityStatus(KeyCode keyCode, bool newAbilityStatus)
    {
        transitionAnimator.StopPlayback(); 
        transitionAnimator.Play("AbilityTransition");
        switch (keyCode)
        {
            case KeyCode.G:
                isInState = !isInState;
                StartCoroutine(newAbilityStatus ? 
                    SetAbility(new Vector2(1f, 1f), 1f, false) : 
                    SetAbility(new Vector2(3f, 3f), 1f, true));
                break;
            case KeyCode.M:
                StartCoroutine(newAbilityStatus ? 
                    SetAbility(new Vector2(1f, 1f), 1f, false) :
                    SetAbility(new Vector2(.5f, .5f), 1f, true));
                break;
            case KeyCode.T:
                StartCoroutine(newAbilityStatus ? 
                    SetAbility(new Vector2(1f, 1f), .5f, false) : 
                    SetAbility(new Vector2(2f, 1f), .5f, true));
                break;
            case KeyCode.K:
                if (newAbilityStatus)
                {
                    StartCoroutine(SetAbility(new Vector2(1f, 1f), 1f, false));
                    trampoline.gameObject.SetActive(false);
                }
                else
                {
                    StartCoroutine(SetAbility(new Vector2(1f, 1f), 1f, true));
                    trampoline.gameObject.SetActive(true);
                }
                // StartCoroutine(newAbilityStatus ? 
                //     SetAbility(new Vector2(1f, 1f), 1f) : 
                //     SetAbility(new Vector2(3f, 3f), 1f));
                // _isPulsing = !_isPulsing;
                // if (_sequence == null)
                // {
                //     _sequence = DOTween.Sequence();
                //     _sequence.Append(whatToScale.transform.DOScale(new Vector2(3f * transform.localScale.x, 3f), 1f))
                //         .SetLoops(-1, LoopType.Yoyo);
                //     _sequence.id = _uid;
                // }
                // switch (_isPulsing)
                // {
                //     case true:
                //         StartCoroutine(SetAbility(new Vector2(3f, 3f), .1f));
                //         _sequence.Play();
                //         trampoline.gameObject.SetActive(true);
                //         break;
                //     case false:
                //         DOTween.Kill(_uid);
                //         _sequence = null;
                //         trampoline.gameObject.SetActive(false);
                //         StartCoroutine(SetAbility(new Vector2(1f, 1f), .1f));
                //         break;
                // }
                break;
        }
    }

    private IEnumerator SetAbility(Vector2 targetScale, float scaleDuration, bool abilityStatus)
    {        
        if (abilityAnimator)
            abilityAnimator.SetBool("isAbility", abilityStatus);
        var defaultScale = new Vector2(1f, 1f);
        if (transform.localScale.x < 0)
        {
            targetScale.x *= -1;
            defaultScale.x *= -1;
        }

        _isScaling = true;
        Vector2 currentScale = transform.localScale;
     
        for (float t = 0; t < 1; t += Time.deltaTime / scaleDuration)
        {
            transform.localScale = Vector3.Lerp(currentScale ,targetScale, t);
            yield return null;
        }

        _isScaling = false;
    }
}