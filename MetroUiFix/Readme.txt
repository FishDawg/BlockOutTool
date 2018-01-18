Follow these instructions to allow blocks to float above full-screen Metro UI apps (Modern UI apps).

1. Modify App.manifest with uiAccess="true".
2. Rebuild the project.
3. Create a new folder under under your "Program Files" folder. For example: "C:\Program Files\BlockOutTool". (This requires administrator privileges.)
4. Copy the build output to the new folder.
5. Copy the contents of the "MetroUiFix" folder to the new folder too.
6. Open a command prompt with administrator privileges and go to the new folder.
7. Install the Windows SDK (standalone or part of Visual Studio).
8. Execute the InstallAndSignCertificate.cmd command. (This will create and trust a new certificate and sign BlockOutTool.exe with it.)

[ Note: To run this on a Windows RT computer, instead of steps 5-8 above, follow the instructions in this thread and use the Development 
Tool and Sign Tool in the thread's download section plus install its certificate manually in the LocalMachine Root section: 
https://forum.xda-developers.com/windows-8-rt/rt-development/windows-8-1-rt-jailbreak-exploit-t3226835 ]
