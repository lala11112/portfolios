<?php
include_once("../class/db.php");
?>
<?php

    $num   = $_GET["num"];


    $sql = "delete from men where num = $num";
    mysqli_query($con, $sql);

    mysqli_close($con);
    echo "
	     <script>
            alert('유저가 삭제되었습니다.');
	         location.href = '../admin_user.php';
	     </script>
	   ";
?>
