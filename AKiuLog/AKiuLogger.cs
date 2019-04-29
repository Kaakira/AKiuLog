using System;
using System.Collections.Generic;
using System.Text;

namespace AKiuLog
{
  /// <summary>
  /// 日志记录器
  /// 
  /// </summary>
  public interface IAKiuLogger
  {

    // TODO: 子类实现：记录日志的逻辑（写入文件/写入数据库）
    void SaveLog(AKiuLogMessage message);



  }

  /// <summary>
  /// 默认日志记录器
  /// </summary>
  public class AKiuLogger : IAKiuLogger
  {

    // TODO: 子类实现：记录日志的逻辑（写入文件/写入数据库）
    public void SaveLog(AKiuLogMessage message)
    {
      Console.WriteLine(message.LogContent());
    }



  }
}
