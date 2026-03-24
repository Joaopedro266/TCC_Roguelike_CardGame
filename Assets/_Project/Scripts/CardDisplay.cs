using UnityEngine;
using UnityEngine.UI;
using TMPro; // Usado para textos na interface

public class CardDisplay : MonoBehaviour
{
    [Header("Dados da Carta")]
    public CardData cardData;

    [Header("Elementos de UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    // No CardDisplay.cs, adicione:
    public Image cardBackground;
 
    public void Setup(CardData newData) {
    cardData = newData;
    nameText.text = cardData.cardName;
    descriptionText.text = cardData.description;
    
    // Configura cor baseada no tipo
    if (cardData is ActionCard actionCard) {
        switch (actionCard.cardColor) {
            case "Amarelo": cardBackground.color = Color.yellow; break;
            case "Azul": cardBackground.color = Color.blue; break;
            case "Vermelho": cardBackground.color = Color.red; break;
            case "Cinza": cardBackground.color = Color.gray; break;
            case "Roxo": cardBackground.color = new Color(0.5f, 0f, 1f, 1f); break;
            default: cardBackground.color = Color.white; break;
        }
    }
}

    // Este método será chamado quando o jogador clicar no botão da carta
    public void OnCardClicked()
    {
        if (cardData != null)
        {
            cardData.ApplyEffect(); // Dispara a lógica que você fez no ActionCard
            Debug.Log($"[AÇÃO]: Carta {cardData.cardName} jogada!");
            
            // Destrói a carta visualmente da mão após o uso
            Destroy(gameObject); 
        }
    }
}