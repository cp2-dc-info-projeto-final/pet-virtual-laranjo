using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class gerDados : MonoBehaviour
{
    public string site, cont_;

    public UnityWebRequest link;

    public static gerDados instancia {get; set;}
    public dados dados_;

    public int[] itemAtual = new int[10]; 

    public TextMeshProUGUI textoMoeda, textoDolar;

    gerenciador ger_jogo_;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        instancia = this;

        loginConfigs();
        
        ger_jogo_ = GameObject.Find("ger_jogo").GetComponent<gerenciador>();
        carregar();
        //Debug.Log(ferramentas.Serializar<dados>(dados_));

        for(int i = 0; i < (dados_.outfit.Length * 64);i++){
            if(temItemOutfFit(i)){
                for(int i_ = 0; i_ < ger_jogo_.itens.Count - 1; i++){
                    if(ger_jogo_.itens[i_] != null){
                        if(ger_jogo_.itens[i_].id == i){
                            ger_jogo_.laranjo.GetComponent<design>().MudarMesh(ger_jogo_.itens[i]);

                            if(ger_jogo_.itens[i_].posicao == 5){
                                ger_jogo_.laranjo.GetComponent<Animator>().SetBool("item",ger_jogo_.itens[i].seguraItem);
                            }
                            itemAtual[ger_jogo_.itens[i_].posicao] = i;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator Start() {
        link = UnityWebRequest.Get(site);


        yield return link.SendWebRequest();
        if(link.isNetworkError || link.isHttpError){
            cont_ = "ERROOOU " + link.error;
        }else
        {
            cont_ = link.downloadHandler.text;
        }


    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            dados_.moedas = 14;
            dados_.itens[0] = 1023;
            salvar();
            Debug.Log(ferramentas.Serializar<dados>(dados_));

            Debug.Log(10/3);
        }

        textoMoeda.text = dados_.moedas.ToString();
        textoDolar.text = dados_.dolares.ToString();
    }

    public void loginConfigs(){
        if(PlayerPrefs.HasKey("logado")){
            if(PlayerPrefs.GetInt("logado") == 1){
                print("Voce esta logado como '");
                print(PlayerPrefs.GetString("login" + "' "));
                for(int i = 1; i <= PlayerPrefs.GetString("senha").Length;i++){
                    print("*");
                }
                print(" c:");

                //quando estiver com login:

            }else{
                print("Voce nao esta logado :c");

                //quando estiver sem login:

            }
        }else
        {
            PlayerPrefs.SetInt("logado", 0);
            print("Primeiro uso, registrando dados...");

            //quando estiver entrando plea primeira vez:
            
        }
    }
    public void salvar(){
        PlayerPrefs.SetString("dados",ferramentas.Serializar<dados>(dados_));
    }

    public void carregar(){
        if(PlayerPrefs.HasKey("dados")){
            dados_ = ferramentas.Desserializar<dados>(PlayerPrefs.GetString("dados"));
        }else
        {
            dados_ = new dados();
            salvar();
        }
    }

    public bool temItem(int id_){
        return (dados_.itens[id_/64] &  (1 << (id_ % 64))) != 0;
    }

    public void adicionarItem(int id_){
        dados_.itens[id_/64] |= 1 << (id_ % 64);
    }

    public void removerItem(int id_){
        dados_.itens[id_/64] ^= 1 << (id_ % 64);
    }


    public bool temItemOutfFit(int id_){
        return (dados_.outfit[id_/64] &  (1 << (id_ % 64))) != 0;
    }
    public void adicionarOutFit(int id_){
        dados_.outfit[id_/64] |= 1 << (id_ % 64);
    }

    public void removerOutFit(int id_){
        dados_.outfit[id_/64] ^= 1 << (id_ % 64);
    }

    public void trocarOutFit(int id_){
        removerOutFit(itemAtual[ger_jogo_.itemDeId(id_).posicao]);
        itemAtual[ger_jogo_.itemDeId(id_).posicao] = ger_jogo_.itemDeId(id_).id;
        adicionarOutFit(id_);
    }
}
