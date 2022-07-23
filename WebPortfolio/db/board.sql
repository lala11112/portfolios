CREATE TABLE board(
  num int NOT NULL AUTO_INCREMENT,
  field CHARACTER(80) NOT NULL, -- 분야
  id CHARACTER(20) NOT NULL, -- 글쓴이 아이디
  name CHARACTER(20) NOT NULL, -- 글쓴이 이름
  level int NOT NULL, -- 글쓴이가 관리자인가?
  title CHARACTER(80) NOT NULL, -- 제목
  subtitle CHARACTER(100) NOT NULL, -- 소제목
  content text NOT NULL, -- 글 내용
  regist_day char(20) NOT NULL, -- 작성일
  PRIMARY KEY(num)
);
