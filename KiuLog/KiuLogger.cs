using KiuLog.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KiuLog
{
  /// <summary>
  /// 队列日志实例
  /// </summary>
  public class KiuLogger
  {
    private static KiuLogger instance;
    private static object lockObject = new object();


    /// <summary>
    /// 日志message队列
    /// </summary>
    public Queue<KiuLogContent> QueueLog { get; set; }

    private KiuLogger()
    {
    }

    public static KiuLogger Logger()
    {

      if (instance == null)
      {
        // 锁定，双重判断，处理多线程时可能会出现的错误
        lock (lockObject)
        {
          if (instance == null)
          {
            instance = new KiuLogger();
            instance.QueueLog = new Queue<KiuLogContent>();
          }
        }
      }
      return instance;
    }

    public void WriteLog(string message, KiuLogLevel level = KiuLogLevel.Info)
    {
      if (message != null)
      {
        this.QueueLog.Enqueue(new KiuLogContent { Level = level, Columns = new string[] { message } });
        KiuLoggerRegister.LogSet();
      }
    }

    public void WriteLog(KiuLogContent message)
    {
      if (message != null)
      {
        this.QueueLog.Enqueue(message);
        KiuLoggerRegister.LogSet();
      }

    }


  }
}
