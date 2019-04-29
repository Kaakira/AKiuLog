using System;

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
  /// 队列日志
  /// </summary>
  public class AKiuLogger
  {

    public static object _lock = new object();

    private static AKiuLogger logger
    {
      get
      {
        return logger;
      }
      set
      {
        if (logger != null)
          new Exception("logger只能被初始化一次");
        else
          logger = value;
      }
    }

    public static string LogRootPath;

    private AKiuLogger() { }

    public static AKiuLogger GetCurrentLogger()
    {
      if (logger == null)
        throw new Exception("使用logger类前，需要先执行RegisterAkiuLog方法");
      else
        return logger;
    }

    public static void RegisterAkiuLog(string rootPath)
    {
      LogRootPath = rootPath;
      logger = new AKiuLogger();
    }

    private void WriteLog()
    {

    }

    public void Debug(AKiuLogMessage meesage)
    {
      meesage.Level = AkiuLogLevel.Debug;
    }
  }
}
