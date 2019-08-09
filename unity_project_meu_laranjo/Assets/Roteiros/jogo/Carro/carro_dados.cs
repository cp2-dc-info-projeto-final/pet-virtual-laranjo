public class carro_dados
{
    public int id_chassi = 2;
    public int[] nivel = new int[]{1,1,1,1}, cor_id = new int[]{16,16}, acessorios = new int[]{3,2,3,2,1};

    public carro_dados(){

    }
    public carro_dados (int id_chassi_, int carroceria_, int ar_teto_, int ar_capo_, int aerofolio_, int pneu_, int id_cor1_, int id_cor2_){
        id_chassi = id_chassi_;
        cor_id = new int[]{id_cor1_,id_cor2_};
        acessorios = new int[]{carroceria_,ar_teto_,ar_capo_,aerofolio_,pneu_};
    }
}
