**# 日志队列**

一个简单的日志队列记录框架



# 使用方式

```c#
// 首先需要执行一次注册方法（一般在程序入口处）
AKiuLoggerRegister register = new AKiuLoggerRegister();
register.RegisterAkiuLog(@"D:\Git_Space\AKiuLog\AKiuLog.Tests\Log");

for (int i = 0; i < 50; i++)
{
    //  创建日志实体类
    AKiuLogMessage log = new  AKiuLogMessage();
    // （必须）设置日志内容一及日志存储器（泛型指定）
    // PS:日志存储器可实现接口，自定义扩展.
    log.SetColumns<AKiuLogSaveFile>(i + "-号" + Thread.CurrentThread.ManagedThreadId.ToString());

    // 获取单例logger对象，写入日志（其实是将实体插入队列）
    AKiuLogger.Logger().WriteLog(log);

}
```

