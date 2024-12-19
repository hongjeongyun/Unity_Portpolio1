# 개요
REST API / OAuth 2.0 을 이용한 PSN Account ID 를 취득하는 기능을 구현.
플레이스테이션 게임기와 리모트플레이 를 실현하는 오픈소스 프로그램 Chiaki 에서  
기기등록을 위해 Base64 타입 의 Account ID 가 사용됩니다.  
처음에는 C# 으로 동작을 하는 프로그램을 작성하는것이 목표였습니다.  
본 공고의 요건에 적합하도록 유니티에서 동작하도록 구현하는 포트폴리오를 제작하였습니다.
         
# 문제
OAuth 2.0 에 대한 사전지식이 없었습니다.
# 해결 
CLIENT_ID, CLIENT_SCRET 키워드를 근거로 자료를 검색하여 OAuth 2.0 의 동작 원리, C# 의 OAuth2.0 API 에 대해 학습하였습니다.  
OAuth2.0 프로세스를 따라 작업합니다.  
1.API KEY(CLIENT_ID, CLIENT_SECRET) 가 사전에 오픈소스에 공개되어 있기때문에 authorization code 발급을 사용  
링크를 통해 외부 브라우저를 오픈하여 Login URL 의 쿼리 파라미터로 CLIENT_ID 를 전달하여 로그인 합니다. [이동](https://github.com/hongjeongyun/Unity_Portpolio1/commit/200e4590a92ea992e92efbd79a0cd0bb65bae3b0#diff-59608a8a40d909c99dc7d68c516a10b8a36370ff370b3b84309121cc95831302R8)
로그인후 redirect uri 로 발행된 authorization code 값이 "code" 쿼리 파라미터로 전달됩니다. Uri의 쿼리로부터 "code" 값을 저장합니다.

2.발행된 authorization code 을 사용하여 access token 을 발급합니다.
HTTP 요청의 기본인증 헤더를 지정, 보낼 본문을 생성합니다.
Access token 을 발급 하기 위해 TOKEN_URL 으로 "code" 정보를 POST 합니다. 
https://github.com/hongjeongyun/Unity_Portpolio1/commit/ac32e088cf2427becd9566d32dbd8a14683a8859#diff-32b174d1270b0b5600e4017a939636bdbdf5a4c35b79aa37ebdd477791ca86b8R33
결과를 tokeninfo 에 저장합니다.
3.Account ID 취득
TOKEN_URL/tokeninfo 으로 GET 요청을 보냅니다. 
https://github.com/hongjeongyun/Unity_Portpolio1/commit/ae4910075821b5ebdb4a8c48408f49ed5893adc9#diff-32b174d1270b0b5600e4017a939636bdbdf5a4c35b79aa37ebdd477791ca86b8R43
요청결과를 Dictionary 변환하고 user_id 항목을 찾아 Base64 로 변환합니다.
