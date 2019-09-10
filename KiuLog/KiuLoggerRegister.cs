using KiuLog;
using KiuLog.Message;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KiuLog
{

  public enum KiuLogLevel
  {
    Debug,
    Info,
    Error,
    Warning
  }


  /// <summary>
  /// 队列日志注册器
  /// </summary>
  public class KiuLoggerRegister
  {

    /// <summary>
    /// 线程阻断信号
    /// </summary>
    private static ManualResetEvent signal = new ManualResetEvent(false);

    /// <summary>
    /// logger日志记录器
    /// </summary>
    private Dictionary<string, IKiuLogStored> logStoreds = new Dictionary<string, IKiuLogStored>();
    private static KiuLogger logger = null;

    private Action<Exception> catchException;


    /// <summary>
    /// 添加默认文件日志记录器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public KiuLoggerRegister AddFileStored<T>(string rootPath) where T : IKiuLogContent
    {
      logStoreds.Add(typeof(T).FullName, new KiuLogStoredFile(rootPath));
      return this;
    }


    // 添加默认logger记录器


    /// <summary>
    /// 添加日志记录器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public KiuLoggerRegister AddLogStored<T>() where T : IKiuLogStored, new()
    {
      logStoreds.Add(typeof(T), new T());
      return this;
    }




    public KiuLoggerRegister Catch(Action<Exception> ex)
    {
      this.catchException = ex;
      return this;
    }


    /// <summary>
    /// 注册日志队列“事件”，开启线程
    /// </summary>
    /// <param name="rootPath"></param>
    public KiuLoggerRegister Register()
    {
      if (logStoreds.Count == 0)
      {
        throw new Exception("至少添加一个日志记录器，ps:可使用AddFileStored默认记录器");
      }
      if (this.catchException == null)
      {
        throw new Exception("日志队列线程异常处理方法不能为null");
      }
      logger = KiuLogger.Logger();

      // 启用线程
      Thread thread = new Thread(QueueHandleThread);
      thread.Start();
      return this;
    }




    /// <summary>
    /// 队列更新，有新日志等待记录
    /// </summary>
    public static void LogSet()
    {
      signal.Set();
    }


    /// <summary>
    /// 队列线程处理
    /// </summary>
    private void QueueHandleThread()
    {

      // 死循环，线程一直保持运行
      KiuLogContent message = null;
      while (true)
      {
        // 等待信号
        signal.WaitOne();
        // 接收到信号，重置信号
        signal.Reset();
        // 记录日志，
        while (logger.QueueLog.Count > 0)
        {
          try
          {
            message = logger.QueueLog.Dequeue();
            logStoreds[message.LoggerType].SaveLog(message);
          }
          catch (Exception ex)
          {
            this.catchException(ex);
          }
        }
      }
    }



  }
}
