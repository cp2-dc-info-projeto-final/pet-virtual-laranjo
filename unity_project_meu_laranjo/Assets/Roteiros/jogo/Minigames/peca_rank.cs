using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class peca_rank : MonoBehaviour
{
    public Color[] paleta = new Color[8];
    public TextMeshProUGUI text_nome, text_posi, text_pont;
    public GameObject fundo, contorno;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetarDados(bool ehjogador_,string Nome_,int Posicao_, long Pontuacao_,TipoDePontuacao Tipo_){

        text_nome.text = Nome_;

        contorno.SetActive(ehjogador_);

        text_posi.text = "#" + Posicao_.ToString().PadLeft(2, '0');

        if(Tipo_ == TipoDePontuacao.Ponto){
            text_pont.text = Pontuacao_.ToString();
        }

        if(Tipo_ == TipoDePontuacao.Tempo){
            text_pont.text = (Pontuacao_ / 60).ToString().PadLeft(2, '0') + ":"+ (Pontuacao_ % 60).ToString().PadLeft(2, '0') + "mins";
        }

        // --- mudar cor da linha do ranking de acordo com a posicao

        if(Posicao_ <= 3){
            
            if(Posicao_ == 1){
                fundo.GetComponent<Image>().color = paleta[0];
                contorno.GetComponent<Image>().color = paleta[1];
            }

            if(Posicao_ == 2){
                fundo.GetComponent<Image>().color = paleta[2];
                contorno.GetComponent<Image>().color = paleta[3];
            }

            if(Posicao_ == 3){
                fundo.GetComponent<Image>().color = paleta[4];
                contorno.GetComponent<Image>().color = paleta[5];
            }

        }else{
            fundo.GetComponent<Image>().color = paleta[6];
            contorno.GetComponent<Image>().color = paleta[7];
        }
        
    }
}
