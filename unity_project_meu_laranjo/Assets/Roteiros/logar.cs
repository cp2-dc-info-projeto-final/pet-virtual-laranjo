using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class logar : MonoBehaviour
{
    public TMP_InputField log_nick, log_senha;
    public string site, nick, senha, st1, st2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botao_login(){
        nick = log_nick.text;
        senha = log_senha.text;
        StartCoroutine(fazerLogin(nick,senha));
    }

    IEnumerator fazerLogin(string login_, string senha_){
        

        WWWForm form = new WWWForm();
        form.AddField("nickPost", login_);
        form.AddField("senhaPost", senha_);

        UnityWebRequest link = UnityWebRequest.Post(site,form);

        yield return link.SendWebRequest();

        if(link.isNetworkError || link.isHttpError){
            st1 = "ERROOOU " + link.error;
        }else
        {
            st1 = link.downloadHandler.text;
        }

    }
}
