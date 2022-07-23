<?php
include_once("./header.php");
?>


<section>


<?php
	$num  = $_GET["num"];

	$sql = "select * from board where num=$num";
	$result = mysqli_query($con, $sql);

	// $result에서 데이터 가져오기
	$row = mysqli_fetch_array($result);
	$id      = $row["id"];
	$name      = $row["name"];
	$regist_day = $row["regist_day"];
	$title    = $row["title"];
	$subtitle    = $row["subtitle"];
	$content    = $row["content"];

	$content = str_replace(" ", "&nbsp;", $content);
	$content = str_replace("\n", "<br>", $content);
?>
			<div id="board_box">
				<h3 class="title"><?=$title?> <small class="text-muted"><?=$subtitle?></small></h3>
	    <ul id="view_content">
			<li>
				<span class="col3"><?=$name?> | <?=$regist_day?></span>
			</li>
			<li>
				<?=$content?>
			</li>
	    </ul>
	    <ul class="buttons">
				<li><button onclick="location.href='board_list.php?page=<?=$page?>'">목록</button></li>
				<li><button onclick="location.href='board_delete.php?num=<?=$num?>&page=<?=$page?>'">삭제</button></li>
				<li><button onclick="location.href='board_form.php'">글쓰기</button></li>
		</ul>
	</div> <!-- board_box -->
</section>
<?php
include_once("./footer.php");
?>
