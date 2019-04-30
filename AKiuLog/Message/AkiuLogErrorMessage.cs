using AKiuLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKiuLog.Message
{


  /// <summary>
  /// 错误日志实体类，自动记录引发的异常信息
  /// </summary>
  public class AKiuLogErrorMessage : AKiuLogMessage
  {

    public AKiuLogErrorMessage(Exception ex)
    {
      this.Level = AkiuLogLevel.Error;
      this.Ex = ex;
    }

    public Exception Ex { get; set; }


    public override string[] ConvertColumns()
    {
      return new string[] {
        "错误信息：" + Ex.Message,
        "错误类型：" + Ex.Source,
        "堆栈信息：" + Ex.StackTrace
      };

    }
  }
}
