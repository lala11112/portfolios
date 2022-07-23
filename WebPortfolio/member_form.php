<?php error_reporting(E_ERROR | E_WARNING | E_PARSE); ?>
<?php
include_once("./header.php");
?>
<?php
include_once("./class/bootstrap.php");
?>
<script>
   function check_input()
   {
      if (!document.member_form.id.value) {
          alert("아이디를 입력하세요!");
          document.member_form.id.focus();
          return;
      }

      if (!document.member_form.pass.value) {
          alert("비밀번호를 입력하세요!");
          document.member_form.pass.focus();
          return;
      }

      if (!document.member_form.pass_confirm.value) {
          alert("비밀번호확인을 입력하세요!");
          document.member_form.pass_confirm.focus();
          return;
      }
      if (!document.member_form.name.value) {
          alert("이름을 입력하세요!");
          document.member_form.name.focus();
          return;
      }

      if (!document.member_form.email1.value) {
          alert("이메일 주소를 입력하세요!");
          document.member_form.email1.focus();
          return;
      }

      if (document.member_form.pass.value !=
            document.member_form.pass_confirm.value) {
          alert("비밀번호가 일치하지 않습니다.\n다시 입력해 주세요!");
          document.member_form.pass.focus();
          document.member_form.pass.select();
          return;
      }

      document.member_form.submit();
   }
   function reset_form() {
      document.member_form.id.value = "";
      document.member_form.pass.value = "";
      document.member_form.pass_confirm.value = "";
      document.member_form.name.value = "";
      document.member_form.email1.value = "";
      document.member_form.email2.value = "";
      document.member_form.id.focus();
      return;
   }

</script>
<form  name="member_form" method="post" action="./api/member_insert.php">
    <h2>회원 가입</h2>
        <div class="form id">
            <div class="col1">아이디</div>
                <input type="text" name="id">
        </div>
        <div class="clear"></div>

        <div class="form">
            <div class="col1">비밀번호</div>
            <div class="col2">
                <input type="password" name="pass">
            </div>
        </div>
        <div class="clear"></div>

        <div class="form">
            <div class="col1">비밀번호 확인</div>
            <div class="col2">
                <input type="password" name="pass_confirm">
            </div>
        </div>
        <div class="form">
          <div class="col1">이름</div>
          <div class="col2">
        <input type="text" name="name">
          </div>
        </div>
        <div class="clear"></div>
        <div class="form email">
          <div class="col1">이메일</div>
          <div class="col2">
        <input type="text" name="email1">@<input type="text" name="email2">
          </div>
        </div>

        <div class="bottom_line"> </div>
        <div class="buttons">
        <input type='button' value='회원가입하기' onclick="check_input()"/>&nbsp;
        <input type='button' value='취소하기' onclick="reset_form()"/>
        </div>
</form>

<?php
include_once("./footer.php");
?>
