<?php
include_once("../class/db.php");
?>
<?php
  $num = $_GET["num"];
  $title = $_POST["title"];
  $subtitle = $_POST["subtitle"];
  echo "
     <script>
         console.log('$title');
     </script>
   ";
  $sql = "update chapter set title='$title', subtitle='$subtitle' where num = $num";
  mysqli_query($con, $sql);
  mysqli_close($con);
  echo "
     <script>
        alert('목차가 수정되었습니다.');
         location.href = '../admin_chapter_update_form.php';
     </script>
   ";
?>
