
<?php
include_once("./class/db.php");
?>
<?php
include_once("./class/bootstrap.php");
?>
<?php
include_once("./header.php");
?>

<div id="carouselExampleCaptions" class="carousel slide" data-bs-ride="carousel" style="background-color: #000000;">
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="3" aria-label="Slide 4"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="4" aria-label="Slide 5"></button>
  </div>

  <div class="carousel-inner">
    <div class="carousel-item active">
      <img src="./img/chapter.jpg" class="rounded mx-auto d-block" style="width: 1000px; height: 600px;  overflow: hidden; border: 5px solid black;" alt="...">
      <div class="carousel-caption d-none d-md-block">
        <h5>목차</h5>
        <p>목차를 살피려면 좌우에 있는 버튼을 클릭하세요.</p>
      </div>
    </div>

<?php
  $sql = "select * from chapter order by num desc";
  $result = mysqli_query($con, $sql);
  $total_record = mysqli_num_rows($result); // 전체 회원 수
  $number = $total_record;

   while ($row = mysqli_fetch_array($result))
   {
    $title = $row["title"];
    $subtitle = $row["subtitle"];
    $belongingflie = $row["belongingflie"];
?>
  <div class="carousel-item">
    <img src="./img/<?=$belongingflie?>.jpg" class="rounded mx-auto d-block" style="width: 1000px; height: 600px;  overflow: hidden; border: 5px solid black;" alt="...">
    <div class="carousel-caption d-none d-md-block">
      <h4><mark><a href="<?=$belongingflie?>.php" class="link-dark"><?=$title?></a></mark></h4>
      <p><mark><?=$subtitle?></mark></p>
    </div>
  </div>

<?php
   	   $number--;
   }
?>

  </div>
<button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
  <span class="carousel-control-prev-icon" aria-hidden="true"></span>
  <span class="visually-hidden">Previous</span>
</button>
<button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
  <span class="carousel-control-next-icon" aria-hidden="true"></span>
  <span class="visually-hidden">Next</span>
</button>
</div>
<?php
include_once("./footer.php");
?>
