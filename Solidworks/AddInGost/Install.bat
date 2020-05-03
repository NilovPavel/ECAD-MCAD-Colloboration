if %processor_architecture%==AMD64 goto x64
  C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe "D:\ELSY-PDM\EPDM_LIBRARY\EPDM_SolidWorks\EPDM_AddInELSY\AddInGost.dll" /codebase
  REG IMPORT D:\ELSY-PDM\EPDM_LIBRARY\EPDM_SolidWorks\EPDM_AddInELSY\Add_addin_reg.reg
 @pause
 :x64
  C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe "D:\ELSY-PDM\EPDM_LIBRARY\EPDM_SolidWorks\EPDM_AddInELSY\AddInGost.dll" /codebase
  REG IMPORT D:\ELSY-PDM\EPDM_LIBRARY\EPDM_SolidWorks\EPDM_AddInELSY\Add_addin_reg.reg
 @pause