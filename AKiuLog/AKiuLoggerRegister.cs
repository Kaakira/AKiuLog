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
  /// 队列日志注册器
  /// </summary>
  public class AKiuLoggerRegister
  {

    private static string LogRootPath;
    /// <summary>
    /// 日志message队列
    /// </summary>
    private static Queue<AKiuLogMessage> logQueue = new Queue<AKiuLogMessage>();
    /// <summary>
    /// 线程阻断信号
    /// </summary>
    private static ManualResetEvent signal = new ManualResetEvent(false);
    /// <summary>
    /// logger日志记录器字典
    /// </summary>
    private static Dictionary<Type,IAKiuLogger> loggers = new Dictionary<Type, IAKiuLogger>();


    /// <summary>
    /// 添加日志记录器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public AKiuLoggerRegister AddSignleLogger<T>() where T : IAKiuLogger, new()
    {

      loggers.Add(typeof(T), new T());
      return this;
    }



    /// <summary>
    /// 开启线程，注册日志队列“事件”
    /// </summary>
    /// <param name="rootPath"></param>
    public void RegisterAkiuLog(string rootPath)
    {
      LogRootPath = rootPath;
      // 添加默认logger记录器
      loggers.Add(typeof(AKiuLogger), new AKiuLogger());

      // 启用线程
      Thread log_threa = new Thread(QueueHandleThread);
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
        // 接收到信号，重置信号
        signal.Reset();
        if (logQueue.Count > 0)
        {
          AKiuLogMessage message ;
          // 记录日志，
          while ((message = logQueue.Dequeue()) != null)
          {
            // 日志内容
            // string content = message.ToString();
            // TODO: 日志记录器记录日志（不同的日志记录器，效果不同，这样开发者可以通过实现不同的日志记录器，自定义记录日志的格式、方式，比如插入到数据库中，而非写入文件），但是问题是，日志记录器，在这里怎样获取比较好.
            // 已解决
            IAKiuLogger logger = loggers[message.LoggerType];
            // TODO: 实现logger类
            logger.SaveLog(message);
          }
        }
      }
    }




  }
}
