# Async Logger
 A simple asynchronous logging engine in C#.
 
## How to use
There are a couple ways to use this logger.
* Adding the .dll to project references. (fastest)
  * Download the latest .dll on [releases](https://github.com/Zolmex/async-logger/releases).
  * Right-click on 'Dependencies' (or 'References' if you're working on .NET Framework) of the project you want to add the logger to, click 'Add Project Reference', go to 'Browse', then 'Browse...'(on the bottom), find and select the .dll file and click 'Accept', then 'Ok' and you're done.
  * Last, right-click the project and go to 'Manage NuGet packages'. Click on 'Browse' and install 'Newtonsoft.Json'.
* If you wish to have the source code at hand.
  * Add the 'AsyncLogger' folder to the solution and add the project to it as well. [How to add an exisisting project to a Solution](https://docs.microsoft.com/en-us/sql/ssms/solution/add-an-existing-project-to-a-solution?view=sql-server-ver15).
  * After adding the project to the solution, right-click the project and go to 'Manage NuGet packages'. Click on 'Browse' and install 'Newtonsoft.Json'.
  * _**Note:** If you want other projects to use the logger, right-click on 'Dependencies' (or 'References' if you're working on .NET Framework) of the project you want, click 'Add Project Reference' and select the AsyncLogger project._
> For the simplest of uses, you can look at [RedisBackup](https://github.com/Zolmex/redis-backup) for guidance.

## logConfig.json
```json
[
  {
    "LoggerName": "DefaultLogger",
    "LogDir": "/logs/.../",
    "LevelConfigs": [
      {
        "BackgroundColor": 0,
        "ForegroundColor": 7
      },
      [...]
    ]
  }
]
```
* LoggerName: Name of the logger. Loggers that have this name will have this configuration.
  * _**Note:** Logger names that don't have configuration use the DefaultLogger configuration._
* LogDir: Directory for the log files and folders to be created.
* LevelConfigs: Configuration for each log level.
  * BackgroundColor: Color for the back of the log message.
  * ForegroundColor: Color for the text of the log message.
