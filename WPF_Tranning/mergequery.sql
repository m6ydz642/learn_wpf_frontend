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

MERGE INTO SubScoreTable --INSERT/UPDATE 할 테이블
 USING (SELECT 1AS DUM) X
      ON (score_id= 123)--조건
    WHEN MATCHED THEN --위 조건에 맞는 데이터가 있으면 UPDATE
            UPDATE SET
            score_id=4
            ,score_sub='BBB'		    
			
			WHEN NOT MATCHED THEN --위 조건에 맞1는 데이터가 없으면 INSERT
            INSERT (score_id, score_sub)
            VALUES(
                30
                ,'조건미달2');

MERGE INTO SubScoreTable --INSERT/UPDATE 할 테이블
 USING (SELECT 1AS DUM) X
      ON (score_id= 1234)--조건
    WHEN MATCHED THEN --위 조건에 맞는 데이터가 있으면 UPDATE
            UPDATE SET
            score_id=2
            ,score_sub='BBB'

WHEN NOT MATCHED THEN --위 조건에 맞1는 데이터가 없으면 INSERT
            INSERT (score_id, score_sub)
            VALUES(
                40
                ,'조건미달3');

    --WHEN NOT MATCHED THEN --위 조건에 맞1는 데이터가 없으면 INSERT
    --        INSERT (score_id, score_sub)
    --        VALUES(
    --            5
    --            ,'조건미달'
--  );
          

	


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



