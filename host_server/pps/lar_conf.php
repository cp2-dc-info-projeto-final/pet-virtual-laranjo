<?php

include 'lar_dads.php';

$id = 0;
$codigo = 0;

$quant = 0;
$code = 0;

if (isset($_GET['id'])){
    $id = $_GET['id'];
}

if (isset($_POST['id'])){
    $id = $_POST['id'];
}


if (isset($_GET['codigo'])){
    $codigo = $_GET['codigo'];
}

if (isset($_POST['codigo'])){
    $codigo = $_POST['codigo'];
}


	//conexao com o db
	$conexao = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);



if(!$conexao){
    // sem conecao com o banco
    echo "99,0";
    echo ",ERRO NA CONEXAO MySQL: ". mysqli_connect_error();
    
}else {
    $sql = "SELECT confirm FROM usuario WHERE id = '".$id."'";
	    
    $result = mysqli_query($conexao ,$sql);

    if(mysqli_num_rows($result) > 0){
        while($row = mysqli_fetch_assoc($result)){

            $quant = substr($row['confirm'],0,1);
            $code = substr($row['confirm'],1,6);
            
            if($code == $codigo){

                $sql2 = "UPDATE `usuario` SET `confirm` = 's' WHERE `usuario`.`id` = ".$id;

                $result2 = mysqli_query($conexao ,$sql2);

                echo "1,0";

            }else{

                $quant--;

                if($quant > 0){
                
                $sql2 = "UPDATE `usuario` SET `confirm` = '".$quant.$code."' WHERE `usuario`.`id` = ".$id;

                $result2 = mysqli_query($conexao ,$sql2);

                }else{

                    $sql2 = "DELETE FROM `usuario` WHERE `usuario`.`id` = ".$id;

                    $result2 = mysqli_query($conexao ,$sql2);

                }


                echo "0,$quant";
    
                //$result = mysqli_query($conexao ,$sql);
            }

            //echo "tentativas restantes: ".$quant.". codigo: ".$code.".";
        }
    }
}

?>