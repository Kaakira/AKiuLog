using KiuLog.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiuLog.Tests
{
  [TestClass]
  public class TestAkiuLog
  {
    public TestAkiuLog()
    {
    }


    /// <summary>
    /// 写日志
    /// </summary>
    [TestMethod()]
    public void TestLog()
    {

      // 首先需要执行一次注册方法（一般在程序入口处）
      KiuLoggerRegister register = new KiuLoggerRegister();
      register.AddFileStored("../")
        .Catch(ex =>
        {
          throw ex;
        })
        .Register();


      for (int i = 0; i < 50; i++)
      {
        //  创建日志实体类
        KiuLogContent log = new KiuLogContent();
        // （必须）设置日志内容一及日志存储器（泛型指定）
        // PS:日志存储器可实现接口，自定义扩展.
        log.SetColumns(i + "-号" + Thread.CurrentThread.ManagedThreadId.ToString());

        // 获取单例logger对象，写入日志（其实是将实体插入队列）
        KiuLogger.Logger().WriteLog(log);

      }

      // 测试日志
      //KiuLogContent log2 = new KiuLogContent());
      //log2.SetColumns("error");
      //KiuLogger.Logger().WriteLog(c);
    }

  }
}
