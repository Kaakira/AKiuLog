using AKiuLog;
using AKiuLog.Message;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AKiuLog
{

  public enum AkiuLogLevel
  {
    Debug,
    Info,
    Error,
    Warning
  }


  /// <summary>
  /// 
  /// </summary>


  /// <summary>
  /// 队列日志注册器
  /// </summary>
  public class AKiuLoggerRegister
  {

    /// <summary>
    /// 线程阻断信号
    /// </summary>
    private  ManualResetEvent signal = new ManualResetEvent(false);
    /// <summary>
    /// logger日志记录器字典
    /// </summary>
    private  Dictionary<Type,IAKiuLogSave> loggers = new Dictionary<Type, IAKiuLogSave>();
    private static AKiuLogger logger;


    private AKiuLoggerRegister()
    {

    }
    /// <summary>
    /// 添加日志记录器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public AKiuLoggerRegister AddSignleLogger<T>() where T : IAKiuLogSave, new()
    {

      loggers.Add(typeof(T), new T());
      return this;
    }


    /// <summary>
    /// 注册日志队列“事件”，开启线程
    /// </summary>
    /// <param name="rootPath"></param>
    public void RegisterAkiuLog(string rootPath)
    {



      logger = AKiuLogger.Logger();
      logger.LogFilePath = rootPath;
      // 添加默认logger记录器
      loggers.Add(typeof(AKiuLogFileSave), new AKiuLogFileSave());

      // 启用线程
      Thread thread = new Thread(QueueHandleThread);
      thread.Start();


      //return instance;
    }


    /// <summary>
    /// 队列线程处理
    /// </summary>
    private void QueueHandleThread()
    {

      // 死循环，线程一直保持运行
      AKiuLogMessage message = null;
      while (true)
      {
        // 等待信号
        signal.WaitOne();
        // 接收到信号，重置信号
        signal.Reset();
        if (logger.QueueLog.Count < 1)
        {
          continue;
        }
        // 记录日志，
        while ((message = logger.QueueLog.Dequeue()) != null)
        {
          try
          {
            // 日志内容  string content = message.ToString();
            IAKiuLogSave logger = loggers[message.LoggerType];
            // TODO: 实现logger类
            logger.SaveLog(message);

          }
          catch (Exception ex)
          {
            // 使用默认记录器记录队列执行异常
            AKiuLogErrorMessage  error = new AKiuLogErrorMessage(ex);
            error.SetColumns<AKiuLogFileSave>("日志队列线程出现错误", message == null ? "" : "日志内容：" + message.LogContent());
            loggers[error.LoggerType].SaveLog(error);
          }
        }
      }
    }



  }
}
