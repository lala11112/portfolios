<?php
include_once("./class/admin_header.php");
?>
<h2>목차 수정</h2>
<table class="table table-striped">
<thead>
<tr>
  <th scope="col">번호</th>
  <th scope="col">제목</th>
  <th scope="col">소제목</th>
  <th scope="col">수정</th>
  <th scope="col">삭제</th>
</tr>
</thead>
<tbody>
<?php
  $sql = "select * from chapter order by num desc";
  $result = mysqli_query($con, $sql);
  $total_record = mysqli_num_rows($result); // 전체 회원 수

  $number = $total_record;

   while ($row = mysqli_fetch_array($result))
   {
     $num = $row["num"];
     $title = $row["title"];
     $subtitle = $row["subtitle"];
?>
<form method="post" action="./api/admin_chapter_update.php?num=<?=$num?>">
  <tr>
    <th scope="row"><?=$number?></th>
    <td><input type="text" name="title" value="<?=$title?>"></td>
    <td><input type="text" name="subtitle" value="<?=$subtitle?>"></td>
    <td><button type="submit">수정</button></td>
    <td><button type="button" onclick="location.href='./api/admin_chapter_delete.php?num=<?=$num?>'">삭제</button></td>
  </tr>
</form>

<?php
       $number--;
   }
?>
</tbody>
</table>
<?php
  include_once("./class/admin_footer.php");
?>
