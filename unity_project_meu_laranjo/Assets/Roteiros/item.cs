using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Laranjo/Inventario/Item")]
public class item : ScriptableObject
{
    public int id, id_ordem;
    public string nome;
    public string descricao;
    public int preco;
    public int posicao;
    public GameObject prefab;
    public Sprite imagem;
    public int raridade;
    public bool seguraItem = false; 

}
