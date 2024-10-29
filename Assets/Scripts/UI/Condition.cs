using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour //���� �������� ü�� �ۿ� ��� �������̽� ������ ����
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
