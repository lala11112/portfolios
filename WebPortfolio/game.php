<?php
include_once("./header.php");
?>
<section>
   	<div id="board_box">
	    <h3>
	    	게임 게시글 목록
		</h3>

<div class="row justify-content-md-center">
<?php

	$sql = "select * from board order by num desc";
	$result = mysqli_query($con, $sql);
	$total_record = mysqli_num_rows($result); // 전체 글 수


	$number = $total_record;

   for ($i=0; $i < $total_record; $i++)
   {
      mysqli_data_seek($result, $i);
      // 가져올 레코드로 위치(포인터) 이동
      $row = mysqli_fetch_array($result);
      // 하나의 레코드 가져오기
	    $num         = $row["num"];
	    $id          = $row["id"];
	    $name        = $row["name"];
	    $title     = $row["title"];
      $subtitle     = $row["subtitle"];
      $regist_day  = $row["regist_day"];
      $field = $row["field"];
      if($field == "게임"){
?>
  <div class="col-sm-4 card text-center">
    <div class="card border-dark mb-3">
      <div class="card-header"><?=$name?></div>
      <img src="./img/<?=$title?>.png" style="width: 400px; height: 400px;  overflow: hidden;" class="card-img-top img-thumbnail rounded mx-auto d-block" alt="...">
      <div class="card-body text-dark">
        <h5 class="card-title"><?=$title?></h5>
        <p class="card-text"><?=$subtitle?></p>

        <a href="board_view.php?num=<?=$num?>" class="btn btn-primary">Go <?=$title?></a>
      </div>
      <div class="card-footer">
          <small class="text-muted"><?=$regist_day?></small>
    </div>
    </div>
  </div>
<?php
        }
   	   $number--;
   }
   mysqli_close($con);

?>
</div>
	</div> <!-- board_box -->
</section>
<?php
include_once("./footer.php");
?>
