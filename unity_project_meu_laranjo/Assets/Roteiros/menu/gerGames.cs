using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gerGames : MonoBehaviour
{
    public static gerGames instancia;
    public GameObject menu_minigame;
    public TextMeshProUGUI nome, descricao;
    public GameObject[] fundo;
    public Color[] paleta;
    public GameObject rank_peca, rank_piv;
    public List<mini_game> games;
    // Start is called before the first frame update

    void Awake(){
        instancia = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirGame(int id_game){
        menu_minigame.SetActive(true);

        nome.text = gameDeId(id_game).nome[gerDados.instancia.dados_.lingua];

        descricao.text = gameDeId(id_game).desc[gerDados.instancia.dados_.lingua];



        fundo[0].GetComponent<Image>().color = paleta[id_game * 2 - 2];
        fundo[1].GetComponent<Image>().color = paleta[id_game * 2 - 2];
        fundo[2].GetComponent<Image>().color = paleta[id_game * 2 - 1];



        // --- limpar rank
        for ( int i= rank_piv.transform.childCount-1; i>=0; --i )
        {
            GameObject child = rank_piv.transform.GetChild(i).gameObject;
            Destroy(child );
        }



        GameObject rank_ = Instantiate(rank_peca,rank_piv.transform);

        rank_.GetComponent<peca_rank>().SetarDados(true,"cristovin",1,gerDados.instancia.dados_.recordeDeId(id_game).pontuacao,gameDeId(id_game).tipoDePont);
    }

    public mini_game gameDeId(int id_){
        mini_game game_ = null;

        foreach(mini_game gm_ in games){
            if(gm_.id == id_){
                game_ = gm_;
            }
        }

        return game_;
    }
}
