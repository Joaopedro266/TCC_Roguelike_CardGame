using UnityEngine;

[CreateAssetMenu(fileName = "NewActionCard", menuName = "Game/Cards/Action")]
public class ActionCard : CardData
{
    public enum ActionType { 
        // Fast-Tokens Amarelos (Lógica)
        IF, ELSE, FOR,
        
        // Fast-Tokens Azuis (Alvo)
        TARGET_HP, TARGET_SHIELD,
        
        // Fast-Tokens Vermelhos (Payload)
        BRUTE_FORCE, SQL_INJECTION, LOG_CLEANER,
        
        // Fast-Tokens Cinzas (Valor)
        VALUE_50, VALUE_100, VALUE_MAX,
        
        // Fast-Tokens Especiais (Operadores)
        LESS_THAN, GREATER_THAN, EQUALS,
        
        // Fast-Token Especial (Ponteiro)
        PTR_SCAN
    }
    
    [Header("Configurações da Carta")]
    public ActionType type;
    public int powerValue;
    public new string description;
    public string cardColor;
    
    private void OnValidate() {
        // Auto-atribui cores baseadas no tipo
        switch (type) {
            case ActionType.IF:
            case ActionType.ELSE:
            case ActionType.FOR:
                cardColor = "Amarelo";
                break;
                
            case ActionType.TARGET_HP:
            case ActionType.TARGET_SHIELD:
                cardColor = "Azul";
                break;
                
            case ActionType.BRUTE_FORCE:
            case ActionType.SQL_INJECTION:
            case ActionType.LOG_CLEANER:
                cardColor = "Vermelho";
                break;
                
            case ActionType.VALUE_50:
            case ActionType.VALUE_100:
            case ActionType.VALUE_MAX:
                cardColor = "Cinza";
                break;
                
            case ActionType.LESS_THAN:
            case ActionType.GREATER_THAN:
            case ActionType.EQUALS:
                cardColor = "Branco";
                break;
                
            case ActionType.PTR_SCAN:
                cardColor = "Roxo";
                break;
        }
        
        // Auto-atribui descrições
        description = GetCardDescription();
    }
    
    private string GetCardDescription() {
        switch (type) {
            case ActionType.IF: return "Executa se HP > 50";
            case ActionType.ELSE: return "Executa se HP <= 50";
            case ActionType.FOR: return "Repete 3x";
            case ActionType.TARGET_HP: return "Altera HP";
            case ActionType.TARGET_SHIELD: return "Altera Shield";
            case ActionType.BRUTE_FORCE: return "Dano direto";
            case ActionType.SQL_INJECTION: return "Ignora Shield";
            case ActionType.LOG_CLEANER: return "Cura HP";
            case ActionType.VALUE_50: return "Valor 50";
            case ActionType.VALUE_100: return "Valor 100";
            case ActionType.VALUE_MAX: return "Valor Máximo";
            case ActionType.LESS_THAN: return "<";
            case ActionType.GREATER_THAN: return ">";
            case ActionType.EQUALS: return "==";
            case ActionType.PTR_SCAN: return "& - Lock alvo";
            default: return "Carta de Ação";
        }
    }
    
    public override void ApplyEffect() {
        var bm = BattleManager.Instance;
        if (bm != null) {
            bm.AddCardToPipeline(this);
        }
    }
    
    public bool IsLogicCard() {
        return type == ActionType.IF || type == ActionType.ELSE || type == ActionType.FOR;
    }
    
    public bool IsTargetCard() {
        return type == ActionType.TARGET_HP || type == ActionType.TARGET_SHIELD;
    }
    
    public bool IsPayloadCard() {
        return type == ActionType.BRUTE_FORCE || type == ActionType.SQL_INJECTION || type == ActionType.LOG_CLEANER;
    }
    
    public bool IsValueCard() {
        return type == ActionType.VALUE_50 || type == ActionType.VALUE_100 || type == ActionType.VALUE_MAX;
    }
    
    public bool IsOperatorCard() {
        return type == ActionType.LESS_THAN || type == ActionType.GREATER_THAN || type == ActionType.EQUALS;
    }
    
    public bool IsPointerCard() {
        return type == ActionType.PTR_SCAN;
    }
}
