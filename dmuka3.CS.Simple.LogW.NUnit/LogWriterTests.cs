using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace dmuka3.CS.Simple.LogW.NUnit
{
    public class LogWriterTests
    {
        [Test]
        public void ClassicTest()
        {
            var previousLogFilePath = DateTime.UtcNow.ToString("yyyy-MM-dd") + ".csv";
            if (File.Exists(previousLogFilePath))
                File.Delete(previousLogFilePath);

            var infoTime = LogWriter.AddLine(LogWriter.LogType.Info, "NUnit-Log", "This is a info log.", new
            {
                info = new
                {
                    data = true
                }
            });
            var warningTime = LogWriter.AddLine(LogWriter.LogType.Warning, "NUnit-Log", "This is a warning log.", new
            {
                warning = new
                {
                    data = true
                }
            });
            var errorTime = LogWriter.AddLine(LogWriter.LogType.Error, "NUnit-Log", "This is a error log.", new
            {
                error = new
                {
                    data = true
                }
            });

            Assert.AreEqual(
                File.ReadAllText(previousLogFilePath, Encoding.UTF8), 
                $@"""TYPE"",""DATETIME"",""TITLE"",""DESCRIPTION"",""PROCESS DATA""" + Environment.NewLine +
                $@"""Info"",""{infoTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}"",""NUnit-Log"",""This is a info log."",""{{""""info"""":{{""""data"""":true}}}}""" + Environment.NewLine +
                $@"""Warning"",""{warningTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}"",""NUnit-Log"",""This is a warning log."",""{{""""warning"""":{{""""data"""":true}}}}""" + Environment.NewLine +
                $@"""Error"",""{errorTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}"",""NUnit-Log"",""This is a error log."",""{{""""error"""":{{""""data"""":true}}}}""");
        }
    }
}