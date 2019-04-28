using System;
using System.Collections.Generic;
using System.Text;

namespace KiuLog
{

  /// <summary>
  /// 单例工厂
  /// </summary>
  /// <returns></returns>
  public class AKiuLogFactory
  {
    private static AKiuLogger logger = new AKiuLogger();


    public AKiuLogger GetCurrentLogger()
    {
      return logger;
    }





  }
}
