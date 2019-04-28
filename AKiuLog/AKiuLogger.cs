using System;

namespace KiuLog
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


    private void WriteLog()
    {

    }


    public void Debugg(KiuLogMessage meesage)
    {
      meesage.Level = AkiuLogLevel.Debugg;
    }



  }
}
