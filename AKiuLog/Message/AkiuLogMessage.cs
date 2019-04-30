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
  public class AKiuLogMessage
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
    protected string[] Columns { get; set; }

    private string rootPath;


    /// <summary>
    /// 按照年月日分级存储日志文件
    /// 例如：2019/05/Debug_01.log
    /// </summary>
    /// <returns></returns>
    public string LogFilePath
    {
      get
      {
        if (string.IsNullOrEmpty(rootPath) || !Path.IsPathRooted(rootPath))
        {
          throw new InvalidDataException("日志实体异常：日志文件根路径非法，路径：" + rootPath == null ? "null" : rootPath);
        }
        return Path.Combine(rootPath, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), Level.ToString() + "_" + DateTime.Now.ToString("dd") + ".log");
      }
    }
    public void SetFileRootPath(string rootPath)
    {
      this.rootPath = rootPath;
    }


    /// <summary>
    /// 按“列”设置日志内容，
    /// </summary>
    /// <param name="columns"></param>
    public void SetColumns<T>(params string[] columns) where T : IAKiuLogSave
    {
      this.LoggerType = typeof(T);
      this.Columns = columns;
    }

    /// <summary>
    /// 子类重写时，自定义日志列
    /// </summary>
    /// <returns></returns>
    public virtual string[] ConvertColumns()
    {
      return null;
    }



    /// <summary>
    /// 日志内容
    /// </summary>
    /// <returns></returns>
    public virtual string LogContent()
    {
      // 日志记录时间
      this.Date = DateTime.Now;
      string content = this.Date.ToString("yyyy-MM-dd hh:mm:ss") +">>>>>>>>>>>>>>"+ Environment.NewLine;
      string[] customColumns = ConvertColumns();
      if (customColumns != null && customColumns.Length > 0)
      {
        content += string.Join(Environment.NewLine, ConvertColumns()) + Environment.NewLine;
      }
      return content + string.Join(Environment.NewLine, this.Columns);
    }



  }


}
