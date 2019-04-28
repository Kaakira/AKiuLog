using System;
using System.Collections.Generic;
using System.Text;

namespace KiuLog
{
  public class KiuLogMessage
  {

    public DateTime Date { get; set; } = DateTime.Now;
    public AkiuLogLevel Level { get; set; }
    public string Content { get; set; }

    public override string ToString()
    {
      this.Content = this.Date.ToString("yyyy-MM-dd hh:mm:ss\n") + this.Content;
      return this.Content + "\n";
    }
  }


  public class KiuLogErrorMessage : KiuLogMessage
  {
    public KiuLogErrorMessage(Exception ex)
    {
      this.Level = AkiuLogLevel.Error;
      this.Ex = ex;
    }
    public Exception Ex { get; set; }
    public override string ToString()
    {
      this.Content = $"错误信息：{Ex.Message}\n错误类型：{Ex.Source}\n堆栈信息：{Ex.StackTrace}\n" + this.Content;
      return base.ToString();
    }
  }

}
