using KiuLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKiuLog
{


  public interface IAKiuLogWriter
  {

  }



  /// <summary>
  /// 基础日志实体类，自动记录日期时间戳
  /// </summary>
  public abstract class AKiuLogMessage
  {


    //AKiuLogMessage message;
    //public AKiuLogMessage(AKiuLogMessage message)
    //{
    //  this.message = message;
    //}

    public Type LoggerType { get; protected set; }
    /// <summary>
    /// 获取到日志记录时时间
    /// </summary>
    public DateTime Date { get; protected set; }
    public AkiuLogLevel Level { get; set; }
    /// <summary>
    /// 日志“列”，因为用换行符分割了，所以是竖着的列
    /// </summary>
    protected List<string> Columns { get; set; } = new List<string>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    public void SetColumns<T>(params string[] columns)
    {
      this.LoggerType = typeof(T);
      if (Columns.Count > 0)
      {
        this.Columns.Clear();
      }
      this.Columns.AddRange(columns);

    }

    /// <summary>
    /// 子类重写时，自定义日志列
    /// </summary>
    /// <returns></returns>
    public abstract string[] ConvertColumns();



    /// <summary>
    /// 日志内容
    /// </summary>
    /// <returns></returns>
    public string LogContent()
    {
      // 日志记录时间
      this.Date = DateTime.Now;
      string content = this.Date.ToString("yyyy-MM-dd hh:mm:ss") + Environment.NewLine;
      this.Columns.AddRange(ConvertColumns());
      return content + string.Join(Environment.NewLine, this.Columns);
    }
  }


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
