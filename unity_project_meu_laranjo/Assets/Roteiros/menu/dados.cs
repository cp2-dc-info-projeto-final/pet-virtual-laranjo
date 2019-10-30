﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dados
{
    public long id = 0;
    public string nick = "laranjo";
    public float nivel = 0;

    public string ult_ctt = "2000-05-29 23:59:59";

    public int lingua = 0;
    public long moedas = 0;
    public long  dolares = 0;
    public long[] itens = new long[4];
    public long[] outfit = new long[4];

    public int id_casa = 1;

    public int quant_gar = 1;
    public carro_dados[] carro = new carro_dados[]{null,new carro_dados(1,0,0,0,0,1,3,19),null,null};

    public List<recorde> recordes = new List<recorde>();

    public dados(){

    }
    public dados(long id_, string nick_, float nivel_, int lingua_, long moedas_, long dolares_, int id_casa_, int quant_gar_, string ult_ctt_){
        id = id_;
        nick = nick_;
        nivel = nivel_;
        ult_ctt = ult_ctt_;
        lingua = lingua_;
        moedas = moedas_;
        dolares = dolares_;
        id_casa = id_casa_;
        quant_gar = quant_gar_;
    }

    public dados(long id_, string nick_, float nivel_, int lingua_, long moedas_, long dolares_, int id_casa_, int quant_gar_, string ult_ctt_, long[] itens_, long[] outfit_){
        id = id_;
        nick = nick_;
        nivel = nivel_;
        ult_ctt = ult_ctt_;
        lingua = lingua_;
        moedas = moedas_;
        dolares = dolares_;
        itens = itens_;
        outfit = outfit_;
        id_casa = id_casa_;
        quant_gar = quant_gar_;
    }




    public void setarDados(string nick_, float nivel_, string ult_ctt_, int lingua_, int moedas_, int dolares_, long[] itens_, long[] outfit_, int id_casa_, int quant_gar_){
        nick = nick_;
        nivel = nivel_;
        ult_ctt = ult_ctt_;
        lingua = lingua_;
        moedas = moedas_;
        dolares = dolares_;
        itens = itens_;
        outfit = outfit_;
        id_casa = id_casa_;
        quant_gar = quant_gar_;
    }

    public recorde recordeDeId(int id_){
        recorde recorde_ = null;

        Debug.Log("COM RECORDDDDDDDDDDDDDDD");

        foreach(recorde rcd_ in recordes){
            if(rcd_.id_minigame == id_){
                recorde_ = rcd_;
            }
        }

        if(recorde_ == null){
            recordes.Add(new recorde(id_));
            Debug.Log("SEM RECORD");

            gerDados.instancia.salvar();
        }
        Debug.Log("COM RECORD");
        

        foreach(recorde rcd_ in recordes){
            if(rcd_.id_minigame == id_){
                recorde_ = rcd_;
            }
        }

        return recorde_;
    }
}
