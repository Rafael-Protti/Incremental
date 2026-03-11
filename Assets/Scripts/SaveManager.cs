using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager
{
    //JSON -> JavaScript Object Notation

    static string arquivo = "savegame.json";
    static string caminho = Path.Combine(Application.persistentDataPath, arquivo);

    static string key = "incrementalsenac";
    public static void Salvar()
    {
        try
        {
            GameData gd = CapturarEstado();
            string json = JsonUtility.ToJson(gd);

            //Criptografia usando AES
#if !UNITY_EDITOR
            json = Encrypt(json);
#endif
            File.WriteAllText(caminho, json);

            Debug.Log("Arquivo salvo em: " + caminho);
        }

        catch (System.Exception e)
        {
            Debug.Log("Erro ao salvar: " + e.Message);
        }

    }

    public static void Carregar()
    {
        if (!File.Exists(caminho))
        {
            Debug.Log("Arquivo de Save não encontrado");
            return;
        }

        try
        {
            string json = File.ReadAllText(caminho);
#if !UNITY_EDITOR
            json = Decrypt(json);
#endif
            GameData gd = JsonUtility.FromJson<GameData>(json);
            AplicarEstado(gd);
        }
        catch (System.Exception e)
        {
            Debug.Log("Não foi possível carrega o arquivo: " + e.Message);
        }
    }

    public static void Deletar_TrueDelete()
    {
        if (File.Exists(caminho))
        {
            try
            {
                File.Delete(caminho);
                Debug.Log("Arquivo Deletado!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            catch(System.Exception e)
            {
                Debug.Log("Erro ao deletar arquivo: " + e.Message);
            }
        }
    }

    public static void Delete_Reset()
    {
        try
        {
            GameData gd = new GameData();
            string json = JsonUtility.ToJson(gd);

            File.WriteAllText(caminho, json);

            Debug.Log("Arquivo resetado com sucesso em: " + caminho);
        }

        catch (System.Exception e)
        {
            Debug.Log("Erro ao salvar arquivo resetado: " + e.Message);
        }
    }

    public static GameData CapturarEstado()
    {
        GameData gd = new GameData();

        gd.versao = GameManager.VERSAO;
        gd.tempoJogo = GameManager.tempoJogo;

        gd.dinheiro = GameDirector.instancia.levelManager.dinheiro;
        gd.qntMultiplicador = GameDirector.instancia.levelManager.qntMultiplicador;
        gd.qntGanhoPassivo = GameDirector.instancia.levelManager.qntGanhoPassivo;
        return gd;
    }

    public static void AplicarEstado(GameData gd)
    {
        GameManager.tempoJogo = gd.tempoJogo;
        GameDirector.instancia.levelManager.dinheiro = gd.dinheiro;
        GameDirector.instancia.levelManager.qntMultiplicador = gd.qntMultiplicador;
        GameDirector.instancia.levelManager.qntGanhoPassivo = gd.qntGanhoPassivo;
    }


    private static string Encrypt(string plainText)
    {
        using (var rijndael = new RijndaelManaged())
        {
            rijndael.Key = Encoding.UTF8.GetBytes(key);
            rijndael.IV = Encoding.UTF8.GetBytes(key.Substring(0, 16));

            var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }




    private static string Decrypt(string cipherText)
    {
        using (var rijndael = new RijndaelManaged())
        {
            rijndael.Key = Encoding.UTF8.GetBytes(key);
            rijndael.IV = Encoding.UTF8.GetBytes(key.Substring(0, 16));

            var decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

            using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

}

