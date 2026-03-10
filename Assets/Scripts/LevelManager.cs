using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Dinheiro coletado no jogo")]
    public float dinheiro;
    [Space]
    [Header("Quantidade de multiplicadores")]
    public int qntMultiplicador; 
    [Header("Quantidade de Ganhos Passivos")]
    public int qntGanhosPassivos;
    [Space]
    [Header("Valor do multiplicador")]
    public float valorMultiplicador;
    [Header("Valor do ganho que o jogador ganha passivamente")]
    public float valorGanhoPassivo;

    public static event System.Action OnDinheiroChange;

    public void CliqueLimao()
    {
        float valor = 1;

        float multiplicador = qntMultiplicador * valorMultiplicador;
        valor *= multiplicador;

        dinheiro += valor;

        OnDinheiroChange?.Invoke();
    }
}
