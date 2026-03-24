using UnityEngine;

// Este script não vai em nenhum objeto, ele serve de base para os outros
public abstract class CardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string description;

    // Toda carta vai implementar o seu próprio efeito aqui
    public abstract void ApplyEffect();
}