

-- html �׽�Ʈ DB����

CREATE Table Notice(
    Notice_id  int IDENTITY(1,1), -- autoincrement (IDENTITY)
	NoticeContent NVARCHAR(max),
	NoticeWriterEmpNo varchar(100),
	NoticeWriteDate DateTime default(getdate()),
	NoticeModifyEmpNo varchar(100),
	NoticeModifyDate DateTime default(getdate())
)

-- ���� ��ȸ ���ν���
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[SelectNotice] 
-- ���� ��ȸ
/* exec [dbo].[SelectNotice] */
as Begin

select * from [dbo].[Notice]

end

-- ���� �߰� ���ν���
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[WriteNotice] 
-- ���� �߰�
/* exec [dbo].[WriteNotice] '����','�ۼ���' */
 @Content NVARCHAR(max)
  ,@WriterEmpNo NVARCHAR(100)

as Begin
insert into [Notice](NoticeContent, NoticeWriterEmpNo ) values
	(@Content, @WriterEmpNo)

end

-- ���� ���� ���ν���
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[ModifyNotice] 
-- ���� ����
/* exec [dbo].[ModifyNotice] 1, '����','�ۼ���' */
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
-- ���� ���� or �߰�
USE [BaseBallGameWinform_DB]
GO
Create procedure [dbo].[SaveNotice]
@Notice_id int,
@Content NVARCHAR(max),
--@ModifyEmpNo NVARCHAR(100),
@EmpNo NVARCHAR(100)

as Begin
MERGE INTO notice as A --INSERT/UPDATE �� ���̺�
 USING (SELECT 1AS DUM) X
 ON (A.notice_id = @Notice_id )--����

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
', '��2'

------------------------------------------------------------------------------

exec [Save_Double_Score]
exec CheckScoreDouble

drop type TYPE_SaveDoubleScore
drop procedure [dbo].[Save_Double_Score] 

---------------------------------------------------------
-- decimal ��ȸ
USE [BaseBallGameWinform_DB]
GO

exec [dbo].[CheckScoreDouble]

select * from dbo.ScoreDouble
--------------------------------------------------------------------------------
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[CheckScoreDouble] 
-- ���Խ� �˻����� �׽�Ʈ ���̺�
as Begin

select * from ScoreDouble

end




CREATE TYPE TYPE_SaveDoubleScore AS TABLE(
-- ����� ���� ���̺� (�Ҽ��� �÷� �������� ����)
    Score_id bit,
	Score_double decimal,
	updatedate date
)

--------------------
-- ���ھ� decimal ���� ���ν���
USE [BaseBallGameWinform_DB]
GO
Create procedure [dbo].[Save_Double_Score] 
@Get_SaveDoubleScore TYPE_SaveDoubleScore READONLY
-- decimal ���ھ� ���� ���ν���
as Begin
MERGE INTO ScoreDouble as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveDoubleScore as B
      ON (A.Score_id = B.Score_id )--����

			when Matched  then  
				UPDATE SET  A.Score_double = B.Score_double, A.updatedate=getdate() -- ���뺯�游 �����ϰ� ����

			WHEN not MATCHED THEN 
		 	INSERT (Score_id,Score_double, updatedate) VALUES(B.Score_id, B.Score_double, getdate());

end
------------------------------------------------------------------------------


CREATE TYPE TYPE_SaveScore AS TABLE(
-- ����� ���� ���̺� (����, ������)
    checkbox bit,
	Score_id int,

	Score varchar(max),
	 modify datetime,
	 createdt datetime

)

GO
-----------------------------------------------------
-- ���ھ� ���� ���ν���
USE [BaseBallGameWinform_DB]
GO
create procedure [dbo].[Save_Score] 
@Get_SaveScore TYPE_SaveScore READONLY
-- ���ھ� ���� ���ν���
as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--����

			when Matched  and A.Score != B.Score then 
					   UPDATE SET  A.Score = B.Score , A.modify=getdate()-- ���뺯�游 �����ϰ� ����

		WHEN not MATCHED  THEN --B.Score_id != B.Score_id ��ü�� ���� �ȵǴ� �����̶� insert�� �ȵ�
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



	


	---------------------------------���ǿ� �´� ��� ������ ���α�
	MERGE INTO SubScoreTable --INSERT/UPDATE �� ���̺�
 USING (SELECT 1AS DUM) X
      ON (score_id= 111 and score_sub = 'test')--����2��
--   WHEN MATCHED THEN --�� ���ǿ� �´� �����Ͱ� ������ UPDATE
          
WHEN NOT MATCHED THEN --�� ���ǿ� ��1�� �����Ͱ� ������ INSERT
     UPDATE SET
            score_id=3
            ,score_sub='BBB';


