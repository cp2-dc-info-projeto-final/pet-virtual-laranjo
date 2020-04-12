<?php

	//include_once 'dbconnect.php';
	
	//include("lar_ati.php");
    
    include 'lar_dads.php';

	$email = 0;
	$fb_id = 0;

	if (isset($_GET['email'])){
	    $email = $_GET['email'];
	}
	
	if (isset($_POST['email'])){
        $email = $_POST['email'];
    }
    

    if (isset($_GET['fb_id'])){
	    $fb_id = $_GET['fb_id'];
	}
	
	if (isset($_POST['fb_id'])){
        $fb_id = $_POST['fb_id'];
	}
	
	//conexao com o db
	$conexao = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);
	$conexao2 = new mysqli($nome_server_db,$nome_usuario_db2,$senha_db,$nome_db2);


    
	if(!$conexao || !$conexao2){
		// sem conecao com o banco
		echo "99,0";
        echo ",ERRO NA CONEXAO MySQL: ". mysqli_connect_error();
        
	}else {
		
	    $sql = "SELECT id FROM usuario WHERE id_fb = ".$fb_id;
	    
        $result = mysqli_query($conexao ,$sql);
    }
    
    if(mysqli_num_rows($result) > 0){
        // facebook anexado a uma conta
        
        $sql = "SELECT id, nick, senha FROM usuario WHERE id_fb = ".$fb_id;

        $result = mysqli_query($conexao ,$sql);
        
        while($row = mysqli_fetch_assoc($result)){

            $id_usuario = $row["id"];

            $nick_usuario = $row["nick"];

            $sql = "SELECT id FROM info_laranjo	 WHERE id = ".$id_usuario;

            $result2 = mysqli_query($conexao2 ,$sql);


            if(mysqli_num_rows($result2) > 0){

                //login bem sucedido e com dados registrados
                echo "1,1,".$id_usuario;
            }else{

                //login bem sucedido mas sem dados salvos
                echo "1,2,".$id_usuario.","."$nick_usuario";



                //$sql = "INSERT INTO `info_laranjo` (`id`, `nick_laranjo`, `moedas`, `dolares`, `nivel`, `id_casa`, `quant_gar`, `relacionamento`, `primeiro_cont`, `ultimo_cont`) VALUES ('111', 'nick', '9999', '20', '0.4', '5', '3', 'p', current_timestamp(), current_timestamp())";
                //$result3 = mysqli_query($conexao2 ,$sql);
            }

        }
        
        
        
        
    

    }else{
        //facebook nao registrado, verificar se o email esta registrado

        $sql = "SELECT id_fb FROM usuario WHERE email = '".$email."'";
	    
        $result4 = mysqli_query($conexao ,$sql);

        if(mysqli_num_rows($result4) > 0){
            // email registrado, anexar facebook ao usuario

            while($row = mysqli_fetch_assoc($result4)){

                if($row["id_fb"] == 0){
                    $sql = "UPDATE usuario SET id_fb = ".$fb_id." WHERE email = '".$email."'";
	    
                    $result3 = mysqli_query($conexao ,$sql);
                }
            }
        
            $sql = "SELECT id, nick, senha FROM usuario WHERE email = '".$email."'";

            $result = mysqli_query($conexao ,$sql);
            
            while($row = mysqli_fetch_assoc($result)){

                $id_usuario = $row["id"];

                $nick_usuario = $row["nick"];

                $sql = "SELECT id FROM info_laranjo	 WHERE id = ".$id_usuario;

                $result2 = mysqli_query($conexao2 ,$sql);


                if(mysqli_num_rows($result2) > 0){

                    //login bem sucedido e com dados registrados
                    echo "1,1,".$id_usuario;
                }else{

                    //login bem sucedido mas sem dados salvos
                    echo "1,2,".$id_usuario.","."$nick_usuario";



                    //$sql = "INSERT INTO `info_laranjo` (`id`, `nick_laranjo`, `moedas`, `dolares`, `nivel`, `id_casa`, `quant_gar`, `relacionamento`, `primeiro_cont`, `ultimo_cont`) VALUES ('111', 'nick', '9999', '20', '0.4', '5', '3', 'p', current_timestamp(), current_timestamp())";
                    //$result3 = mysqli_query($conexao2 ,$sql);
                }

            }

        }else{
            //fazer registro com os dados do facebook apenas

            echo "2,0";

        }
    }

    
?>