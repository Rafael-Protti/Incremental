using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Dinheiro coletado no jogo")]
    public float dinheiro { get; private set; }
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
    [Space]
    [Header("Preço dos ganhos base do multiplicador e do ganho passivo")]
    public float precoBaseMultiplicador;
    public float multiplicadorMultiplicador;
    public float precoBaseGanhoPassivo;
    public float multiplicadorGanhoPassivo;


    public static event System.Action OnDinheiroChange;


    private void Start()
    {
        StartCoroutine(RotinaGanhoPassivo());
    }

    public void CliqueLimao()
    {
        float valor = 1;

        float multiplicador = qntMultiplicador * valorMultiplicador;
        valor *= multiplicador;

        AddDinheiro(valor);
    }

    public void AddDinheiro(float valor)
    {
        dinheiro += valor;

        OnDinheiroChange?.Invoke();
    }

    public void ComprarMultiplicador()
    {
        float preco = ObtemPrecoMultiplicador();

        if (dinheiro < preco)
        {
            return;
        }

        AddDinheiro(-preco);

        qntMultiplicador++;

        GameDirector.instancia.hudManager.AtualizarMultiplicador();
    }

    public void ComprarGanhoPassivo()
    {
        float preco = ObtemPrecoGanhoPassivo();

        if (dinheiro < preco)
        {
            return;
        }

        AddDinheiro(-preco);

        qntGanhosPassivos++;

        GameDirector.instancia.hudManager.AtualizarGanhoPassivo();
    }

    public float ObtemPrecoMultiplicador()
    {
        float preco = qntMultiplicador * precoBaseMultiplicador * multiplicadorMultiplicador;

        return preco;
    }

    public float ObtemPrecoGanhoPassivo()
    {
        float preco = qntGanhosPassivos * precoBaseGanhoPassivo * multiplicadorGanhoPassivo;

        return preco;
    }

    IEnumerator RotinaGanhoPassivo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float valor = qntGanhosPassivos * valorGanhoPassivo;
            AddDinheiro(qntGanhosPassivos - 1);
        }
    }
}
