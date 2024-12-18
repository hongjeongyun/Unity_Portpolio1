# 개요
REST API / OAuth 2.0 을 이용한 PSN Account ID 를 취득하는 기능을 구현.  
플레이스테이션 게임기와 리모트플레이 를 실현하는 오픈소스 프로그램 Chiaki 에서  
기기등록을 위해 PSN 으로부터 Account ID 를 취득하여야 합니다.
C# 으로 동작을 하는 프로그램을 작성하는것이 첫번째 목표였습니다.  
본 공고의 요건에 적합하도록 유니티에서 동작하도록 구현하는 포트폴리오를 제작하였습니다.
         
# 문제
OAuth 2.0 에 대한 사전지식이 없었습니다.
# 해결 
CLIENT_ID, CLIENT_SCRET 키워드를 근거로 자료를 검색하여 OAuth 2.0 의 동작 원리를 학습하였습니다.  
OAuth2.0 프로세스를 따라 작업합니다.  
1.API KEY(CLIENT_ID, CLIENT_SECRET) 가 공개되어 있으므로 authorization code 발급을 사용합니다.  
링크를 통해 외부 브라우저를 오픈하여 Login URL 의 쿼리 파라미터에 CLIENT_ID 를 전달하여 로그인 합니다.
https://github.com/hongjeongyun/Unity_Portpolio1/blob/da78979ce6f74c0db298a283658225d18e74d314/Assets/Script/LoginLink.cs#L8
https://github.com/hongjeongyun/Unity_Portpolio1/blob/5966d8d1f76eabd808993cb4c14812bfb8e412eb/Assets/Script/PSNIDBASE64.cs#L16
로그인후 redirect uri 로 발행된 authorization code 값이 쿼리 파라미터 "code" 로 전달됩니다.  
유니티상에서 Inputfield 에 redirect uri 를 붙여넣고 버튼을 누르면 취득과정이 시작됩니다. https://github.com/hongjeongyun/Unity_Portpolio1/blob/fea26d109b62c9f8513eb2ab3d153555a5f32a29/Assets/Script/Main.cs#L21

2.발행된 authorization code 을 사용하여 Access token 을 발급합니다.  
입력된 redirect uri 의 쿼리로부터 authorization code 값을 저장합니다.  
HTTP 요청의 기본인증 헤더를 지정하고 (Basic), 보낼 본문(인증방식, authorization code, redirect uri)을 생성합니다.  
Access token 을 발급 하기 위해 TOKEN_URL 으로 서버측에 POST 요청을 보냅니다. 
https://github.com/hongjeongyun/Unity_Portpolio1/blob/bc72f9b9d6dccb1387dc4517154336b07bc503a8/Assets/Script/PSNIDBASE64.cs#L33 
결과를 tokeninfo 에 저장합니다.

3.Account ID 취득  
토큰에 연결된 계정정보를 취득하기 위해 URL에 tokeninfo 를 포함하여 서버측에 GET 요청을 보냅니다.  https://github.com/hongjeongyun/Unity_Portpolio1/blob/6e9ca6562332434e06130ee2c00bcf62ae98b9a3/Assets/Script/PSNIDBASE64.cs#L43
결과로 받은 계정정보에서 user_id 항목을 찾아 Base64 로 변환하여 최종출력 합니다.
