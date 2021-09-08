

-- html 테스트 DB생성

CREATE Table Notice(
    Notice_id  int IDENTITY(1,1), -- autoincrement (IDENTITY)
	NoticeContent NVARCHAR(max),
	NoticeWriterEmpNo varchar(100),
	NoticeWriteDate DateTime default(getdate()),
	NoticeModifyEmpNo varchar(100),
	NoticeModifyDate DateTime default(getdate())
)

-- 공지 조회 프로시저
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[SelectNotice] 
-- 공지 조회
/* exec [dbo].[SelectNotice] */
as Begin

select * from [dbo].[Notice]

end

-- 공지 추가 프로시저
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[WriteNotice] 
-- 공지 추가
/* exec [dbo].[WriteNotice] '내용','작성자' */
 @Content NVARCHAR(max)
  ,@WriterEmpNo NVARCHAR(100)

as Begin
insert into [Notice](NoticeContent, NoticeWriterEmpNo ) values
	(@Content, @WriterEmpNo)

end

-- 공지 수정 프로시저
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[ModifyNotice] 
-- 공지 수정
/* exec [dbo].[ModifyNotice] 1, '내용','작성자' */
@Notice_id int,
@Content NVARCHAR(max),
@ModifyEmpNo NVARCHAR(100)

as Begin
update [Notice] 
	set 
		NoticeContent = @Content,
		NoticeModifyEmpNo = @ModifyEmpNo
	where Notice_id = @Notice_id
end


--------------------------------------------------------------------
-- 공지 수정 or 추가
USE [BaseBallGameWinform_DB]
GO
Create procedure [dbo].[SaveNotice]
@Notice_id int,
@Content NVARCHAR(max),
--@ModifyEmpNo NVARCHAR(100),
@EmpNo NVARCHAR(100)

as Begin
MERGE INTO notice as A --INSERT/UPDATE 할 테이블
 USING (SELECT 1AS DUM) X
 ON (A.notice_id = @Notice_id )--조건

			when Matched  then  
				UPDATE SET  A.NoticeContent = @Content, A.NoticeModifyDate = getdate(), A.NoticeModifyEmpNo = @EmpNo

			WHEN not MATCHED THEN 
		 	INSERT (notice_id,NoticeContent, NoticeWriterEmpNo, NoticeWriteDate) VALUES(@Notice_id, @Content, @EmpNo, getdate());

end

--------------------------------------------------------------------
select top 1 notice_id from notice order by notice_id desc

--------------------------------------------------------------------
SET IDENTITY_INSERT notice ON

exec [dbo].[SaveNotice] 1, '
<head>

<title>Title is here</title>

</head>

<body>

<script>

alert("Hello, world!");

</script>

</body>
', '준2'

------------------------------------------------------------------------------

exec [Save_Double_Score]
exec CheckScoreDouble

drop type TYPE_SaveDoubleScore
drop procedure [dbo].[Save_Double_Score] 

---------------------------------------------------------
-- decimal 조회
USE [BaseBallGameWinform_DB]
GO

exec [dbo].[CheckScoreDouble]

select * from dbo.ScoreDouble
--------------------------------------------------------------------------------
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[CheckScoreDouble] 
-- 정규식 검사전용 테스트 테이블
as Begin

select * from ScoreDouble

end




CREATE TYPE TYPE_SaveDoubleScore AS TABLE(
-- 사용자 정의 테이블 (소수점 컬럼 수정내용 저장)
    Score_id bit,
	Score_double decimal,
	updatedate date
)

--------------------
-- 스코어 decimal 저장 프로시저
USE [BaseBallGameWinform_DB]
GO
Create procedure [dbo].[Save_Double_Score] 
@Get_SaveDoubleScore TYPE_SaveDoubleScore READONLY
-- decimal 스코어 저장 프로시저
as Begin
MERGE INTO ScoreDouble as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveDoubleScore as B
      ON (A.Score_id = B.Score_id )--조건

			when Matched  then  
				UPDATE SET  A.Score_double = B.Score_double, A.updatedate=getdate() -- 내용변경만 가능하게 수정

			WHEN not MATCHED THEN 
		 	INSERT (Score_id,Score_double, updatedate) VALUES(B.Score_id, B.Score_double, getdate());

end
------------------------------------------------------------------------------


CREATE TYPE TYPE_SaveScore AS TABLE(
-- 사용자 정의 테이블 (저장, 수정용)
    checkbox bit,
	Score_id int,

	Score varchar(max),
	 modify datetime,
	 createdt datetime

)

GO
-----------------------------------------------------
-- 스코어 저장 프로시저
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[Save_Score] 
@Get_SaveScore TYPE_SaveScore READONLY
-- 스코어 저장 프로시저
as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--조건

			when Matched  and A.Score != B.Score then 
					   UPDATE SET  A.Score = B.Score , A.modify=getdate()-- 내용변경만 가능하게 수정

		WHEN not MATCHED  THEN --B.Score_id != B.Score_id 자체가 말이 안되는 조건이라 insert도 안됨
		 			          INSERT (score,createdt) VALUES(B.Score, getdate());

end

------------------------------------------------------------------------------






select * from ScoreTable
select * from SubScoreTable

