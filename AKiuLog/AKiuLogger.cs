using AKiuLog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KiuLog
{

  public enum AkiuLogLevel
  {
    Debug,
    Info,
    Error,
    Warning
  }


  /// <summary>
  /// 队列日志
  /// </summary>
  public class AKiuLogger
  {

    private static string LogRootPath;
    private static Queue<AKiuLogMessage> logQueue = new Queue<AKiuLogMessage>();
    private static AKiuLogger logger ;
    private static ManualResetEvent signal = new ManualResetEvent(false);



    public static AKiuLogger GetCurrentLogger()
    {
      if (logger == null)
      {
        throw new Exception("使用logger类前，需要先执行RegisterAkiuLog方法");
      }
      return logger;
    }


    /// <summary>
    /// 开启线程，注册日志队列“事件”
    /// </summary>
    /// <param name="rootPath"></param>
    public static void RegisterAkiuLog(string rootPath)
    {
      LogRootPath = rootPath;
      logger = new AKiuLogger();
      // 启用线程
      Thread log_threa = new Thread(logger.QueueHandleThread);
      log_threa.Start();

    }


    /// <summary>
    /// 队列线程处理
    /// </summary>
    private void QueueHandleThread()
    {
      // 死循环，线程一直保持运行
      while (true)
      {
        // 等待信号
        signal.WaitOne();
        if (logQueue.Count > 0)
        {
          AKiuLogMessage message ;
          while ((message = logQueue.Dequeue()) != null)
          {
            this.WriteLog(/*message*/);
          }
        }


      }
    }


    private AKiuLogger()
    {

    }

    private void WriteLog()
    {

    }


    public void Debugg(AKiuLogMessage meesage)
    {
      meesage.Level = AkiuLogLevel.Debug;
    }



  }
}
