using AKiuLog.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AKiuLog
{
  public class AKiuLogger
  {
    private static AKiuLogger instance;
    private static object lockObject = new object();

    public string LogFilePath;

    public string GetFullPath()
    {
      if (Path.IsPathRooted(LogFilePath))
      {
        return LogFilePath;
      }
      return Path.GetFullPath(LogFilePath);
    }

    /// <summary>
    /// 日志message队列
    /// </summary>
    public Queue<AKiuLogMessage> QueueLog { get; set; }

    private AKiuLogger()
    {
    }

    public static AKiuLogger Logger()
    {

      if (instance == null)
      {
        // 锁定，双重判断，处理多线程时可能会出现的错误
        lock (lockObject)
        {
          if (instance == null)
          {
            instance = new AKiuLogger();
            instance.QueueLog = new Queue<AKiuLogMessage>();
          }
        }
      }
      return instance;
    }

    public void WriteLog(AKiuLogMessage message)
    {
      message.SetFileRootPath(this.LogFilePath);
      if (message != null)
      {
        this.QueueLog.Enqueue(message);
        AKiuLoggerRegister.LogSet();
      }

    }


  }
}
