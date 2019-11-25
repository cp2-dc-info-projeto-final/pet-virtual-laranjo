<?php

    include 'lar_dads.php';

    $nome = 0;
    $sobrenome = 0;
    $nick = 0;
    $email = 0;
    $nascimento = 0;
    $lingua = 0;
    $fb_id = 0;

    

	if (isset($_GET['nome'])){
	    $nome = $_GET['nome'];
	}
	
	if (isset($_POST['nome'])){
        $nome = $_POST['nome'];
    }

    

    if (isset($_GET['sobrenome'])){
	    $sobrenome = $_GET['sobrenome'];
	}
	
	if (isset($_POST['sobrenome'])){
        $sobrenome = $_POST['sobrenome'];
    }
    


	if (isset($_GET['nick'])){
	    $nick = $_GET['nick'];
	}
	
	if (isset($_POST['nick'])){
        $nick = $_POST['nick'];
    }
    


    if (isset($_GET['email'])){
	    $email = $_GET['email'];
	}
	
	if (isset($_POST['email'])){
        $email = $_POST['email'];
	}
    
    

    if (isset($_GET['nascimento'])){
	    $nascimento = $_GET['nascimento'];
	}
	
	if (isset($_POST['nascimento'])){
        $nascimento = $_POST['nascimento'];
    }
    


	if (isset($_GET['lingua'])){
	    $lingua = $_GET['lingua'];
	}
	
	if (isset($_POST['lingua'])){
        $lingua = $_POST['lingua'];
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


    
	if(!$conexao && !$conexao2){
		// sem conecao com o banco
		echo "99,0";
        echo ",ERRO NA CONEXAO MySQL: ". mysqli_connect_error();
        
	}else{
        //inserir dados


        $sql = "INSERT INTO `usuario` (`id`, `confirm`, `nome`, `sobrenome`, `nick`, `email`, `senha`, `criacao`, `nascimento`, `lingua`, `id_gg`, `id_fb`, `id_tt`, `id_nc`) VALUES (NULL, 's', '".$nome."', '".$sobrenome."', '".$nick."', '".$email."', '". rand(10000000, 99999999) ."', CURRENT_TIME(), '".$nascimento." 00:00:00', '".$lingua."', 0, ".$fb_id.", 0, 0)";
                
        //$sql = "INSERT INTO `usuario` (`id`, `confirm`, `nome`, `sobrenome`, `nick`, `email`, `senha`, `criacao`, `nascimento`, `lingua`, `id_gg`, `id_fb`, `id_tt`, `id_nc`) VALUES (NULL, 'aaa', 'crisvaldo', 'vandirlei', 'jooj', 'slaaaa@aaaaaa.coum', '123123', current_timestamp(), '2019-10-22 00:00:00', 'pt-pt', '', '', '', '')";

        $result = mysqli_query($conexao ,$sql);


        $sql = "SELECT id FROM usuario WHERE id_fb =".$fb_id;

        $result = mysqli_query($conexao ,$sql);

        while($row = mysqli_fetch_assoc($result)){
            echo "1,0,";
            echo $row['id'];
        }

        


    }

    


?>