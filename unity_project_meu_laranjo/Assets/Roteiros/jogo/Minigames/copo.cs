using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copo : MonoBehaviour
{
    public bool finalizado = true, transitando = false;
    public int quant_transicoes = 0, id_;
    public List<transicao> transicoes = new List<transicao>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        quant_transicoes = transicoes.Count;

        if(transicoes.Count > 0){
            finalizado = false;
            if(transitando == false){
                id_ = LeanTween.moveLocalZ(gameObject, transicoes[0].posicao * 3, transicoes[0].duracao).setEaseOutCubic().id;

                LeanTween.resume(id_);

                transitando = true;
            }else
            {
                if(LeanTween.isTweening(id_)){
                    //transitando = true;
                }else
                {
                    transicoes.RemoveAt(0);
                    transitando = false;
                }
            }
        }else{
            finalizado = true;
        }
    }

    public void mudarPosicao(float pos_, float tempo_){

        transicoes.Add(new transicao(pos_,tempo_));
    }

    public void mostrar(){
        LeanTween.moveLocalY(transform.GetChild(0).gameObject, 4, 0.5f).setEaseOutCubic();
    }

    public void esconder(){
        LeanTween.moveLocalY(transform.GetChild(0).gameObject, 0, 0.5f).setEaseOutCubic();
    }

    public void mostrar_esconder(){
        int id_ = LeanTween.moveLocalY(transform.GetChild(0).gameObject, 4, 0.5f).setEaseOutCubic().id;
    }
    
}

public class transicao {
    public float posicao;
    public float duracao;

    public transicao(float pos_, float dur_){
        posicao = pos_;
        duracao = dur_;
    }
}