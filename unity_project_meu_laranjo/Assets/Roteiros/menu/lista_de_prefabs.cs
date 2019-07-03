using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lista_de_prefabs : MonoBehaviour
{
    public lista_de_prefabs instancia;
    public List<item> itens;
    public item_casa[] lista_casa;
    public peca_terreno[] pecas;

    // Start is called before the first frame update
    void Awake()
    {
        instancia = this;
    }

    void Start() {
        itens = gerenciador.instancia.itens;
        pecas = gerenciador.instancia.pecas;
    }
}
