# dmuka3.CS.Simple.LogW
 
 This library provides you to write your logs to your log files daily.
 
 ## Nuget
 **Link** : https://www.nuget.org/packages/dmuka3.CS.Simple.LogW
 ```nuget
 Install-Package dmuka3.CS.Simple.LogW
 ```
 
 ## Example 1
 
  We will save our logs as csv format. You can see through open with excel.
 
 ```csharp
 // Schema is fairly easy to be understood.
 // TYPE, DATETIME, TITLE, DESCRIPTION, PROCESS DATA
 // Taking an info log.
var infoTime = LogWriter.AddLine(LogWriter.LogType.Info, "NUnit-Log", "This is a info log.", new
{
    info = new
    {
        data = true
    }
});

 // Taking a warning log.
var warningTime = LogWriter.AddLine(LogWriter.LogType.Warning, "NUnit-Log", "This is a warning log.", new
{
    warning = new
    {
        data = true
    }
});

 // Taking an error log.
var errorTime = LogWriter.AddLine(LogWriter.LogType.Error, "NUnit-Log", "This is a error log.", new
{
    error = new
    {
        data = true
    }
});
 ```

 The output file is;
 
| TYPE    | DATETIME                | TITLE     | DESCRIPTION            | PROCESS DATA              |
|---------|-------------------------|-----------|------------------------|---------------------------|
| Info    | 2019-10-31 19:09:25.309 | NUnit-Log | This is a info log.    | {"info":{"data":true}}    |
| Warning | 2019-10-31 19:09:25.319 | NUnit-Log | This is a warning log. | {"warning":{"data":true}} |
| Error   | 2019-10-31 19:09:25.322 | NUnit-Log | This is a error log.   | {"error":{"data":true}}   |
