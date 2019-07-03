using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "item", menuName = "Laranjo/Inventario/Item Casa")]
public class item_casa : ScriptableObject
{
    public int id, id_ordem;
    public string nome;
    public string descricao;
    public int preco;
    public GameObject prefab_casa, prefab_garagem;
    public Sprite imagem;
    public raridadeItemCasa raridade;


    public enum raridadeItemCasa
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
