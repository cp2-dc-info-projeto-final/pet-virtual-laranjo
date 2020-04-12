<?php

include 'lar_dads.php';

//error_reporting(E_ALL);
require("PHPMailer_5.2.4/class.phpmailer.php");
$mail = new PHPMailer();
$mail->IsSMTP(); // set mailer to use SMTP
$mail->SMTPDebug  = 2;
$mail->From = $email_from;
$mail->FromName = $email_from_nome;
$mail->Host = "smtp.gmail.com"; // specif smtp server
$mail->SMTPSecure= "ssl"; // Used instead of TLS when only POP mail is selected
$mail->Port = 465; // Used instead of 587 when only POP mail is selected
$mail->SMTPAuth = true;
$mail->Username = $email_username; // SMTP username
$mail->Password = $email_senha; // SMTP password

//   $mail->AddAddress("valdenildoc@gmail.com", "cristovao"); //replace myname and mypassword to yours

//$mail->AddReplyTo("NOREPLYjovdev@gmail.com", "NOREPLYjovdev");
$mail->WordWrap = 50; // set word wrap
//$mail->AddAttachment("c:\\temp\\js-bak.sql"); // add attachments
//$mail->AddAttachment("c:/temp/11-10-00.zip");

//NAO DEBUGAR MENSAGENS
$mail->SMTPDebug = false;

$mail->IsHTML(true); // set email format to HTML

$nick = 0;
$nome = 0;
$sobrenome = 0;
$email = 0;
$senha = 0;
$lingua = 0;
$nascimento = 0;

$codigo = rand(100000, 999999);


if (isset($_GET['nick'])){
    $nick = $_GET['nick'];
}

if (isset($_POST['nick'])){
    $nick = $_POST['nick'];
    
}


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


if (isset($_GET['email'])){
    $email = $_GET['email'];
}

if (isset($_POST['email'])){
    $email = $_POST['email'];
    
}


if (isset($_GET['senha'])){
    $senha = $_GET['senha'];
}

if (isset($_POST['senha'])){
    $senha = $_POST['senha'];
    
}


if (isset($_GET['lingua'])){
    $lingua = $_GET['lingua'];
}

if (isset($_POST['lingua'])){
    $lingua = $_POST['lingua'];
    
}


if (isset($_GET['nascimento'])){
    $nascimento = $_GET['nascimento'];
}

if (isset($_POST['nascimento'])){
    $nascimento = $_POST['nascimento'];
    
}



	//conexao com o db
	$conexao = new mysqli($nome_server_db,$nome_usuario_db,$senha_db,$nome_db);

    
	if(!$conexao){
        // sem conecao com o banco
        echo "ERRO NA CONEXAO MySQL: ". mysqli_connect_error();
        echo "0,99";
	}else {
		
	    $sql = "INSERT INTO `usuario` (`id`, `confirm`, `nome`, `sobrenome`, `nick`, `email`, `senha`, `criacao`, `nascimento`, `lingua`, `id_gg`, `id_fb`, `id_tt`, `id_nc`) VALUES (NULL, '5".$codigo."', '".$nome."', '".$sobrenome."', '".$nick."', '".$email."', '".$senha."', CURRENT_TIME(), '".$nascimento." 00:00:00', '".$lingua."', 0, 0, 0, 0)";
	    
	    //$sql = "INSERT INTO `usuario` (`id`, `confirm`, `nome`, `sobrenome`, `nick`, `email`, `senha`, `criacao`, `nascimento`, `lingua`, `id_gg`, `id_fb`, `id_tt`, `id_nc`) VALUES (NULL, 'aaa', 'crisvaldo', 'vandirlei', 'jooj', 'slaaaa@aaaaaa.coum', '123123', current_timestamp(), '2019-10-22 00:00:00', 'pt-pt', '', '', '', '')";
	    
        $result = mysqli_query($conexao ,$sql);

        

        $mail->AddAddress($email, $nome . " " . $sobrenome);

        if($lingua == "pt-br" || $lingua == "pt-pt"){
            $mail->Subject = "Codigo de registro";
            $mail->Body = 'Seu código de registro é ' . $codigo . '. <br> Utilise este código para confirmar o e-mail da sua conta JovDev :) <br> <br> ATENÇÃO: SEU CÓDIGO SÓ É VALIDO POR 24 HORAS E DIGITAR O CODIGO ERRADO 5 VEZES IRÁ CANCELAR O SEU REGISTRO!';
        }

        if($lingua == "en-us" || $lingua == "en-uk"){
            $mail->Subject = "Registration Code";
            $mail->Body = 'Your registration code is ' . $codigo . '. <br> Use it to confirm your JovDev account e-mail address :) <br> <br> PLEASE NOTE: YOUR CODE IS ONLY VALID FOR 24 HOURS AND ENTERING THE WRONG CODE 5 TIMES WILL CANCEL YOUR REGISTRATION!';
        }

        if($lingua == "es-mx"){
            $mail->Subject = "Codigo de registro";
            $mail->Body = 'Su código de registro es '. $codigo. '. <br> Utilice este código para confirmar el correo electrónico de su cuenta JovDev :) <br> <br> TENGA EN CUENTA: ¡SU CÓDIGO SÓLO ES VÁLIDO POR 24 HORAS E INGRESAR EL CÓDIGO INCORRECTO 5 VECES CANCELARÁ SU REGISTRO!';
        }
        

        if($mail->Send())
        {

            $sql2 = "SELECT id FROM usuario WHERE email = '".$email."'";

            $result2 = mysqli_query($conexao ,$sql2);

            while($row = mysqli_fetch_assoc($result2)){

                //email enviado
                echo "0,1,". $row['id'];
            }

        }else {
            // email nao enviado
            echo "0,2";
        }
    }
    


?>