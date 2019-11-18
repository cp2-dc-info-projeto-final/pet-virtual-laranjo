using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class carro_dados
{
    public int id_chassi = 1;
    //niveis: motor (velocidade maxima), cambio (aceleracao), freios, tracao (friccao)   acessorios: carroceria, passagem de ar do teto, passagem de ar do capo, aerofolio, pneu, vinil
    public int[] nivel = new int[4]{1,1,1,1}, cor_id = new int[2]{3,39}, acessorios = new int[6]{0,0,0,0,1,0};
    

    public carro_dados(){

    }
    public carro_dados (int id_chassi_, int carroceria_, int ar_teto_, int ar_capo_, int aerofolio_, int pneu_, int id_cor1_, int id_cor2_){
        id_chassi = id_chassi_;
        cor_id = new int[]{id_cor1_,id_cor2_};
        acessorios = new int[]{carroceria_,ar_teto_,ar_capo_,aerofolio_,pneu_};
    }

    public carro_dados(carro_dados carro_){
        id_chassi = carro_.id_chassi;
        nivel = carro_.nivel;
        cor_id = carro_.cor_id;
        acessorios = carro_.acessorios;
    }


    
}

public static class ClassExtensions
    {
        public static T CloneProfundo<T>(this T source) where T : class
        {
            using (Stream cloneStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                 
                formatter.Serialize(cloneStream, source);
                cloneStream.Position = 0;
                T clone = (T)formatter.Deserialize(cloneStream);
 
                return clone;
            }
        }
    }
