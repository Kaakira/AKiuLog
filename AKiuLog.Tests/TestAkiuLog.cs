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
    }


    /// <summary>
    /// д��־
    /// </summary>
    [TestMethod()]
    public void TestLog()
    {

      // ������Ҫִ��һ��ע�᷽����һ���ڳ�����ڴ���
      AKiuLoggerRegister register = new AKiuLoggerRegister();
      register.RegisterAkiuLog(@"D:\Git_Space\AKiuLog\AKiuLog.Tests\Log");


      for (int i = 0; i < 50; i++)
      {
        //  ������־ʵ����
        AKiuLogMessage log = new  AKiuLogMessage();
        // �����룩������־����һ����־�洢��������ָ����
        // PS:��־�洢����ʵ�ֽӿڣ��Զ�����չ.
        log.SetColumns<AKiuLogSaveFile>(i + "-��" + Thread.CurrentThread.ManagedThreadId.ToString());

        // ��ȡ����logger����д����־����ʵ�ǽ�ʵ�������У�
        AKiuLogger.Logger().WriteLog(log);

      }

      // ������־
      AKiuLogMessage log2 = new  AKiuLogErrorMessage(new System.Exception("�����쳣"));
      log2.SetColumns<AKiuLogSaveFile>("error");
      AKiuLogger.Logger().WriteLog(log2);
    }

  }
}
