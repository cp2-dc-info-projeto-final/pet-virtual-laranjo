<?php

    //include_once 'dbconnect.php';
    
    
	$nome_server = "localhost";
	$nome_usuario = "id9454732_joveem";
	$senha = "31813181";
	$nome_db = "id9454732_jovdev";
	
	$coneccao = new mysqli($nome_server,$nome_usuario,$senha,$nome_db);

	if(!$coneccao){
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}else {
		
	    $sql = "SELECT id, nome, itens, outfit FROM teste";
	    
        $result = mysqli_query($coneccao ,$sql);
	}
	
	if(mysqli_num_rows($result) > 0){
		//show data for each row
		while($row = mysqli_fetch_assoc($result)){
			echo $row['id'] . ",".$row['nome']. ",".$row['itens']. ",".$row['outfit'] . "<br>";
		}
	}
?>