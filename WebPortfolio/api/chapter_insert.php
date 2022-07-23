<?php
include_once("../class/db.php");
?>
<?php
    $title   = $_POST["title"];
    $subtitle = $_POST["subtitle"];


	$sql = "INSERT INTO chapter(title, subtitle) ";
	$sql .= "VALUES('$title', '$subtitle')";

	mysqli_query($con, $sql);  // $sql 에 저장된 명령 실행
    mysqli_close($con);

    echo "
	      <script>
	          location.href = '../admin.php';
	      </script>
	  ";
?>
