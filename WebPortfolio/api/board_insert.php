<?php
include_once("../class/db.php");
?>
<meta charset="utf-8">
<?php
session_start();
if(isset($_SESSION["userid"])) $userid = $_SESSION['userid'];
else $userid = "";
if (isset($_SESSION["username"])) $username = $_SESSION["username"];
else $username = "";
if(isset($_SESSION["userlevel"])) $userlevel = $_SESSION['userlevel'];
else $userlevel = "";
    if ( !$userid )
    {
        echo("
                    <script>
                    alert('게시판 글쓰기는 로그인 후 이용해 주세요!');
                    history.go(-1)
                    </script>
        ");
                exit;
    }
    $title = $_POST["title"];
    $subtitle = $_POST["subtitle"];
    $content = $_POST["content"];
    $field = $_POST["field"];

	$title = htmlspecialchars($title, ENT_QUOTES);
  $subtitle = htmlspecialchars($subtitle, ENT_QUOTES);
	$content = htmlspecialchars($content, ENT_QUOTES);
  $field = htmlspecialchars($field, ENT_QUOTES);

	$regist_day = date("Y-m-d (H:i)");  // 현재의 '년-월-일-시-분'을 저장
	$sql = "insert into board (field ,id, name, level, title, subtitle, content, regist_day)";
	$sql .= " values ('$field', '$userid', '$username', '$userlevel', '$title', '$subtitle', '$content', '$regist_day')";
	mysqli_query($con, $sql);  // $sql 에 저장된 명령 실행
	echo "
	   <script>
	     location.href = '../index.php';
	   </script>
	";
?>
