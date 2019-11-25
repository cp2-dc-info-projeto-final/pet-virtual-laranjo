using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Facebook.Unity;

public class logar : MonoBehaviour
{
    UnityWebRequest link_log;
    public TMP_InputField log_nick, log_senha;
    public string site_log, site_log_fb, nick, senha, st1, st2;
    public string[] resposta; // = new List<string>(); 
    public string[] dados_FB = new string[5];
    public bool carregando = false;
    public Slider barra_carregamento;
    public GameObject avisoCarregando, menu_conf, menu_logreg, menu_ERRO;

    void Awake() {
        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            Debug.Log("inicializando FB SDK...");

            FB.Init(InitCallback, OnHideUnity);
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    
    }
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void InitCallback ()
    {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void botao_login(){
        nick = log_nick.text;
        senha = log_senha.text;

        barra_carregamento.value = 0.0f;
        StartCoroutine(fazerLogin(nick,senha));
    }

    public void botao_login_FB(){
        List<string> permicoes = new List<string>(){"user_birthday", "email"};
        FB.LogInWithReadPermissions(permicoes, AuthCallback);
    }

    private void AuthCallback (ILoginResult result) {
    if (FB.IsLoggedIn) {

        var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

        FB.API ("me?fields=id,email,birthday,first_name,last_name",HttpMethod.GET, GetFacebookInfo);

        

        } else {
            Debug.Log("User cancelled login");
        }
    }

    public void GetFacebookInfo(IResult resultado_){
    if (resultado_.Error == null) {
        dados_FB[0] = resultado_.ResultDictionary ["id"].ToString ();
        dados_FB[1] = resultado_.ResultDictionary ["email"].ToString ();
        dados_FB[2] = resultado_.ResultDictionary ["first_name"].ToString ();
        dados_FB[3] = resultado_.ResultDictionary ["last_name"].ToString ();
        dados_FB[4] = resultado_.ResultDictionary ["birthday"].ToString ();

        StartCoroutine(fazerLoginFB(dados_FB[0],dados_FB[1]));
    } else {
        Debug.Log (resultado_.Error);
    }
}

    public void botao_login_TT(){
        
    }
    IEnumerator fazerLogin(string login_, string senha_){

        //Debug.Log("aaaa");
        

        WWWForm form = new WWWForm();
        form.AddField("nickPost", login_);
        form.AddField("senhaPost", senha_);

        link_log = UnityWebRequest.Post(gerenciador.host + site_log,form);

        avisoCarregando.SetActive(true);

        barra_carregamento.value = Mathf.Lerp(barra_carregamento.value, (link_log.uploadProgress + link_log.downloadProgress) / 2,Time.deltaTime * 4);

        yield return link_log.SendWebRequest();

        avisoCarregando.SetActive(false);

        if(link_log.isNetworkError || link_log.isHttpError){

            Debug.Log("erro na rede (" + link_log.error + ")");

            avisoCarregando.SetActive(false);

            menu_ERRO.SetActive(true);

            menu_ERRO.transform.Find("desc").gameObject.GetComponent<TextMeshProUGUI>().text = "erro na rede (" + link_log.error + ")";
            
        }else
        {

            avisoCarregando.SetActive(false);
            resposta = link_log.downloadHandler.text.Split(',');

            if(resposta[0]=="0"){
                // usuario nao encontrado

                Debug.Log("usuario nao encontrado");
                
                log_nick.text = "";
                log_senha.text = "";
            }

            if(resposta[0]=="1"){
                // login bem sucedido
                
                if(resposta[1] == "1"){
                    //dados existentes

                    gerDados.instancia.dados_.id = long.Parse(resposta[2]);
                    gerDados.instancia.dados_.ult_ctt = "2001-10-28 22:10:04";

                    PlayerPrefs.SetInt("logado", 1);

                    gerDados.instancia.baixarDados();

                    Debug.Log("login bem sucedido, dados existentes");
                    menu_logreg.SetActive(false);
                    gerDados.instancia.botao_logout.SetActive(true);
                }

                if(resposta[1] == "2"){
                    //dados inexistentes
                    
                    gerDados.instancia.dados_.id = long.Parse(resposta[2]);
                    gerDados.instancia.dados_.nick = resposta[3];

                    PlayerPrefs.SetInt("logado", 1);

                    gerDados.instancia.salvar();

                    Debug.Log("login bem sucedido, dados inexistentes");
                    menu_logreg.SetActive(false);
                    gerDados.instancia.botao_logout.SetActive(true);
                }
            }

            

            if(resposta[0]=="2"){
                // senha incorreta

                Debug.Log("senha incorreta");
                
                log_senha.text = "";
            }

            if(resposta[0]=="3"){
                // conta nao confirmada
                menu_conf.SetActive(true);

                menu_conf.GetComponent<confirmar>().id = resposta[1];
            }
        }
    }

        IEnumerator fazerLoginFB(string fb_id_, string email_){
        

        WWWForm form = new WWWForm();
        form.AddField("fb_id", fb_id_);
        form.AddField("email", email_);

        link_log = UnityWebRequest.Post(gerenciador.host + site_log_fb,form);

        avisoCarregando.SetActive(true);

        barra_carregamento.value = Mathf.Lerp(barra_carregamento.value, (link_log.uploadProgress + link_log.downloadProgress) / 2,Time.deltaTime * 4);

        yield return link_log.SendWebRequest();

        avisoCarregando.SetActive(false);

        if(link_log.isNetworkError || link_log.isHttpError){

            Debug.Log("erro na rede (" + link_log.error + ")");

            avisoCarregando.SetActive(false);

            menu_ERRO.SetActive(true);

            menu_ERRO.transform.Find("desc").gameObject.GetComponent<TextMeshProUGUI>().text = "erro na rede (" + link_log.error + ")";
            
        }else
        {

            avisoCarregando.SetActive(false);
            resposta = link_log.downloadHandler.text.Split(',');

            Debug.Log("RESPOSTA: " + link_log.downloadHandler.text);

            if(resposta[0]=="0"){
                // usuario nao encontrado

                Debug.Log("usuario nao encontrado");
                
                log_nick.text = "";
                log_senha.text = "";
            }

            if(resposta[0]=="1"){
                // login bem sucedido
                
                if(resposta[1] == "1"){
                    //dados existentes

                    gerDados.instancia.dados_.id = long.Parse(resposta[2]);
                    gerDados.instancia.dados_.ult_ctt = "2001-10-28 22:10:04";

                    PlayerPrefs.SetInt("logado", 1);

                    gerDados.instancia.baixarDados();

                    Debug.Log("login bem sucedido, dados existentes");
                    menu_logreg.SetActive(false);
                    gerDados.instancia.botao_logout.SetActive(true);
                }

                if(resposta[1] == "2"){
                    //dados inexistentes
                    
                    gerDados.instancia.dados_.id = long.Parse(resposta[2]);
                    gerDados.instancia.dados_.nick = resposta[3];

                    PlayerPrefs.SetInt("logado", 1);

                    gerDados.instancia.salvar();

                    Debug.Log("login bem sucedido, dados inexistentes");
                    menu_logreg.SetActive(false);
                    gerDados.instancia.botao_logout.SetActive(true);
                }
            }

            

            if(resposta[0]=="2"){
                // facebook nao cadastrado

                Debug.Log("facebook nao cadastrado");

            }
        }
    }
}
