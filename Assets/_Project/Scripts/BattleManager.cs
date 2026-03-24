using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance;
    public Entity player, enemy;
    
    [Header("Sistema de Pipeline")]
    public List<ActionCard> codePipeline = new List<ActionCard>();
    public string lockedAttribute = "";
    public bool isPointerActive = false;
    
    [Header("UI Feedback")]
    public TextMeshProUGUI terminalLog;
    
    private void Awake() {
        Instance = this;
    }
    
    public void AddCardToPipeline(ActionCard card) {
        codePipeline.Add(card);
        LogToTerminal($"> {card.type} added to pipeline");
    }
    
    public void ExecuteScript() {
        if (codePipeline.Count == 0) {
            LogToTerminal("> Error: Empty pipeline");
            return;
        }
        
        StartCoroutine(ExecuteScriptCoroutine());
    }
    
    private IEnumerator ExecuteScriptCoroutine() {
        LogToTerminal("> Compiling script...");
        yield return new WaitForSeconds(0.5f);
        
        // Verificação de Syntax Fault
        if (!ValidateSyntax()) {
            LogToTerminal("> SYNTAX ERROR: Invalid script structure");
            LogToTerminal("> SEGMENTATION FAULT - Taking 10 damage");
            player.ModifyStat("hp", -10);
            codePipeline.Clear();
            yield break;
        }
        
        LogToTerminal("> Syntax validated successfully");
        yield return new WaitForSeconds(0.3f);
        
        // Execução em pipeline
        float totalDamage = 0;
        bool conditionPassed = true;
        
        for (int i = 0; i < codePipeline.Count; i++) {
            var card = codePipeline[i];
            
            LogToTerminal($"> Executing: {card.type}");
            yield return new WaitForSeconds(0.2f);
            
            // Processa cartas de lógica
            if (IsLogicCard(card)) {
                conditionPassed = ProcessLogicCard(card);
                LogToTerminal($"> Logic result: {(conditionPassed ? "TRUE" : "FALSE")}");
                continue;
            }
            
            // Processa cartas de ponteiro
            if (card.type == ActionCard.ActionType.PTR_SCAN) {
                isPointerActive = true;
                LogToTerminal("> Pointer activated - locking target attribute");
                continue;
            }
            
            // Processa cartas de alvo
            if (IsTargetCard(card)) {
                if (!isPointerActive) {
                    lockedAttribute = GetAttributeFromCard(card);
                    LogToTerminal($"> Target locked: {lockedAttribute}");
                }
                continue;
            }
            
            // Processa cartas de payload
            if (IsPayloadCard(card) && conditionPassed) {
                float cardDamage = ProcessPayloadCard(card);
                totalDamage += cardDamage;
                LogToTerminal($"> Payload injected: {cardDamage} damage");
            }
        }
        
        // Aplica dano final com Overclock
        float complexityMultiplier = 1f + (codePipeline.Count * 0.2f);
        float finalDamage = totalDamage * complexityMultiplier;
        
        LogToTerminal($"> Calculating final damage: {totalDamage} * {complexityMultiplier:F1} = {finalDamage:F1}");
        yield return new WaitForSeconds(0.3f);
        
        enemy.ModifyStat("hp", -Mathf.RoundToInt(finalDamage));
        
        LogToTerminal("> PAYLOAD EXECUTED SUCCESSFULLY");
        LogToTerminal($"> Total damage dealt: {Mathf.RoundToInt(finalDamage)}");
        
        // Limpa pipeline e reset
        codePipeline.Clear();
        lockedAttribute = "";
        isPointerActive = false;
    }
    
    private bool ValidateSyntax() {
        for (int i = 0; i < codePipeline.Count; i++) {
            var card = codePipeline[i];
            
            // Verifica se operador vem antes de lógica
            if (IsOperatorCard(card) && i > 0) {
                var prevCard = codePipeline[i - 1];
                if (!IsLogicCard(prevCard) && !IsTargetCard(prevCard)) {
                    return false;
                }
            }
            
            // Verifica se valor vem sem variável
            if (IsValueCard(card) && i > 0) {
                var prevCard = codePipeline[i - 1];
                if (!IsTargetCard(prevCard) && !IsOperatorCard(prevCard)) {
                    return false;
                }
            }
        }
        return true;
    }
    
    private bool IsLogicCard(ActionCard card) {
        return card.type == ActionCard.ActionType.IF || 
               card.type == ActionCard.ActionType.ELSE || 
               card.type == ActionCard.ActionType.FOR;
    }
    
    private bool IsOperatorCard(ActionCard card) {
        return card.type == ActionCard.ActionType.LESS_THAN || 
               card.type == ActionCard.ActionType.GREATER_THAN ||
               card.type == ActionCard.ActionType.EQUALS;
    }
    
    private bool IsTargetCard(ActionCard card) {
        return card.type == ActionCard.ActionType.TARGET_HP || 
               card.type == ActionCard.ActionType.TARGET_SHIELD;
    }
    
    private bool IsValueCard(ActionCard card) {
        return card.type == ActionCard.ActionType.VALUE_50 || 
               card.type == ActionCard.ActionType.VALUE_100 ||
               card.type == ActionCard.ActionType.VALUE_MAX;
    }
    
    private bool IsPayloadCard(ActionCard card) {
        return card.type == ActionCard.ActionType.BRUTE_FORCE || 
               card.type == ActionCard.ActionType.SQL_INJECTION ||
               card.type == ActionCard.ActionType.LOG_CLEANER;
    }
    
    private bool ProcessLogicCard(ActionCard card) {
        switch (card.type) {
            case ActionCard.ActionType.IF:
                return enemy.GetStatValue("hp") > 50;
            case ActionCard.ActionType.ELSE:
                return enemy.GetStatValue("hp") <= 50;
            case ActionCard.ActionType.FOR:
                return true; // FOR sempre executa 3x
            default:
                return true;
        }
    }
    
    private string GetAttributeFromCard(ActionCard card) {
        switch (card.type) {
            case ActionCard.ActionType.TARGET_HP: return "hp";
            case ActionCard.ActionType.TARGET_SHIELD: return "shield";
            default: return "hp";
        }
    }
    
    private float ProcessPayloadCard(ActionCard card) {
        switch (card.type) {
            case ActionCard.ActionType.BRUTE_FORCE:
                return 30 + card.powerValue;
            case ActionCard.ActionType.SQL_INJECTION:
                return 25 + card.powerValue; // Ignora shield
            case ActionCard.ActionType.LOG_CLEANER:
                player.ModifyStat("hp", card.powerValue);
                return 0;
            default:
                return card.powerValue;
        }
    }
    
    private void LogToTerminal(string message) {
        if (terminalLog != null) {
            terminalLog.text += message + "\n";
        }
        Debug.Log($"[BATTLE MANAGER] {message}");
    }
}
