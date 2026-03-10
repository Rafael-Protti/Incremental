using UnityEngine;

public class SaveManagerPlayerPrefs : MonoBehaviour
{
    public static void Salvar()
    {
        PlayerPrefs.SetFloat("dinheiro", GameDirector.instancia.levelManager.dinheiro);
        PlayerPrefs.SetInt("multiplicador", GameDirector.instancia.levelManager.qntMultiplicador);
        PlayerPrefs.SetInt("ganhoPassivo", GameDirector.instancia.levelManager.qntGanhosPassivos);
        PlayerPrefs.Save();

        Debug.Log("Jogo salvado.!.!.!.!.!");
    }

    public static void Carregar()
    {
        GameDirector.instancia.levelManager.AddDinheiro(PlayerPrefs.GetFloat("dinheiro", 0));
        GameDirector.instancia.levelManager.qntMultiplicador = PlayerPrefs.GetInt("multiplicador", 1);
        GameDirector.instancia.levelManager.qntGanhosPassivos = PlayerPrefs.GetInt("ganhoPassivo", 1);
    }
}
