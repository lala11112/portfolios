<?php
include_once("./class/admin_header.php");
?>
          <h2>게시글 정보</h2>
          <table class="table table-striped">
        <thead>
          <tr>
            <th scope="col">번호</th>
            <th scope="col">분야</th>
            <th scope="col">작성자</th>
            <th scope="col">레벨</th>
            <th scope="col">제목</th>
            <th scope="col">소제목</th>
            <th scope="col">게시일</th>
            <th scope="col">수정</th>
            <th scope="col">삭제</th>
          </tr>
        </thead>
        <tbody>
          <?php
            $sql = "select * from board order by num desc";
            $result = mysqli_query($con, $sql);
            $total_record = mysqli_num_rows($result); // 전체 회원 수

            $number = $total_record;

             while ($row = mysqli_fetch_array($result))
             {
               $num         = $row["num"];
               $field          = $row["field"];
               $name        = $row["name"];
               $level       = $row["level"];
               $title  = $row["title"];
               $subtitle  = $row["subtitle"];
               $regist_day  = $row["regist_day"];
          ?>
          <tr>
            <th scope="row"><?=$number?></th>
            <td><?=$field?></td>
            <td><?=$name?></td>
            <td><?=$level?></td>
            <td><?=$title?></td>
            <td><?=$subtitle?></td>
            <td><?=$regist_day?></td>
            <td><button type="button" onclick="location.href='./board_modity_form.php?num=<?=$num?>'">수정</button></td>
            <td><button type="button" onclick="location.href='./api/board_delete.php?num=<?=$num?>'">삭제</button></td>

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
