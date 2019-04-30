using AKiuLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AKiuLog.Message
{



  /// <summary>
  /// 基础日志实体类，自动记录日期时间戳
  /// </summary>
  public abstract class AKiuLogMessage
  {


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

    /// <summary>
    /// 按照年月日分级存储日志文件
    /// 例如：2019/05/Debug_01.log
    /// </summary>
    /// <returns></returns>
    public string LogFilePath()
    {
      return Path.Combine(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), Level.ToString() + "_" + DateTime.Now.Day + ".log");
    }


  }


}
