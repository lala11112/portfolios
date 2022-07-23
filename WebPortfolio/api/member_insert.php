<?php
include_once("../class/db.php");
?>
<?php
    $id   = $_POST["id"];
    $pass = $_POST["pass"];
    $name = $_POST["name"];
    $email1  = $_POST["email1"];
    $email2  = $_POST["email2"];

    $email = $email1."@".$email2;
    $regist_day = date("Y-m-d (H:i)");  // 현재의 '년-월-일-시-분'을 저장


	$sql = "INSERT INTO men(id, pass, level, email, name, regist_day) ";
	$sql .= "VALUES('$id', '$pass', 1,'$email', '$name', '$regist_day')";

	mysqli_query($con, $sql);  // $sql 에 저장된 명령 실행
    mysqli_close($con);

    echo "
	      <script>
	          location.href = '../index.php';
	      </script>
	  ";
?>
