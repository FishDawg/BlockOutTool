"%WindowsSdkDir%\bin\x64\makecert.exe" -r -pe -n "CN=BlockOut Test Certificate" -ss My BlockOutTest.cer
"%WindowsSdkDir%\bin\x64\certmgr.exe" -add BlockOutTest.cer -s -r LocalMachine Root
"%WindowsSdkDir%\bin\x64\signtool.exe" sign /v /s My /n "BlockOut Test Certificate" /t http://timestamp.comodoca.com/authenticode BlockOutTool.exe
