using UnityEngine;

[CreateAssetMenu(fileName = "NewLogicCard", menuName = "Game/Cards/Logic")]
public class LogicCard : CardData
{
    public bool isLiteral; 
    public float literalValue;
    
    public bool isOperator; 
    public ComparisonOperator op;

    public override void ApplyEffect()
    {
        if (BattleManager.Instance == null) return;

        // Converte LogicCard para ActionCard para compatibilidade
        if (isLiteral) {
            // Cria uma ActionCard de valor temporária
            var valueCard = ScriptableObject.CreateInstance<ActionCard>();
            valueCard.type = isLiteral ? ActionCard.ActionType.VALUE_50 : ActionCard.ActionType.VALUE_100;
            valueCard.powerValue = Mathf.RoundToInt(literalValue);
            BattleManager.Instance.AddCardToPipeline(valueCard);
        }
        
        if (isOperator) {
            // Cria uma ActionCard de operador temporária
            var opCard = ScriptableObject.CreateInstance<ActionCard>();
            switch (op) {
                case ComparisonOperator.LessThan:
                    opCard.type = ActionCard.ActionType.LESS_THAN;
                    break;
                case ComparisonOperator.GreaterThan:
                    opCard.type = ActionCard.ActionType.GREATER_THAN;
                    break;
                case ComparisonOperator.Equals:
                    opCard.type = ActionCard.ActionType.EQUALS;
                    break;
            }
            BattleManager.Instance.AddCardToPipeline(opCard);
        }
    }
}