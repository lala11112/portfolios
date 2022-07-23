
<?php
include_once("./class/admin_header.php");
?>
<script>
   function check_input()
   {
      if (!document.chapter_form.title.value) {
          alert("제목을 입력하세요!");
          document.chapter_form.title.focus();
          return;
      }

      if (!document.chapter_form.subtitle.value) {
          alert("소제목을 입력하세요!");
          return;
      }

      document.chapter_form.submit();
   }
   function reset_form() {
      document.chapter_form.title.value = "";
      document.chapter_form.subtitle.value = "";
      document.chapter_form.title.focus();
      return;
   }

</script>
<form  name="chapter_form" method="post" action="./api/chapter_insert.php">
  <div class="row">

    <h2>목차 생성</h2>
        <div class="form">
            <div class="col-md-4">제목</div>
                <input type="text" name="title">
        </div>
        <div class="clear"></div>

        <div class="form">
            <div class="col-md-4">소제목</div>
            <div class="col-md-4">
                <input type="text" name="subtitle">
            </div>
        </div>
        <div class="clear"></div>

        <div class="bottom_line"> </div>
        <div class="buttons">
        <input type='button' value='목차생성하기' onclick="check_input()"/>&nbsp;
        <input type='button' value='취소하기' onclick="reset_form()"/>
        </div>
  </div>
</form>

<?php
include_once("./class/admin_footer.php");
?>
