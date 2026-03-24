using UnityEngine;

[System.Serializable]
public class Stat
{
    public string statName;
    public int baseValue;
    private int _currentValue;

    public int Value 
    {
        get => _currentValue;
        set => _currentValue = Mathf.Clamp(value, 0, 999); // Garante que não seja negativo
    }

    public void Initialize()
    {
        _currentValue = baseValue;
    }
}