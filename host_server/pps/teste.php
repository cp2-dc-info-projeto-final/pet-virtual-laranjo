<?php

    $x = 0;
    
    if (isset($_GET['x'])){
	    $x = $_GET['x'];
	}
	
	if (isset($_POST['x'])){
        $x = $_POST['x'];
	}
	
	echo $x;

?>