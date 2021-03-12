# WPF_Tranning


* ICommand를 이용한 Binding 사용 
* Deverpress 20.7 평가판으로 WPF datagrid 등 사용

<br><br><br>
BaseBallGame_Winform -> WPF로 변경작업 <br>


![image](https://user-images.githubusercontent.com/45617447/110933625-57f23900-8370-11eb-85ce-1b7cf8343ee8.png)
<br>
<사진1>
메인페이지 
<br>

![image](https://user-images.githubusercontent.com/45617447/110934097-eebef580-8370-11eb-815a-3a8076e4b95e.png)
<br>
<사진2>
게임입력 <br>

- 게임을 시작하면 재시작 버튼 활성화
- 3자리를 입력해야 엔터키 활성화 
-  CommandManager.InvalidateRequerySuggested() 를 사용하여 게임시작 카운트 등을 확인하여 버튼 활성화 처리 (버튼 움직임 감지 아님)


