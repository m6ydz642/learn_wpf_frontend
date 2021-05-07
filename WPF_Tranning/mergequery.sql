USE [BaseBallGameWinform_DB]
GO

SELECT [Score_id]
      ,[Score_sub]
  FROM [dbo].[SubScoreTable]

GO


--insert into SubScoreTable values (1, 'test')
update SubScoreTable set score_id = 3 where score_id = 2;
update ScoreTable set score_id = 2 where score_id = 2; 




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
drop procedure Save_Score
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
USE [BaseBallGameWinform_DB]
GO
CREATE TYPE TYPE_SaveScore AS TABLE(
-- 사용자 정의 테이블 (저장, 수정용)
    checkbox bit,
	Score_id int,

	Score varchar(max),
	 modify datetime,
	 createdt datetime

)

GO


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








