<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css">
    </head>
    <body>
      <?php
        include_once $_SERVER["DOCUMENT_ROOT"]."/class/bootstrap.php";
      ?>
      <?php
      include_once $_SERVER["DOCUMENT_ROOT"]."/class/chapter_list.php";
      ?>
      <?php
        session_start();
        if(isset($_SESSION["userid"])) $userid = $_SESSION['userid'];
        else $userid = "";
        if (isset($_SESSION["username"])) $name = $_SESSION["username"];
        else $name = "";
        if(isset($_SESSION["userlevel"])) $userlevel = $_SESSION['userlevel'];
        else $userlevel = "";
      ?>
<?php
  if(!$userid){
 ?>
 <nav class="navbar navbar-expand-lg bg-light">
   <div class="container-fluid">
     <a class="navbar-brand" href="index.php">박재웅의 포트폴리오</a>
     <button class="navbar-toggler btn btn-primary btn-lg active dropdown-toggle" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      메뉴
     </button>
     <div class="collapse navbar-collapse" id="navbarSupportedContent">
       <ul class="navbar-nav me-auto mb-2 mb-lg-0">
         <li class="nav-item">
           <a class="nav-link active" aria-current="page" href="index.php">메인 페이지로</a>
         </li>
         <li class="nav-item dropdown">
           <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
             포트폴리오 목차
           </a>
           <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
           <?php

              while ($row = mysqli_fetch_array($result))
              {
               $title = $row["title"];
               $belongingflie = $row["belongingflie"];
           ?>
                 <li><a class="dropdown-item" href="./<?=$belongingflie?>.php"><?=$title?></a></li>
           <?php
              	   $number--;
              }
              $number = $total_record;
           ?>
           <li><hr class="dropdown-divider"></li>
           <li><a class="dropdown-item" href="simple_chapter_menu.php">목차 목록 창으로 이동</a></li>
           </ul>
         </li>
       </ul>
       <form class="d-flex" role="search">
           <a class="nav-link" href="member_form.php">회원가입</a>
       </form>
       <form class="d-flex" role="search">
           <a class="nav-link" href="login_form.php">로그인</a>
       </form>
     </div>
   </div>
 </nav>
<?php
  }
  else{
?>
<nav class="navbar navbar-expand-lg bg-light">
  <div class="container-fluid">
    <a class="navbar-brand" href="index.php">박재웅의 포트폴리오</a>
    <button class="navbar-toggler btn btn-primary btn-lg active dropdown-toggle" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      메뉴
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="index.php">메인 페이지로</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="board_form.php">게시글 작성하기</a>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            포트폴리오 목차
          </a>
          <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
          <?php

             while ($row = mysqli_fetch_array($result))
             {
              $title = $row["title"];
              $belongingflie = $row["belongingflie"];
          ?>
                <li><a class="dropdown-item" href="./<?=$belongingflie?>.php"><?=$title?></a></li>
          <?php
             	   $number--;
             }
          ?>
          <li><hr class="dropdown-divider"></li>
          <li><a class="dropdown-item" href="simple_chapter_menu.php">목차 목록 창으로 이동</a></li>
          </ul>
        </li>
      </ul>
      <form class="d-flex" role="search">
          <a class="nav-link" href="./api/logout.php">로그아웃</a>
      </form>
    </div>
  </div>
</nav>
  <?php
    }
  ?>
