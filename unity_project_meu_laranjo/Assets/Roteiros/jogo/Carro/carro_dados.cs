public class carro_dados
{
    public int id_chassi = 2;
    public int[] nivel = new int[]{1,1,1,1}, cor_id, acessorios = new int[]{3,2,3,2,1};

    public carro_dados(){

    }
    public carro_dados (int id_chassi_, int carroceria_, int ar_teto_, int ar_capo_, int aerofolio_, int pneu_){
        id_chassi = id_chassi_;
        acessorios = new int[]{carroceria_,ar_teto_,ar_capo_,aerofolio_,pneu_};
    }
}
