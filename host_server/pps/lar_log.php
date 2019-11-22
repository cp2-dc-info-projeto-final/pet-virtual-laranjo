<?php

	//include_once 'dbconnect.php';
	
	//include("lar_ati.php");
    
    include 'lar_dads.php';

	$login = 0;
	$senha = 0;

	if (isset($_GET['nickPost'])){
	    $login = $_GET['nickPost'];
	}
	
	if (isset($_POST['nickPost'])){
        $login = $_POST['nickPost'];
	}
	

	if (isset($_GET['senhaPost'])){
    	$senha = $_GET['senhaPost'];
	}
	
	if (isset($_POST['senhaPost'])){
        $senha = $_POST['senhaPost']; 
	}
	
	//conexao com o db
	$conexao = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);
	$conexao2 = new mysqli($nome_server_db,$nome_usuario_db2,$senha_db,$nome_db2);


    
	if(!$conexao && !$conexao2){
		// sem conecao com o banco
		echo "99,0";
        echo ",ERRO NA CONEXAO MySQL: ". mysqli_connect_error();
        
	}else {
		
	    $sql = "SELECT senha FROM usuario WHERE LOWER(nick) = LOWER('".$login."') OR LOWER(email) = LOWER('".$login."')";
	    
        $result = mysqli_query($conexao ,$sql);
	}
	
	if(mysqli_num_rows($result) > 0){
		//show data for each row
		while($row = mysqli_fetch_assoc($result)){
			if($row['senha'] == $senha){
			    //senha correta :D
			    
			    $sql = "SELECT id, nick, confirm, senha FROM usuario WHERE LOWER(nick) = LOWER('".$login."') OR LOWER(email) = LOWER('".$login."')";
	    
                $result = mysqli_query($conexao ,$sql);
			    
				while($row = mysqli_fetch_assoc($result)){

					if($row["confirm"] == "s"){

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
					}else{
						echo "3,".$row["id"];
					}

				}
				
				
			    
				
			}else{
				echo "2,0";
			    //echo "senha incorreta :c";
			}
		}
	}else{
		echo "0,0";
	    //echo "usuario nao encontrado :/";
    }
    
?>