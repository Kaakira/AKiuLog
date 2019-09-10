using KiuLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KiuLog.Message
{

  public interface IKiuLogContent
  {
     string LogContent();
  }


  /// <summary>
  /// 日志实体类，自动记录日期时间戳
  /// 纵列记录日志
  /// </summary>
  public class KiuLogContent : IKiuLogContent
  {
    /// <summary>
    /// 获取到日志记录时时间
    /// </summary>
    public DateTime Date { get; protected set; }

    /// <summary>
    /// 日志级别
    /// </summary>
    public KiuLogLevel Level { get; set; } = KiuLogLevel.Info;

    /// <summary>
    /// 日志“列”，因为用换行符分割了，所以是纵列
    /// </summary>
    public string[] Columns { get; set; }


    public KiuLogContent()
    {
      this.Date = DateTime.Now;
    }

    public void SetColumns(params string[] columns)
    {
      this.Columns = columns;
    }

    /// <summary>
    /// 日志内容
    /// </summary>
    /// <returns></returns>
    public string LogContent()
    {
      string content = this.Date.ToString("yyyy-MM-dd hh:mm:ss") + ">>>>>>>>>>>>>>" + Environment.NewLine;
      return content + string.Join(Environment.NewLine, this.Columns);
    }

  }
}

