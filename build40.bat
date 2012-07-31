@echo off
SET FRAMEWORK_PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319"
SET ASSEMBLY_PATH="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0"
SET SILVERLIGHT_PATH="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0"
SET EXPRESSION_PATH="C:\Program Files (x86)\Microsoft SDKs\Expression\Blend\Silverlight\v4.0\Libraries"
SET PATH=%PATH%;%FRAMEWORK_PATH%;
SET PATH=%PATH%;%ASSEMBLY_PATH%;


if exist output\ReactiveMVVM ( rmdir /s /q output\ReactiveMVVM )
mkdir output\ReactiveMVVM

echo Compiling
msbuild /nologo /verbosity:quiet source/ReactiveMVVM.sln /p:Configuration=Debug /t:Clean
msbuild /nologo /verbosity:quiet source/ReactiveMVVM.sln /p:Configuration=Debug

echo Copying
copy source\ReactiveMVVM\bin\Debug\*.* output\ReactiveMVVM\*.*

echo Merging
if exist output\ReactiveMVVM.Merged ( rmdir /s /q output\ReactiveMVVM.Merged )
mkdir output\ReactiveMVVM.Merged
SET FILES_TO_MERGE=
SET FILES_TO_MERGE=%FILES_TO_MERGE% "source\ReactiveMVVM\Bin\Debug\ReactiveMVVM.dll"
SET FILES_TO_MERGE=%FILES_TO_MERGE% "source\ReactiveMVVM\Bin\Debug\AutoFac.dll"
SET FILES_TO_MERGE=%FILES_TO_MERGE% "source\ReactiveMVVM\Bin\Debug\System.CoreEx.dll"
SET FILES_TO_MERGE=%FILES_TO_MERGE% "source\ReactiveMVVM\Bin\Debug\System.Observable.dll"
SET FILES_TO_MERGE=%FILES_TO_MERGE% "source\ReactiveMVVM\Bin\Debug\System.Reactive.dll"

.\ILMerge.exe /targetplatform:v4,%SILVERLIGHT_PATH% /xmldocs /out:output\ReactiveMVVM.Merged\ReactiveMVVM.dll %FILES_TO_MERGE% /lib:%SILVERLIGHT_PATH%


echo Cleaning
msbuild /nologo /verbosity:quiet source/ReactiveMVVM.sln /p:Configuration=Debug /t:Clean

echo Done
pause