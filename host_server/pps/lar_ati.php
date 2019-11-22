<?php

    //include_once 'dbconnect.php';
    
    
	include 'lar_dads.php';

    $acao = 0;

    $id = 0;
    $nick = 0;
    $moedas = 0;
    $dolares = 0;
    $nivel = 0;
    $id_casa = 0;
    $quant_gar = 0;
    $ult_ctt = 0;

    $itensArray = 0;
    $itens = 0;
    $outfitArray = 0;
    $outfit = 0;
    $moveis = 0;
    $records = 0;


    if (isset($_GET['id'])){
	    $id = $_GET['id'];
	}
	
	if (isset($_POST['id'])){
        $id = $_POST['id'];
    }
    

    if (isset($_GET['nick'])){
	    $nick = $_GET['nick'];
	}
	
	if (isset($_POST['nick'])){
        $nick = $_POST['nick'];
    }
    

    if (isset($_GET['moedas'])){
	    $moedas = $_GET['moedas'];
	}
	
	if (isset($_POST['moedas'])){
        $moedas = $_POST['moedas'];
    }
    

    if (isset($_GET['dolares'])){
	    $dolares = $_GET['dolares'];
	}
	
	if (isset($_POST['dolares'])){
        $dolares = $_POST['dolares'];
    }
    

    if (isset($_GET['nivel'])){
	    $nivel = $_GET['nivel'];
	}
	
	if (isset($_POST['nivel'])){
        $nivel = $_POST['nivel'];
    }
    

    if (isset($_GET['id_casa'])){
	    $id_casa = $_GET['id_casa'];
	}
	
	if (isset($_POST['id_casa'])){
        $id_casa = $_POST['id_casa'];
    }
    

    if (isset($_GET['quant_gar'])){
	    $quant_gar = $_GET['quant_gar'];
	}
	
	if (isset($_POST['quant_gar'])){
        $quant_gar = $_POST['quant_gar'];
    }
    

    if (isset($_GET['ult_ctt'])){
	    $ult_ctt = $_GET['ult_ctt'];
	}
	
	if (isset($_POST['ult_ctt'])){
        $ult_ctt = $_POST['ult_ctt'];
    }

    

    if (isset($_GET['itens'])){
	    $itensArray = $_GET['itens'];
	}
	
	if (isset($_POST['itens'])){
        $itensArray = $_POST['itens'];
    }

    $itens = explode("-",$itensArray);



    if (isset($_GET['outfit'])){
	    $outfitArray = $_GET['outfit'];
	}
	
	if (isset($_POST['outfit'])){
        $outfitArray = $_POST['outfit'];
    }

    $outfit = explode("-",$outfitArray);



    if (isset($_GET['moveis'])){
	    $moveisArray = $_GET['moveis'];
	}
	
	if (isset($_POST['moveis'])){
        $moveisArray = $_POST['moveis'];
    }

    $moveis = explode("-",$moveisArray);




    if (isset($_GET['records'])){
	    $recordsArray = $_GET['records'];
	}
	
	if (isset($_POST['records'])){
        $recordsArray = $_POST['records'];
    }

    $records = explode("-",$recordsArray);



    if (isset($_GET['carros'])){
	    $carrosArray = $_GET['carros'];
	}
	
	if (isset($_POST['carros'])){
        $carrosArray = $_POST['carros'];
    }

    $carros = explode("-",$carrosArray);

    

    $formatoDiaHora = 'Y-m-d';

    $utc_on;
    $utc_off = new DateTime($ult_ctt);
    

    $conexao1 = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);
    $conexao2 = new mysqli($nome_server_db,$nome_usuario_db2,$senha_db,$nome_db2);

    $sql2 = "SELECT ultimo_cont FROM info_laranjo WHERE id = ".$id;
    $result2 = mysqli_query($conexao2 ,$sql2);

    if(mysqli_num_rows($result2) > 0){
        while($row = mysqli_fetch_assoc($result2)){

            $utc_on = new DateTime($row['ultimo_cont']);

			if($utc_on < $utc_off){
                //upar dados offline para atualizar online

                atualizarDados();

            }else{
                if($utc_on > $utc_off){
                    //baixar dados online para atualizar offline

                    baixarDados();
                }else{
                    //dados iguais

                    //atualizarData();
                    atualizarDados();
                }
            }
        }
    }else{
        //criar dados
        criarDados();
        
    }




    //if($acao == 1){
        //atualizarDados();
    //}

    //conexao com o db
    function criarDados(){

        global $id, $nick, $moedas, $dolares, $nivel, $id_casa, $quant_gar, $ult_ctt, $itens, $outfit, $moveis, $records, $carros, $conexao1, $conexao2;

        $sql = "INSERT INTO `info_laranjo` (`id`, `nick_laranjo`, `moedas`, `dolares`, `nivel`, `id_casa`, `quant_gar`, `relacionamento`, `primeiro_cont`, `ultimo_cont`) VALUES (".$id.", '".$nick."', ".$moedas.", ".$dolares.", ".$nivel.", ".$id_casa.", ".$quant_gar.", 'p', current_timestamp(), current_timestamp())";
        $result1 = mysqli_query($conexao2 ,$sql);



        $sql2 = "SELECT ultimo_cont FROM info_laranjo WHERE id = ".$id;
        $result2 = mysqli_query($conexao2 ,$sql2);

        for($i_ = 0; $i_ < count($itens); $i_++){
            gravarDados_InfoLaranjo_int($itens[$i_],0,$i_);
        }

        for($i_ = 0; $i_ < count($outfit); $i_++){
            gravarDados_InfoLaranjo_int($outfit[$i_],1,$i_);
        }

        for($i_ = 0; $i_ < count($moveis); $i_++){
            gravarDados_InfoLaranjo_int($moveis[$i_],2,$i_);
        }

        for($i_ = 0; $i_ < count($records); $i_++){
            gravarDados_InfoLaranjo_int($records[$i_],3,$i_);
        }

        for($i_ = 0; $i_ < count($carros); $i_++){
            gravarDados_InfoLaranjo_int($carros[$i_],4,$i_);
        }

        
        if($result1 === false) { 
            die(mysqli_error($conexao2));
         }else{

        while($row = mysqli_fetch_assoc($result2)){
            //dados criados
            echo "2,".$row['ultimo_cont'];
        }}

        //echo $sql;
    }

    function atualizarDados(){

        global $id, $nick, $moedas, $dolares, $nivel, $id_casa, $quant_gar, $ult_ctt, $itens, $outfit, $moveis, $records, $carros, $conexao1, $conexao2;

        $sql = "UPDATE `info_laranjo` SET `moedas` = '".$moedas."', `dolares` = '".$dolares."', `nivel` = '".$nivel."', `id_casa` = '".$id_casa."', `quant_gar` = '".$quant_gar."', `ultimo_cont` = current_timestamp() WHERE `info_laranjo`.`id` = ".$id;
        $result1 = mysqli_query($conexao2 ,$sql);

        $sql2 = "SELECT ultimo_cont FROM info_laranjo WHERE id = ".$id;
        $result2 = mysqli_query($conexao2 ,$sql2);


        for($i_ = 0; $i_ < count($itens); $i_++){
            gravarDados_InfoLaranjo_int($itens[$i_],0,$i_);
        }

        for($i_ = 0; $i_ < count($outfit); $i_++){
            gravarDados_InfoLaranjo_int($outfit[$i_],1,$i_);
        }

        for($i_ = 0; $i_ < count($moveis); $i_++){
            gravarDados_InfoLaranjo_int($moveis[$i_],2,$i_);
        }

        for($i_ = 0; $i_ < count($records); $i_++){
            gravarDados_InfoLaranjo_int($records[$i_],3,$i_);
        }

        for($i_ = 0; $i_ < count($carros); $i_++){
            gravarDados_InfoLaranjo_int($carros[$i_],4,$i_);
        }
        

        while($row = mysqli_fetch_assoc($result2)){
            //dados atualizados
            echo "1,".$row['ultimo_cont'];
        }
    }

    function baixarDados(){

        global $id, $nick, $moedas, $dolares, $nivel, $id_casa, $quant_gar, $ult_ctt, $itens, $outfit, $moveis, $records, $carros, $conexao1, $conexao2;

        $sql = "UPDATE `info_laranjo` SET `ultimo_cont` = current_timestamp() WHERE `info_laranjo`.`id` = ".$id;
        $result1 = mysqli_query($conexao2 ,$sql);

        $sql2 = "SELECT id, nick_laranjo, nivel, moedas, dolares, id_casa, quant_gar, ultimo_cont FROM info_laranjo WHERE id = ".$id;
        $result2 = mysqli_query($conexao2 ,$sql2);

        

        while($row = mysqli_fetch_assoc($result2)){
            //dados baixados
            echo "3,".$row['id'].",".$row['nick_laranjo'].",".$row['nivel'].",".$row['moedas'].",".$row['dolares'].",".$row['id_casa'].",".$row['quant_gar'].",".$row['ultimo_cont'].",";
        }

        //itens

        for($i_ = 0; $i_ < count($itens); $i_++){
            echo lerDados_InfoLaranjo_int(0,$i_);

            if($i_ != count($itens) - 1){
                echo "-";
            }
        }

        echo ",";

        //outfit

        for($i_ = 0; $i_ < count($outfit); $i_++){
            echo lerDados_InfoLaranjo_int(1,$i_);

            if($i_ != count($outfit) - 1){
                echo "-";
            }
        }

        echo ",";

        //moveis

        for($i_ = 0; $i_ < count($moveis); $i_++){
            echo lerDados_InfoLaranjo_int(2,$i_);

            if($i_ != count($moveis) - 1){
                echo "-";
            }
        }

        echo ",";

        //records

        for($i_ = 0; $i_ < count($records); $i_++){
            echo lerDados_InfoLaranjo_int(3,$i_);

            if($i_ != count($records) - 1){
                echo "-";
            }
        }

        echo ",";

        for($i_ = 0; $i_ < count($carros); $i_++){
            echo lerDados_InfoLaranjo_int(4,$i_);

            if($i_ != count($carros) - 1){
                echo "-";
            }
        }
        
    }

    function atualizarData()
    {
        global $id, $nick, $moedas, $dolares, $nivel, $id_casa, $quant_gar, $ult_ctt, $conexao1, $conexao2;

        $sql = "UPDATE `info_laranjo` SET `ultimo_cont` = current_timestamp() WHERE `info_laranjo`.`id` = ".$id;
        $result1 = mysqli_query($conexao2 ,$sql);

        $sql2 = "SELECT ultimo_cont FROM info_laranjo WHERE id = ".$id;
        $result2 = mysqli_query($conexao2 ,$sql2);

        while($row = mysqli_fetch_assoc($result2)){

            //dados iguais, data atualizada atualizados
            echo "4,".$row['ultimo_cont'];
            
        }
    }

    function gravarDados_InfoLaranjo_int($valor_, $acrescimo_, $indice_)
    {
        global $id, $nick, $moedas, $dolares, $nivel, $id_casa, $quant_gar, $ult_ctt, $conexao1, $conexao2;

        $sql1 = "SELECT int_info FROM dados_info_laranjo WHERE id_info_laranjo = ".$id." AND indice = ".($indice_ * 10 + $acrescimo_);
        $result1 = mysqli_query($conexao2 ,$sql1);
    
        if(mysqli_num_rows($result1) > 0){
            //atualizar dado

            while($row = mysqli_fetch_assoc($result1)){
                if($row['int_info'] != $valor_){

                    $sql2 = "UPDATE dados_info_laranjo SET int_info = ".$valor_." WHERE id_info_laranjo = ".$id." AND indice = ".($indice_ * 10 + $acrescimo_);
                    $result2 = mysqli_query($conexao2 ,$sql2);

                }
            }
        }else{
            //inserir dado

            $sql2 = "INSERT INTO dados_info_laranjo (id_info_laranjo, indice, int_info) VALUES (".$id.", '".($indice_ * 10 + $acrescimo_)."', ".$valor_.")";
            $result1 = mysqli_query($conexao2 ,$sql2);
        }

    }

    function lerDados_InfoLaranjo_int($acrescimo_, $indice_)
    {
        global $id, $nick, $moedas, $dolares, $nivel, $id_casa, $quant_gar, $ult_ctt, $conexao1, $conexao2;

        $sql1 = "SELECT int_info FROM dados_info_laranjo WHERE id_info_laranjo = ".$id." AND indice = ".($indice_ * 10 + $acrescimo_);
        $result1 = mysqli_query($conexao2 ,$sql1);

        if(mysqli_num_rows($result1) > 0){
            //retornar dado

            while($row = mysqli_fetch_assoc($result1)){
                return $row['int_info'];
            }
        }else{
            //inserir dado

            $sql2 = "INSERT INTO dados_info_laranjo (id_info_laranjo, indice, int_info) VALUES (".$id.", '".($indice_ * 10 + $acrescimo_)."', 0)";
            $result1 = mysqli_query($conexao2 ,$sql2);

            return 0;
        }
    }
	
    
    

    
    
?>