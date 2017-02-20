# gcci
<img src="https://raw.githubusercontent.com/tobychui/gcci/master/interface.png"></img>
<p>
The GCCI is a simple gcc.exe compiler user interface design for Windows 7 or above,
running with .NET framework 4.5
</p>
# Steps to compile with this UI
<p>
1. Install <a href="http://www.mingw.org/">MinGW</a> with all baisc modules included.<br>
2. Select the default install path of the gcc.exe (Which is C:\MinGW )<br>
3. Download the exe file under <strike>\GCCI\bin\Debug\GCCI.exe</strike> versions/[latest_version]/gcc_quick_compile.exe<br>
4. Run the exe, click the button on the bottom right hand corner to select the gcc.exe path if you are not installing the MinGW in the default location.<br>
5. Pull your C programs (.c) into the form windows to compile.<br>
6. Find all the compiled exe files under the base dir of the GCCI.exe program.
</p>
#Update Version 2.1<br>
<p>
GCCI now support NPPExec Direct Compile. Here is an example for the launching command:<br>
<br>
npp_save<br>
"$(CURRENT_DIRECTORY)\GCCI.exe" $(FULL_CURRENT_PATH)<br>
<br>
The 1st variable is the location of your GCCI.exe, the 2nd variable is the path of the document you are currently editing.<br>
You can copy the GCCI.exe to your working everytime you need it. Or set the 1st path to a static path for simple compile.<br>
The compiled .exe will be output in the same directory of the GCCI.exe location.
</p>
#User Interface Preview
<p>Debug Interface</p><br>
<img src="https://raw.githubusercontent.com/tobychui/gcci/master/screenshots/2017-02-20_10-19-14.png"></img><br><br>
<p>Compile using Notepad ++ with NppExec Plugin</p><br>
<img src="https://raw.githubusercontent.com/tobychui/gcci/master/screenshots/2017-02-20_10-19-28.png"></img><br>
