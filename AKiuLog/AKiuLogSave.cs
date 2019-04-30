using AKiuLog.FileLog;
using AKiuLog.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AKiuLog
{
  /// <summary>
  /// 日志记录器
  /// </summary>
  public interface IAKiuLogSave
  {

    // TODO: 子类实现：记录日志的逻辑（写入文件/写入数据库）
    void SaveLog(AKiuLogMessage message);



  }




  /// <summary>
  /// 默认文件日志记录器(保存
  /// </summary>
  public class AKiuLogSaveFile : IAKiuLogSave
  {
    public void SaveLog(AKiuLogMessage message)
    {
      string path =Path.Combine(message.LogFilePath);
      using (FileStream logFile = AKiuLogFile.Create(path))
      using (StreamWriter writer = new StreamWriter(logFile))
      {
        writer.WriteLine(message.LogContent());
      }
    }

  }
}
