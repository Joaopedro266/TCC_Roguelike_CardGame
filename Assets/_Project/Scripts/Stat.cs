using UnityEngine;

[System.Serializable]
public class Stat
{
    [Header("Configurações")]
    public string statName;
    public int baseValue;
    public int maxValue = 999;
    
    [Header("Valores Atuais")]
    [SerializeField] private int _currentValue;
    private int _tempValue;

    public int Value 
    {
        get => _currentValue;
        set => _currentValue = Mathf.Clamp(value, 0, maxValue);
    }

    public int MaxValue => maxValue;
    public float Percentage => (float)_currentValue / maxValue;
    
    public void Initialize()
    {
        _currentValue = baseValue;
    }
    
    public void Modify(int amount)
    {
        _currentValue = Mathf.Clamp(_currentValue + amount, 0, maxValue);
    }
    
    public void SetTemp(int value)
    {
        _tempValue = value;
    }
    
    public void ApplyTemp()
    {
        _currentValue = Mathf.Clamp(_tempValue, 0, maxValue);
    }
    
    public void Reset()
    {
        _currentValue = baseValue;
    }
}