﻿using System.Collections.Generic;

public class dados
{
    public long id = 0;

    public string ult_ctt = "";

    public int lingua = 0;
    public int moedas = 0;
    public int  dolares = 0;
    public int[] itens = new int[32];
    public int[] outfit = new int[32];

    public int id_casa = 1;

    public int quant_gar = 1;
    public carro_dados[] carro = new carro_dados[]{null,new carro_dados(1,0,0,0,0,1,3,19),null,null};

    public List<recorde> recordes;

}
