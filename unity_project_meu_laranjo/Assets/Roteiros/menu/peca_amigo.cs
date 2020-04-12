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
        

        if(nivel < 0.5f){

            fundo.color = Color.Lerp(new Color(0,1,0),new Color(1,1,0),gerDados.instancia.dados_.nivel * 2);

        }else
        {
            fundo.color = Color.Lerp(new Color(1,1,0),new Color(1,113f / 255,50f / 255),(nivel - 0.5f) * 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botao_amigo(){
        GameObject.Find("ger_amigos").GetComponent<gerAmizades>().botaoMostraPerfil(id);
    }
}
