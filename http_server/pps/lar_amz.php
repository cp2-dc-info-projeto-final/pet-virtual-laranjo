<?php

    //include_once 'dbconnect.php';
    
    
	include 'lar_dads.php';

    $id = 0;
    $id_2 = 0;
    $nick = 0;

    $acao = 0;
    

    if (isset($_GET['id_proprio'])){
	    $id = $_GET['id_proprio'];
	}
	
	if (isset($_POST['id_proprio'])){
        $id = $_POST['id_proprio'];
    }

    
    if (isset($_GET['id_outro'])){
	    $id_2 = $_GET['id_outro'];
	}
	
	if (isset($_POST['id_outro'])){
        $id_2 = $_POST['id_outro'];
    }


    if (isset($_GET['nick'])){
	    $nick = $_GET['nick'];
	}
	
	if (isset($_POST['nick'])){
        $nick = $_POST['nick'];
    }


    if (isset($_GET['acao'])){
	    $acao = $_GET['acao'];
	}
	
	if (isset($_POST['acao'])){
        $acao = $_POST['acao'];
    }


    if (isset($_GET['acao_2'])){
	    $acao_2 = $_GET['acao_2'];
	}
	
	if (isset($_POST['acao_2'])){
        $acao_2 = $_POST['acao_2'];
    }


    $conexao1 = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);
    $conexao2 = new mysqli($nome_server_db,$nome_usuario_db2,$senha_db,$nome_db2);



    if($acao == 1){
        //mostrar perfil
        mostrarPerfil();
    }

    if($acao == 2){
        //pesquisa
        fazerPesquisa();
    }

    if($acao == 3){
        //modificar relacionamento
        modificarRelacionamento();
    }

    if($acao == 4){
        //mostrar amizadades confirmadas
        mostrarAmigos();
    }

    if($acao == 5){
        //mostrar amizades pendentes
        mostrarSolicitacoes();
    }


    function mostrarPerfil()
    {
        global $id, $id_2, $nick, $conexao1, $conexao2;
        
        $sql2 = "SELECT nick_laranjo, nivel, moedas, dolares, ultimo_cont FROM info_laranjo WHERE id = ".$id_2;
        $result2 = mysqli_query($conexao2 ,$sql2);

        //dados do perfil

        echo "1,";

        while($row = mysqli_fetch_assoc($result2)){
            echo $row['nick_laranjo'].",";
            echo $row['nivel'].",";
            echo $row['moedas'].",";
            echo $row['dolares'].",";
            echo $row['ultimo_cont'].",";
        }

        //outfit

        $sql2 = "SELECT int_info FROM dados_info_laranjo WHERE indice % 10 = 1 AND id_info_laranjo = ".$id_2." ORDER BY indice";
        $result2 = mysqli_query($conexao2 ,$sql2);

        $i_row_ = 1;

        while($row = mysqli_fetch_assoc($result2)){

            echo $row['int_info'];

            if($i_row_ != mysqli_num_rows($result2)){
                echo "-";
                $i_row_ ++;
            }

        }


        //relacionamento

        $sql2 = "SELECT id_usuario_1, id_usuario_2, relacao FROM amizade WHERE (id_usuario_1 = ".$id." AND id_usuario_2 = ".$id_2.") OR (id_usuario_1 = ".$id_2." AND id_usuario_2 = ".$id.")";
        $result2 = mysqli_query($conexao1 ,$sql2);

        echo ",";

        if(mysqli_num_rows($result2) > 0){


            while($row = mysqli_fetch_assoc($result2)){

                if($row['relacao'] == "a"){
                    //amigos
                    echo "a";
                }else{
                    if($row['id_usuario_2'] == $id){
                        //solicitacao recebida pendente
                        echo "r";
                    }else{
                        //solicitacao enviada pendente
                        echo "e";
                    }
                }
            }

        }else{

            //sem relacionamento
            echo "0";
        }
    }

    function fazerPesquisa()
    {
        global $nick, $conexao1, $conexao2;

        $sql2 = "SELECT id, nick_laranjo, nivel FROM info_laranjo WHERE LOWER(nick_laranjo) = LOWER('".$nick."') UNION SELECT id, nick_laranjo, nivel FROM info_laranjo WHERE LOWER(nick_laranjo) LIKE LOWER('".$nick."_%') UNION SELECT id, nick_laranjo, nivel FROM info_laranjo WHERE LOWER(nick_laranjo) LIKE LOWER('%_".$nick."%')";
        $result2 = mysqli_query($conexao2 ,$sql2);

        //mostra pesquisa
        echo "2,";

        if(mysqli_num_rows($result2) > 0){
            //retornar dado

            echo "1,";

            $i_row_ = 1;

            while($row = mysqli_fetch_assoc($result2)){
                echo $row['id'];
                echo "-";
                echo $row['nick_laranjo'];
                echo "-";
                echo $row['nivel'];

                if($i_row_ != mysqli_num_rows($result2)){
                    echo ",";
                    $i_row_ ++;
                }
            }
        }else{
            echo "0";
        }
    }

    function modificarRelacionamento(){

        global $id, $id_2, $nick, $acao_2, $conexao1, $conexao2;

        if($acao_2 == "0"){
            //desfazer solicitacao
            $sql2 = "DELETE FROM `amizade` WHERE (id_usuario_1 = ".$id." AND id_usuario_2 = ".$id_2.") OR (id_usuario_1 = ".$id_2." AND id_usuario_2 = ".$id.")";
            $result1 = mysqli_query($conexao1 ,$sql2);

            echo "1,0,0";
        }

        if($acao_2 == "1"){
            // enviar solicitacao
            $sql2 = "INSERT INTO amizade (id_usuario_1, id_usuario_2, relacao) VALUES (".$id.", '".$id_2."', 'p')";
            $result1 = mysqli_query($conexao1 ,$sql2);

            echo "1,0,1";
        }

        if($acao_2 == "2"){
            //confirmar solicitacao
            $sql2 = "UPDATE `amizade` SET `relacao` = 'a' WHERE (id_usuario_1 = ".$id." AND id_usuario_2 = ".$id_2.") OR (id_usuario_1 = ".$id_2." AND id_usuario_2 = ".$id.")";
            $result1 = mysqli_query($conexao1 ,$sql2);

            echo "1,0,2";
        }
    }


    function mostrarAmigos()
    {
        global $id, $nick, $conexao1, $conexao2;

        
        $sql2 = "SELECT usuario.nick, amizade.id_usuario_2 AS amigo FROM amizade
        JOIN usuario ON usuario.id = amizade.id_usuario_2 AND amizade.id_usuario_1 = ".$id."  AND amizade.relacao = 'a'
        
        UNION
        
        SELECT usuario.nick, amizade.id_usuario_1 AS amigo FROM amizade
        JOIN usuario ON usuario.id = amizade.id_usuario_1 AND amizade.id_usuario_2 = ".$id."  AND amizade.relacao = 'a'
        
        ORDER BY nick";

        $result2 = mysqli_query($conexao1 ,$sql2);

        //mostra amigos
        echo "4,";

        if(mysqli_num_rows($result2) > 0){

            //guardar nick de amizades

            $i_row_ = 1;

            $amigos = array();

            while($row = mysqli_fetch_assoc($result2)){

                array_push($amigos,$row['nick']);

            }

            $amigos_no_jogo = 0;
            
            foreach ($amigos as $amigo_){

                //buscar amigos no jogo
                
                $sql2 = "SELECT id, nick_laranjo, nivel FROM info_laranjo WHERE nick_laranjo = '".$amigo_."'";
                $result2 = mysqli_query($conexao2 ,$sql2);


                if(mysqli_num_rows($result2) > 0){
                    //retornar dado

                    $amigos_no_jogo ++;
        
                    while($row = mysqli_fetch_assoc($result2)){
                        echo $row['id'];
                        echo "-";
                        echo $row['nick_laranjo'];
                        echo "-";
                        echo $row['nivel'];
                        
                        echo ",";
                    }
                }

            }

            if($amigos_no_jogo == 0){
                //sem amizade :c
                echo "0";
            }

            
        }else{
            //sem amizade :c
            echo "0";
        }


    }


    function mostrarSolicitacoes()
    {
        global $id, $nick, $conexao1, $conexao2;

        
        $sql2 = "SELECT amizade.id, usuario.nick, amizade.id_usuario_2 AS amigo FROM amizade
        JOIN usuario ON usuario.id = amizade.id_usuario_2 AND amizade.id_usuario_1 = ".$id."  AND amizade.relacao = 'p'
        
        UNION
        
        SELECT amizade.id, usuario.nick, amizade.id_usuario_1 AS amigo FROM amizade
        JOIN usuario ON usuario.id = amizade.id_usuario_1 AND amizade.id_usuario_2 = ".$id."  AND amizade.relacao = 'p'
        
        ORDER BY id DESC";

        $result2 = mysqli_query($conexao1 ,$sql2);

        //mostra amizades pendente
        echo "5,";

        if(mysqli_num_rows($result2) > 0){

            //guardar nick de amizades

            $i_row_ = 1;

            $amigos = array();

            while($row = mysqli_fetch_assoc($result2)){

                array_push($amigos,$row['nick']);

            }

            $amigos_no_jogo = 0;
            
            foreach ($amigos as $amigo_){

                //buscar perfil no jogo
                
                $sql2 = "SELECT id, nick_laranjo, nivel FROM info_laranjo WHERE nick_laranjo = '".$amigo_."'";
                $result2 = mysqli_query($conexao2 ,$sql2);


                if(mysqli_num_rows($result2) > 0){
                    //retornar dado

                    $amigos_no_jogo ++;
        
                    while($row = mysqli_fetch_assoc($result2)){
                        echo $row['id'];
                        echo "-";
                        echo $row['nick_laranjo'];
                        echo "-";
                        echo $row['nivel'];
                        
                        echo ",";
                    }
                }

            }

            if($amigos_no_jogo == 0){
                //sem sem pendencias
                echo "0";
            }

            
        }else{
            //sem sem pendencias
            echo "0";
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