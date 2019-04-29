using System;
using System.Collections.Generic;
using System.Text;

namespace AKiuLog
{

  /// <summary>
  /// 单例工厂
  /// </summary>
  /// <returns></returns>
  public class AKiuLogFactory
  {
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
    }




  }
}
