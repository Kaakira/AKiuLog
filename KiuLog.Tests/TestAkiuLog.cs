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
    /// д��־
    /// </summary>
    [TestMethod()]
    public void TestLog()
    {

      // ������Ҫִ��һ��ע�᷽����һ���ڳ�����ڴ���
      KiuLoggerRegister register = new KiuLoggerRegister();
      register.AddFileStored("../")
        .Catch(ex =>
        {
          throw ex;
        })
        .Register();


      for (int i = 0; i < 50; i++)
      {
        //  ������־ʵ����
        KiuLogContent log = new KiuLogContent();
        // �����룩������־����һ����־�洢��������ָ����
        // PS:��־�洢����ʵ�ֽӿڣ��Զ�����չ.
        log.SetColumns(i + "-��" + Thread.CurrentThread.ManagedThreadId.ToString());

        // ��ȡ����logger����д����־����ʵ�ǽ�ʵ�������У�
        KiuLogger.Logger().WriteLog(log);

      }

      // ������־
      //KiuLogContent log2 = new KiuLogContent());
      //log2.SetColumns("error");
      //KiuLogger.Logger().WriteLog(c);
    }

  }
}
