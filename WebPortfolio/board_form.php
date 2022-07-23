<?php
include_once("./header.php");
?>
<?php
include_once("./class/bootstrap.php");
?>
<?php
include_once("./class/db.php");
?>
<script>
  function check_input() {
      if (!document.board_form.title.value)
      {
          alert("제목을 입력하세요!");
          document.board_form.title.focus();
          return;
      }
      if (!document.board_form.subtitle.value)
      {
          alert("제목을 입력하세요!");
          document.board_form.subtitle.focus();
          return;
      }
      if (!document.board_form.content.value)
      {
          alert("내용을 입력하세요!");
          document.board_form.content.focus();
          return;
      }
      if (!document.board_form.field.value)
      {
          alert("주제를 입력하세요!");
          document.board_form.field.focus();
          return;
      }
      document.board_form.submit();
   }
</script>
<form  name="board_form" method="post" action="./api/board_insert.php" enctype="multipart/form-data">
    <h2>게시판 글쓰기</h2>
    <form class="form-horizontal">
  <div class="form-group">
    <label for="text" class="col-md-7 control-label">제목</label>
    <div class="col-sm-7">
      <input type="text" name="title" class="form-control" id="inputEmail3" placeholder="제목">
    </div>
  </div>
  <div class="form-group">
    <label for="text" class="col-md-7 control-label">소제목</label>
    <div class="col-sm-7">
      <input type="text" name="subtitle" class="form-control" id="inputEmail3" placeholder="소제목">
    </div>
  </div>
  <div class="form-group">
    <label for="text" class="col-sm-2 control-label">내용</label>
    <div class="col-sm-7">
      <textarea class="form-control" name="content" rows="10" placeholder="내용"></textarea>
    </div>
  </div>
  <div class="form-group">
    <label for="inputEmail3" class="col-md-7 control-label">주제</label>
    <div class="col-sm-7">
      <select class="form-control" name="field">
        <?php
          $sql = "SELECT * FROM chapter ORDER BY num DESC";
          $result = mysqli_query($con, $sql);
          $chapterlist = mysqli_num_rows($result);


          for($i=0; $i<$chapterlist; $i++){
            mysqli_data_seek($result, $i);
            $row = mysqli_fetch_array($result);
            $title = $row["title"];
        ?>
          <option><?=$title?></option>
        <?php
          }
        ?>
      </select>
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <button type="submit" class="btn btn-default" onclick="check_input()"/>완료</button>
      <button type="submit" class="btn btn-default" onclick="reset_form()"/>취소</button>
    </div>
  </div>
</form>
</form>
<?php
include_once("./footer.php");
?>
