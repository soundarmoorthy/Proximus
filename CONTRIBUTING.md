Hi there, thanks for taking time to contriute. The prerequisites to setup the environment is 
* .NET Core 3.0 or later
* Visual Studio 2019 or later (Community edition is also fine)
* Download and instlall Open Street Maps java application
* Run OSM in the maching, before running this project.


#Running Tests
The tests are based on MSTest and can be ran within the IDE. But to run code coverage you need to do the following. 

1. Make sure dotnet tools path is part of environment path variable. If not please run the following command. Usually the 
path to the dotnet tools in Mac/Linux is "<userdir>/.dotnet/tools".

>cat << \EOF >> ~/.zshrc
># Add .NET Core SDK tools
>export PATH="$PATH:/Users/<username>/.dotnet/tools"
>EOF

2. We use coverlet for code coverage. To setup coverlet run the following command 
 >dotnet tool install --global coverlet.console

Once the installation is done run the following command to get a overall report.
 >dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

Test run for Proximus/Proximus.Tests/bin/Debug/netcoreapp3.0/Proximus.Tests.dll(.NETCoreApp,Version=v3.0)
Microsoft (R) Test Execution Command Line Tool Version 16.3.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 19
     Passed: 19
 Total time: 0.8562 Seconds

Calculating coverage result...
  Generating report './Proximus/Proximus.Tests/coverage.cobertura.xml'

+----------+--------+--------+--------+
| Module   | Line   | Branch | Method |
+----------+--------+--------+--------+
| Proximus | 34.36% | 33.54% | 43.08% |
+----------+--------+--------+--------+

+---------+--------+--------+--------+
|         | Line   | Branch | Method |
+---------+--------+--------+--------+
| Total   | 34.36% | 33.54% | 43.08% |
+---------+--------+--------+--------+
| Average | 34.36% | 33.54% | 43.08% |
+---------+--------+--------+--------+

3. If you want a more detailed report of the code coverage install report generator as follows
> dotnet tool install -g dotnet-reportgenerator-globaltool

and run the following command 
> 
