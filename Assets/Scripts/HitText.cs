using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _healColor;
    [SerializeField] private Color _hitColor;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _timeToDeactive = 0.2f;
    [SerializeField] private float _timeToWait = 0.2f;

    private void Start() {
        StartCoroutine(SerTransparentText());
    }

    public void Setup(float damage, bool isHeal) {
        string prev = isHeal ? "+": "-";
        _text.text = prev + damage.ToString();
        _text.color = isHeal ? _healColor : _hitColor;
    }

    private void Update() {
        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    IEnumerator SerTransparentText() {
        Color color = _text.color;
        yield return new WaitForSeconds(_timeToWait);
        for (float t = 0; t < 1f ; t+= Time.deltaTime / _timeToDeactive) {
            color.a = 1 - t;
            _text.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
}
