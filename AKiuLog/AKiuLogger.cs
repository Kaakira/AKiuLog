using System;

namespace AKiuLog
{

  public enum AkiuLogLevel
  {
    Debugg,
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

    private static AKiuLogger logger ;

    public static string LogRootPath;

    public static AKiuLogger GetCurrentLogger()
    {
      if (logger == null)
      {
        throw new Exception("使用logger类前，需要先执行RegisterAkiuLog方法");
      }
      return logger;
    }

    public static void RegisterAkiuLog(string rootPath)
    {
      LogRootPath = rootPath;
      logger = new AKiuLogger();
    }

    private AKiuLogger() { }

    private void WriteLog()
    {

    }

    public void Debugg(AKiuLogMessage meesage)
    {
      meesage.Level = AkiuLogLevel.Debugg;
    }
  }
}
