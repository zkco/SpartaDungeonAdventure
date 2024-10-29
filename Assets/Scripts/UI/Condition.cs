using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour //현재 게이지가 체력 밖에 없어서 인터페이스 만들지 않음
{
    [SerializeField] private Image _image;
    [SerializeField] private float _maxValue;
    private float CurAmount;
    public float CurValue;
    public float Value;

    private void Update()
    {
        UpdateCondition();
    }

    private void UpdateCondition()
    {
        CurAmount = Mathf.Min(CurValue / _maxValue, 1f);
        _image.fillAmount = CurAmount;
        Regen(Value);
    }
    
    private void Regen(float Value)
    {
        CurValue = Mathf.Min(CurValue += Value * Time.deltaTime, _maxValue);
    }
}
