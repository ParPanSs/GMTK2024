using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DuckAbility : MonoBehaviour
{
    [SerializeField] private KeyCode keyToSetAbility;
    private Sequence _sequence;
    private Guid _uid;
    private bool _abilityStatus;
    private bool _isPulsing;
    private bool _isScaling;
    
    private void Update()
    {
        if (!Input.GetKeyDown(keyToSetAbility)) return;
        if (_isScaling) return;
        SetAbilityStatus(keyToSetAbility, _abilityStatus);
        _abilityStatus = !_abilityStatus;
    }

    private void SetAbilityStatus(KeyCode keyCode, bool newAbilityStatus)
    {
        switch (keyCode)
        {
            case KeyCode.G:
                StartCoroutine(newAbilityStatus ? 
                    SetAbility(new Vector2(1f, 1f), 1f) : 
                    SetAbility(new Vector2(3f, 3f), 1f));
                break;
            case KeyCode.M:
                StartCoroutine(newAbilityStatus ? 
                    SetAbility(new Vector2(1f, 1f), 1f) :
                    SetAbility(new Vector2(.5f, .5f), 1f));
                break;
            case KeyCode.T:
                StartCoroutine(newAbilityStatus ? 
                    SetAbility(new Vector2(1f, 1f), 1f) : 
                    SetAbility(new Vector2(3f, 1f), 1f));
                break;
            case KeyCode.K:
                _isPulsing = !_isPulsing;
                if (_sequence == null)
                {
                    _sequence = DOTween.Sequence();
                    _sequence.Append(transform.DOScale(new Vector2(3f, 3f), .1f))
                        .SetLoops(-1, LoopType.Yoyo);
                    _sequence.id = _uid;
                }
                switch (_isPulsing)
                {
                    case true:
                        _sequence.Play();
                        break;
                    case false:
                        DOTween.Kill(_uid);
                        _sequence = null;
                        StartCoroutine(SetAbility(new Vector2(1f, 1f), .1f));
                        break;
                }
                break;
        }
    }

    private IEnumerator SetAbility(Vector2 targetScale, float scaleDuration)
    {
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