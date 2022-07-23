<?php
include_once("../class/db.php");
?>
<?php
    $num = $_GET["num"];

    $title = $_POST["title"];
    $subtitle = $_POST["subtitle"];
    $content = $_POST["content"];
    $field = $_POST["field"];

    $sql = "update board set field='$field', title='$title', subtitle='$subtitle', content='$content'";
    $sql .= " where num=$num";
    mysqli_query($con, $sql);

    mysqli_close($con);

    echo "
	      <script>
            alert('게시글이 수정이 되었습니다.');
	          location.href = '../admin.php';
	      </script>
	  ";
?>
