using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class logar : MonoBehaviour
{

    public string site, nick, senha, st1, st2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fazerLogin(nick,senha));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fazerLogin(string login_, string senha_){
        

        WWWForm form = new WWWForm();
        form.AddField("nickPost", login_);
        form.AddField("senhaPost", senha_);

        st1 = form.ToString();

        st2 = form.headers.ToString();

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
