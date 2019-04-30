using AKiuLog.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AKiuLog.Tests
{
  [TestClass]
  public class TestAkiuLog
  {
    public TestAkiuLog()
    {

      AKiuLoggerRegister register = new AKiuLoggerRegister();
      register.RegisterAkiuLog(@"D:\Git_Space\AKiuLog\AKiuLog.Tests\Log");
    }


    /// <summary>
    /// д��־
    /// </summary>
    [TestMethod()]
    public void TestLog()
    {
      for (int i = 0; i < 50; i++)
      {
        AKiuLogMessage log = new  AKiuLogMessage();
        log.SetColumns<AKiuLogSaveFile>(i + "-��" + Thread.CurrentThread.ManagedThreadId.ToString());

        AKiuLogger.Logger().WriteLog(log);

        // ������־
        AKiuLogMessage log2 = new  AKiuLogErrorMessage(new System.Exception("�����쳣"));
        log2.SetColumns<AKiuLogSaveFile>("error");
        AKiuLogger.Logger().WriteLog(log2);

      }
    }

  }
}
