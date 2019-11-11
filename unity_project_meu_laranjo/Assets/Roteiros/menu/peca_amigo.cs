using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class peca_amigo : MonoBehaviour
{
    public long id;
    public string nick;
    public float nivel;
    public TextMeshProUGUI texto_nick;
    public GameObject perfil_amigo;
    public Image fundo;
    // Start is called before the first frame update
    void Start()
    {
        perfil_amigo = GameObject.Find("perfil_amigo");
        texto_nick.text = nick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botao_amigo(){
        GameObject.Find("ger_amigos").GetComponent<gerAmizades>().botaoMostraPerfil(id);
    }
}