---------------------------------------------
			-- ���ν��� ����

			USE [BaseBallGameWinform_DB]
GO

create procedure Score_Modify
@Score_id int,
@Score varchar(300)

as Begin
MERGE INTO SubScoreTable --INSERT/UPDATE �� ���̺�
 USING (SELECT 1AS DUM) X
      ON (score_id= 111 and score_sub = 'test')--����2��
    WHEN MATCHED THEN --�� ���ǿ� �´� �����Ͱ� ������ UPDATE
            UPDATE SET
            score_id=3
            ,score_sub='BBB'
			    WHEN NOT MATCHED THEN --�� ���ǿ� ��1�� �����Ͱ� ������ INSERT
            INSERT (score_id, score_sub)
            VALUES(
                20
                ,'���ǹ̴�1');
end
--------------------------------------------------
exec dbo.Score_Modify 1,'test'
exec Score_Modify;


SET IDENTITY_INSERT (ScoreTable) OFF


------------------------------------------------- select ���� ���ν���
USE [BaseBallGameWinform_DB]
GO



create procedure Score_Select
@Score_id int

as Begin

   SELECT convert(bit,0) as 'üũ�ڽ�', [Score_id]
      ,[Score]
  FROM [dbo].[ScoreTable] where Score_Id = @Score_id;

end

------------------------------------------------




select * from information_schema.table_constraints where table_name = 'dbo.TB_AuthUser' -- ���̺� ���� �ܷ�Ű ��ȸ

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
MERGE INTO ScoreTable as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id)--����

			when Matched then 
			     UPDATE SET  A.Score = B.Score , A.modify=getdate() -- ���뺯�游 �����ϰ� ����

			WHEN NOT MATCHED THEN --�� ���ǿ� ��1�� �����Ͱ� ������ INSERT
            INSERT (score,createdt)
            VALUES(
              
                B.Score, B.createdt);
end


-----------------------------------------------
-- 0501 2021 AM 02:06 ���� ���ν��� ������ (������¥, �߰���¥ ����)
USE [BaseBallGameWinform_DB]
GO
/****** Object:  StoredProcedure [dbo].[Save_Score]    Script Date: 2021-05-01 ���� 1:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Save_Score] 
@Get_SaveScore TYPE_SaveScore READONLY

as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--����

			when Matched  and A.Score != B.Score then 
					   UPDATE SET  A.Score = B.Score , A.modify=getdate();-- ���뺯�游 �����ϰ� ����

			--WHEN not MATCHED and B.Score_id != B.Score_id THEN --B.Score_id != B.Score_id ��ü�� ���� �ȵǴ� �����̶� insert�� �ȵ�
		 -- 			          INSERT (score,createdt) VALUES(B.Score, getdate());

MERGE INTO ScoreTable as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--����

			--when Matched  and A.Score != B.Score then 
			--		   UPDATE SET  A.Score = B.Score , A.modify=getdate()-- ���뺯�游 �����ϰ� ����

			WHEN not MATCHED THEN 
		  			          INSERT (score,createdt) VALUES(B.Score, getdate());
end

-----------------------------------------------



------------------------------------------------- ���ھ� �˻� ���ν���
USE [BaseBallGameWinform_DB]
GO



create procedure Score_Search
@Score_id int

as Begin

   SELECT  convert(bit,0) as 'üũ�ڽ�', [Score_id]
      ,[Score] 
   from ScoreTable 
   where Score_Id = @Score_id;

end

---------------------------------------------------------------------------
USE [BaseBallGameWinform_DB]
GO
/****** Object:  StoredProcedure [dbo].[Save_Score]    Script Date: 2021-05-08 ���� 3:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Save_Score] 
@Get_SaveScore TYPE_SaveScore READONLY

as Begin
MERGE INTO ScoreTable as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--����

			when Matched  and A.Score != B.Score then 
					   UPDATE SET  A.Score = B.Score , A.modify=getdate();-- ���뺯�游 �����ϰ� ����

			--WHEN not MATCHED and B.Score_id != B.Score_id THEN --B.Score_id != B.Score_id ��ü�� ���� �ȵǴ� �����̶� insert�� �ȵ�
		 -- 			          INSERT (score,createdt) VALUES(B.Score, getdate());

MERGE INTO ScoreTable as A --INSERT/UPDATE �� ���̺�
 USING @Get_SaveScore as B
      ON (A.Score_id = B.Score_id )--����

			--when Matched  and A.Score != B.Score then 
			--		   UPDATE SET  A.Score = B.Score , A.modify=getdate()-- ���뺯�游 �����ϰ� ����

			WHEN not MATCHED THEN 
		  			          INSERT (score,createdt) VALUES(B.Score, getdate());
end


---------------------------------------------- ���ν��� ���ھ� ���� ��������









