using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Identificação")]
    public string unitName;
    public bool isPlayer;

    [Header("Registros de Memória (Atributos)")]
    public Stat hp;
    public Stat shield;
    public Stat atk;

    void Awake()
    {
        // Inicializa os valores base no início do jogo
        hp.Initialize();
        shield.Initialize();
        atk.Initialize();
    }

    // Método universal para alterar atributos via cartas
    public void ModifyStat(string statType, int amount)
    {
        switch (statType.ToLower())
        {
            case "hp":
                // Se for dano (amount negativo), checa o escudo primeiro
                if (amount < 0 && shield.Value > 0)
                {
                    shield.Value += amount; // Amount é negativo, então subtrai
                    if (shield.Value < 0)
                    {
                        hp.Value += shield.Value;
                        shield.Value = 0;
                    }
                }
                else
                {
                    hp.Value += amount;
                }
                break;

            case "shield":
                shield.Value += amount;
                break;

            case "atk":
                atk.Value += amount;
                break;
        }

        Debug.Log($"[LOG]: {unitName}.{statType} alterado para {GetStatValue(statType)}");
    }

    // Mudamos para um Switch clássico para evitar erro de versão
    public int GetStatValue(string statType)
    {
        switch (statType.ToLower())
        {
            case "hp":
                return hp.Value;
            case "shield":
                return shield.Value;
            case "atk":
                return atk.Value;
            default:
                return 0;
        }
    }
}