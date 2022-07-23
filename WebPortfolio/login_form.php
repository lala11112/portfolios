<?php
include_once("./header.php");
?>
<?php
include_once("./class/bootstrap.php");
?>

<script>
   function check_input()
   {
      if (!document.login_form.id.value) {
          alert("아이디를 입력하세요!");
          document.member_form.id.focus();
          return;
      }

      if (!document.login_form.pass.value) {
          alert("비밀번호를 입력하세요!");
          document.member_form.pass.focus();
          return;
      }

      document.login_form.submit();
   }

</script>
<form name = "login_form" method="post" action="./api/login.php">
<div class="container" style="width: 800px; height: 60%;">
  <br></br>
  <br></br>
  <h2>로그인</h2>
  <br></br>
  <!-- Email input -->
  <div class="form-outline mb-4">
    <input type="text" class="form-control" id="form1Example1" name="id">
    <label class="form-label" for="form1Example1">아이디</label>
  </div>

  <!-- Password input -->
  <div class="form-outline mb-4">
    <input type="password" id="form1Example2" name="pass" class="form-control" />
    <label class="form-label" for="form1Example2">비밀번호</label>
  </div>

  <!-- 2 column grid layout for inline styling -->
  <div class="row mb-4">
    <div class="col d-flex justify-content-center">
      <!-- Checkbox -->
      <div class="form-check">
        <input class="form-check-input" type="checkbox" id="form1Example3" checked />
        <label class="form-check-label" for="form1Example3"> Remember me </label>
      </div>
    </div>
  </div>
  <!-- Submit button -->
  <button type="submit" class="btn btn-primary btn-block" onclick="check_input()">로그인하기</button>

</div>
</div>
  </form>
<?php
include_once("./footer.php");
?>
