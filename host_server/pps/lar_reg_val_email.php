<?php

    include 'lar_dads.php';
    
    $campo = 0;
    $verif = 0;


    
	if (isset($_GET['campo'])){
	    $campo = $_GET['campo'];
    }

    if (isset($_GET['verif'])){
	    $verif = $_GET['verif'];
    }


    if (isset($_POST['campo'])){
        $campo = $_POST['campo'];
	}
	
    if (isset($_POST['verif'])){
        $verif = $_POST['verif'];
    }
    

    if( filter_var($campo, FILTER_VALIDATE_EMAIL) == true || $verif == 2){

        //conexao com o db
        $conexao = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);

        
        if(!$conexao){
            echo "99,ERRO NA CONEXAO MySQL: ". mysqli_connect_error();
        }else {

            if($verif == 1){
                $sql = "SELECT email FROM usuario WHERE LOWER(email) = LOWER('" . $campo . "')";
            }
            
            if($verif == 2){
                $sql = "SELECT nick FROM usuario WHERE LOWER(nick) = LOWER('" . $campo . "')";
            }
            
            
            $result = mysqli_query($conexao ,$sql);

            if(mysqli_num_rows($result) > 0){
                while($row = mysqli_fetch_assoc($result)){

                    // registrado/indisponivel
                    echo "2,0";
                    
                }
            }else{

                // valido e dispovivel
                echo "1,0";

            }
        }

        
	}else{
        // invalido
        echo "0,0";
    }

?>