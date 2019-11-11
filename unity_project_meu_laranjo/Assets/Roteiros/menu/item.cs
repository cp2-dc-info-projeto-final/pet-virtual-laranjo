using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Laranjo/Inventario/Item de Roupa")]
public class item : ScriptableObject
{
    public int id, id_ordem;
    public string[] nome;
    public string[] descricao;
    public bool listado = true;
    public int preco_moedas;
    public int preco_dolares;
    public PosicaoItemRoupa posicao;
    public GameObject prefab;
    public Sprite imagem;
    public raridadeItemRoupa raridade;
    public bool seguraItem = false;

    public enum PosicaoItemRoupa
    {
        Cabelo = 1,
        Sobrancelha,
        Bigode,
        Barba,
        ItemMao,
        Chapeu,
        Oculos,
        Colar,
        Roupa,
        Sapato,
        Skin
    }

    public enum raridadeItemRoupa
    {
        SemNivel = 0,
        Nivel1,
        Nivel2,
        Nivel3,
        Nivel4,
        Nivel5,
        Nivel6
    }
}
