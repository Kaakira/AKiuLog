using System;
using System.IO;
﻿using AKiuLog;
using System.Collections.Generic;
using System.Threading;

namespace AKiuLog
{
  /// <summary>
  /// 队列日志
  /// </summary>
  public class AKiuLogger
  {
    /// <summary>
    /// 日志文件目录
    /// </summary>
    public static string LogRootPath;
    public static object _lock = new object();
    private static Queue<AKiuLogMessage> logQueue = new Queue<AKiuLogMessage>();
    private static ManualResetEvent signal = new ManualResetEvent(false);

    private static AKiuLogger logger
    {
      get
      {
        return logger;
      }
      set
      {
        if (logger != null)
          throw new Exception("logger只允许被初始化一次");
        else
          logger = value;
      }
    }

    private AKiuLogger() { }


    public static AKiuLogger GetCurrentLogger()
    {
      if (logger == null)
        throw new Exception("使用logger类前，需要先执行RegisterAkiuLog()以初始化");
      else
        return logger;
    }

    /// <summary>
    /// 开启线程，注册日志队列“事件”
    /// </summary>
    /// <param name="rootPath"></param>
    public static void RegisterAkiuLog(string rootPath)
    {
      // 初始化日志类
      LogRootPath = rootPath;
      logger = new AKiuLogger();
      // 创建日志文件夹
      if (!Directory.Exists(rootPath + "\\Log\\Debug"))
        Directory.CreateDirectory(rootPath);
      if (!Directory.Exists(rootPath + "\\Log\\Info"))
        Directory.CreateDirectory(rootPath);
      if (!Directory.Exists(rootPath + "\\Log\\Error"))
        Directory.CreateDirectory(rootPath);
      if (!Directory.Exists(rootPath + "\\Log\\Warning"))
        Directory.CreateDirectory(rootPath);

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

    private void WriteLog()
    {

    }

    public void Error(Exception ex)
    {
      AKiuLogErrorMessage err = new AKiuLogErrorMessage(ex);
      string errMsg = err.ToString();
    }

    public void Debug(AKiuLogMessage meesage)
    {
      meesage.Level = AkiuLogLevel.Debug;
    }

  }
}