SELECT *
  FROM (
         SELECT Score, Score_id
           FROM ScoreTable
       ) AS result
 PIVOT ( 
       sum(Score_id) for Score IN ([1], [2], [3], [4],[5],[10]) 
       ) AS pivot_result



	


	---------------------------------조건에 맞는 경우 가만히 놔두기
	MERGE INTO SubScoreTable --INSERT/UPDATE 할 테이블
 USING (SELECT 1AS DUM) X
      ON (score_id= 111 and score_sub = 'test')--조건2개
--   WHEN MATCHED THEN --위 조건에 맞는 데이터가 있으면 UPDATE
          
WHEN NOT MATCHED THEN --위 조건에 맞1는 데이터가 없으면 INSERT
     UPDATE SET
            score_id=3
            ,score_sub='BBB';


---------------------------------------------
			-- 프로시저 정의

			USE [BaseBallGameWinform_DB]
GO

create procedure Score_Modify
@Score_id int,
@Score varchar(300)

as Begin
MERGE INTO SubScoreTable --INSERT/UPDATE 할 테이블
 USING (SELECT 1AS DUM) X
      ON (score_id= 111 and score_sub = 'test')--조건2개
    WHEN MATCHED THEN --위 조건에 맞는 데이터가 있으면 UPDATE
            UPDATE SET
            score_id=3
            ,score_sub='BBB'
			    WHEN NOT MATCHED THEN --위 조건에 맞1는 데이터가 없으면 INSERT
            INSERT (score_id, score_sub)
            VALUES(
                20
                ,'조건미달1');
end
--------------------------------------------------
exec dbo.Score_Modify 1,'test'
exec Score_Modify;


SET IDENTITY_INSERT (ScoreTable) OFF


------------------------------------------------- select 선택 프로시저
USE [BaseBallGameWinform_DB]
GO



create procedure Score_Select
@Score_id int

as Begin

   SELECT convert(bit,0) as '체크박스', [Score_id]
      ,[Score]
  FROM [dbo].[ScoreTable] where Score_Id = @Score_id;

end

------------------------------------------------




select * from information_schema.table_constraints where table_name = 'dbo.TB_AuthUser' -- 테이블 기준 외래키 조회

-----------------------------------------------------------
drop procedure [dbo].[Save_Score] 
drop type TYPE_SaveScore
exec Save_Score
select * from ScoreTable
-----------------------------------------------------------
USE [BaseBallGameWinform_DB]
GO
create procedure Save_Score 
@Get_SaveScore TYPE_SaveScore READONLY

as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id)--조건

			when Matched then 
			     UPDATE SET  A.Score = B.Score , A.modify=getdate() -- 내용변경만 가능하게 수정

			WHEN NOT MATCHED THEN --위 조건에 맞1는 데이터가 없으면 INSERT
            INSERT (score,createdt)
            VALUES(
              
                B.Score, B.createdt);
end


-----------------------------------------------
-- 0501 2021 AM 02:06 저장 프로시저 수정본 (수정날짜, 추가날짜 별도)
USE [BaseBallGameWinform_DB]
GO
/****** Object:  StoredProcedure [dbo].[Save_Score]    Script Date: 2021-05-01 오전 1:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Save_Score] 
@Get_SaveScore TYPE_SaveScore READONLY

as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--조건

			when Matched  and A.Score != B.Score then 
					   UPDATE SET  A.Score = B.Score , A.modify=getdate();-- 내용변경만 가능하게 수정

			--WHEN not MATCHED and B.Score_id != B.Score_id THEN --B.Score_id != B.Score_id 자체가 말이 안되는 조건이라 insert도 안됨
		 -- 			          INSERT (score,createdt) VALUES(B.Score, getdate());

MERGE INTO ScoreTable as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--조건

			--when Matched  and A.Score != B.Score then 
			--		   UPDATE SET  A.Score = B.Score , A.modify=getdate()-- 내용변경만 가능하게 수정

			WHEN not MATCHED THEN 
		  			          INSERT (score,createdt) VALUES(B.Score, getdate());
end

-----------------------------------------------



------------------------------------------------- 스코어 검색 프로시저
USE [BaseBallGameWinform_DB]
GO



create procedure Score_Search
@Score_id int

as Begin

   SELECT  convert(bit,0) as '체크박스', [Score_id]
      ,[Score] 
   from ScoreTable 
   where Score_Id = @Score_id;

end

---------------------------------------------------------------------------
USE [BaseBallGameWinform_DB]
GO
/****** Object:  StoredProcedure [dbo].[Save_Score]    Script Date: 2021-05-08 오전 3:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Save_Score] 
@Get_SaveScore TYPE_SaveScore READONLY

as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--조건

			when Matched  and A.Score != B.Score then 
					   UPDATE SET  A.Score = B.Score , A.modify=getdate();-- 내용변경만 가능하게 수정

			--WHEN not MATCHED and B.Score_id != B.Score_id THEN --B.Score_id != B.Score_id 자체가 말이 안되는 조건이라 insert도 안됨
		 -- 			          INSERT (score,createdt) VALUES(B.Score, getdate());

MERGE INTO ScoreTable as A --INSERT/UPDATE 할 테이블
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--조건

			--when Matched  and A.Score != B.Score then 
			--		   UPDATE SET  A.Score = B.Score , A.modify=getdate()-- 내용변경만 가능하게 수정

			WHEN not MATCHED THEN 
		  			          INSERT (score,createdt) VALUES(B.Score, getdate());
end


---------------------------------------------- 프로시저 스코어 저장 이전버전









