using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class logar : MonoBehaviour
{
    UnityWebRequest link;
    public TMP_InputField log_nick, log_senha;
    public string site, nick, senha, st1, st2;
    public string[] resposta; // = new List<string>(); 
    public bool carregando = false;
    public Slider barra_carregamento;
    public GameObject avisoCarregando;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        avisoCarregando.SetActive(carregando);

        if(carregando){
            barra_carregamento.value = Mathf.Lerp(barra_carregamento.value,link.uploadProgress,Time.deltaTime * 4);
        }
    }

    public void botao_login(){
        nick = log_nick.text;
        senha = log_senha.text;

        barra_carregamento.value = 0.0f;
        StartCoroutine(fazerLogin(nick,senha));
    }

    IEnumerator fazerLogin(string login_, string senha_){
        

        WWWForm form = new WWWForm();
        form.AddField("nickPost", login_);
        form.AddField("senhaPost", senha_);

        link = UnityWebRequest.Post(site,form);

        carregando = true;

        yield return link.SendWebRequest();

        carregando = false;

        if(link.isNetworkError || link.isHttpError){
            st1 = "ERROOOU " + link.error;
            //if(link.error == UnityWebRequest.)
        }else
        {
            resposta = link.downloadHandler.text.Split(',');
        }

    }
}
