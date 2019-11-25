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
    public string site_log, site_log_fb, site_reg_FB, site_ver, nick, senha, st1, st2;
    public string[] resposta; // = new List<string>(); 
    public string[] dados_FB = new string[5];
    public bool carregando = false;
    public Slider barra_carregamento;
    public GameObject avisoCarregando, menu_conf, menu_logreg, menu_ERRO;

    public TMP_InputField texto_reg_nick_FB;
    public TMP_InputField texto_reg_nick_TT;
    public Button botao_enviar_nick_FB, botao_enviar_nick_TT;
    public Image[] bordas;
    public bool[] campoOk = new bool[2]{false, false};

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
        if(campoOk[0]){
            botao_enviar_nick_FB.interactable = true;
        }else
        {
            botao_enviar_nick_FB.interactable = false;
        }

        if(campoOk[1]){
            botao_enviar_nick_TT.interactable = true;
        }else
        {
            botao_enviar_nick_TT.interactable = false;
        }
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

    public void checar_campo_nick_FB(){
        StopCoroutine(checarCampo("",0));
        if(texto_reg_nick_FB.text.Length >= 3){
            StartCoroutine(checarCampo(texto_reg_nick_FB.text,1));
        }else
        {
            campoOk[0] = false;
            bordas[0].color = Color.red;
        }
        
    }

    public void checar_campo_nick_TT(){
        StopCoroutine(checarCampo("",0));
        if(texto_reg_nick_FB.text.Length >= 3){
            StartCoroutine(checarCampo(texto_reg_nick_FB.text,1));
        }else
        {
            campoOk[0] = false;
            bordas[0].color = Color.red;
        }
        
    }

    IEnumerator checarCampo(string campo_, int verif_){

        UnityWebRequest link = new UnityWebRequest();
        
        //Debug.Log("checando " + verif_);

        WWWForm form = new WWWForm();
        form.AddField("campo", campo_);
        form.AddField("verif", 2);

        link = UnityWebRequest.Post(gerenciador.host + site_ver,form);

        //carregando = true;

        if(verif_ == 1){
            bordas[0].color = new Color(1,0.5f,0);
            campoOk[0] = false;
        }

        if(verif_ == 2){
            bordas[1].color = new Color(1,0.5f,0);
            campoOk[1] = false;
        }

        yield return link.SendWebRequest();

        //carregando = false;

        if(link.isNetworkError || link.isHttpError){
            if(verif_ == 1){
                bordas[0].color = Color.red;
            }

            if(verif_ == 2){
                bordas[1].color = Color.red;
            }

            menu_ERRO.SetActive(true);
        }else
        {
            resposta = link.downloadHandler.text.Split(',');

            if(verif_ == 1){

                if(resposta[0] == "0"){
                    bordas[0].color = Color.red;
                    campoOk[0] = false;
                }

                if(resposta[0] == "1"){
                    bordas[0].color = Color.green;
                    campoOk[0] = true;
                }

                if(resposta[0] == "2"){
                    bordas[0].color = Color.red;
                    campoOk[0] = false;
                }

                if(resposta[0] == "99"){
                    bordas[0].color = Color.red;
                    campoOk[0] = false;
                }
                
            }

            if(verif_ == 2){

                if(resposta[0] == "1"){
                    bordas[1].color = Color.green;
                    campoOk[1] = true;
                }

                if(resposta[0] == "2"){
                    bordas[1].color = Color.red;
                    campoOk[1] = false;
                }

                if(resposta[0] == "99"){
                    bordas[1].color = Color.red;
                    campoOk[1] = false;
                }
            }
        }

    }

    public void botao_registrar_por_nick(int i_){

        string lingua_ = "";

        if(gerDados.instancia.dados_.lingua == 0){
            lingua_ = "pt-br";
        }

        if(gerDados.instancia.dados_.lingua == 1){
            lingua_ = "pt-pt";
        }

        if(gerDados.instancia.dados_.lingua == 2){
            lingua_ = "en-us";
        }

        if(gerDados.instancia.dados_.lingua == 3){
            lingua_ = "en-uk";
        }

        if(gerDados.instancia.dados_.lingua == 4){
            lingua_ = "es-mx";
        }
        if(i_ == 0){
            StartCoroutine(fazerRegistro(i_,i_ == 0 ? texto_reg_nick_FB.text : texto_reg_nick_TT.text,dados_FB[2],dados_FB[3],dados_FB[1],"0",lingua_,dados_FB[4],dados_FB[0]));
        }else
        {
            StartCoroutine(fazerRegistro(i_,i_ == 0 ? texto_reg_nick_FB.text : texto_reg_nick_TT.text,dados_FB[2],dados_FB[3],dados_FB[1],"0",lingua_,dados_FB[4],dados_FB[0]));
        }
    }

    IEnumerator fazerRegistro(int opc_ ,string nick_, string nome_, string sobrenome_, string email_, string senha_, string lingua_, string nascimento_, string id_){

        nascimento_ = nascimento_.Split('/')[2] + "-" + nascimento_.Split('/')[0] + "-" + nascimento_.Split('/')[1];

        UnityWebRequest link = new UnityWebRequest();
        
        WWWForm form = new WWWForm();

        form.AddField("nick", nick_);
        form.AddField("nome", nome_);
        form.AddField("sobrenome", sobrenome_);
        form.AddField("email", email_);
        form.AddField("senha", senha_);
        form.AddField("lingua", lingua_);
        form.AddField("nascimento", nascimento_);

        if(opc_ == 0){
            form.AddField("fb_id", id_);

            link = UnityWebRequest.Post(gerenciador.host + site_reg_FB,form);

        }else
        {
            link = UnityWebRequest.Post(gerenciador.host + site_reg_FB,form);
        }
        
        if(opc_ == 0){
            botao_enviar_nick_FB.interactable = false;
        }else
        {
            botao_enviar_nick_TT.interactable = false;
        }

        yield return link.SendWebRequest();

        if(link.isNetworkError || link.isHttpError){

            if(opc_ == 0){
                botao_enviar_nick_FB.interactable = true;
            }else
            {
                botao_enviar_nick_TT.interactable = true;
            }

            menu_ERRO.SetActive(true);

        }else
        {
            resposta = link.downloadHandler.text.Split(',');

            if(resposta[1] == "1"){
                menu_conf.SetActive(true);

                if(opc_ == 0){
                    texto_reg_nick_FB.text = "";
                }else
                {
                    texto_reg_nick_TT.text = "";
                }
                
                menu_conf.GetComponent<confirmar>().id = resposta[2];
            }
            if(resposta[1] == "2"){

                menu_ERRO.SetActive(true);

                string[] txt_ = new string[5]{"falha ao enviar email :(", "falha ao enviar email :(", "email sending fairule :(", "email sending fairule :(", "falha ao enviar email :("};

                menu_ERRO.transform.Find("desc").gameObject.GetComponent<TextMeshProUGUI>().text = txt_[gerDados.instancia.dados_.lingua];

                if(opc_ == 0){
                    botao_enviar_nick_FB.interactable = true;
                }else
                {
                    botao_enviar_nick_TT.interactable = true;
                }

            }
        }

        

    }
}
