using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Laranjo/Moveis/Item de Pintura")]
public class item_pintura_movel : ScriptableObject
{
    public int id, id_ordem;
    public string[] nome;
    public string[] descricao;
    public bool listado = true;
    public int preco_moedas;
    public int preco_dolares;
    public Texture2D textura;
    public Sprite imagem;
    public raridadeItemRoupa raridade;

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
