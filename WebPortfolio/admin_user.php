<?php
include_once("./class/admin_header.php");
?>
      <h2>유저 정보</h2>
    <table class="table table-striped">
  <thead>
    <tr>
      <th scope="col">번호</th>
      <th scope="col">아이디</th>
      <th scope="col">이름</th>
      <th scope="col">레벨</th>
      <th scope="col">가입일</th>
      <th scope="col">삭제</th>
    </tr>
  </thead>
  <tbody>
    <?php
      $sql = "select * from men order by num desc";
      $result = mysqli_query($con, $sql);
      $total_record = mysqli_num_rows($result); // 전체 회원 수

      $number = $total_record;

       while ($row = mysqli_fetch_array($result))
       {
        $num         = $row["num"];
        $id          = $row["id"];
        $name        = $row["name"];
        $level       = $row["level"];
        $regist_day  = $row["regist_day"];
    ?>
    <tr>
      <th scope="row"><?=$number?></th>
      <td><?=$id?></td>
      <td><?=$name?></td>
      <td><?=$level?></td>
      <td><?=$regist_day?></td>
      <td><button type="button" onclick="location.href='./api/admin_member_delete.php?num=<?=$num?>'">삭제</button></td>

    </tr>

    <?php
           $number--;
       }
    ?>
  </tbody>
</table>
<?php
include_once("./class/admin_footer.php");
?>
