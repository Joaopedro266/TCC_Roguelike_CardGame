using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [Header("Configurações do Baralho")]
    public List<CardData> deck; // Arraste suas cartas (ScriptableObjects) para cá no Inspector
    
    [Header("Referências de Interface")]
    public GameObject cardPrefab; // O molde visual da carta
    public Transform handContainer; // O seu Hand_Container

    void Start()
    {
        // Compra 4 cartas iniciais para testar o protótipo
        for (int i = 0; i < 4; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (deck.Count == 0) return;

        // Sorteia uma carta aleatória da lista
        CardData randomData = deck[Random.Range(0, deck.Count)];
        
        // Cria a carta visualmente dentro do Hand_Container
        GameObject newCard = Instantiate(cardPrefab, handContainer);
        
        // Passa os dados para o script visual se configurar
        newCard.GetComponent<CardDisplay>().Setup(randomData);
    }
}