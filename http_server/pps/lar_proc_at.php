<?php
    
    include 'lar_dads.php';
    
    $conexao1 = new mysqli($nome_server_db,$nome_usuario_db2,$senha_db,$nome_db2);

    $sql1 = "SELECT ultima_versao FROM info";


    $result1 = mysqli_query($conexao1 ,$sql1);

    while($row = mysqli_fetch_assoc($result1)){
            echo $row['ultima_versao'].",0";
    }


?>