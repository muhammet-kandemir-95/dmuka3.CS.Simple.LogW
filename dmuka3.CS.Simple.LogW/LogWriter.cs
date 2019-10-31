using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dmuka3.CS.Simple.LogW
{
    /// <summary>
    /// You can get some logs about your current project using this class.
    /// </summary>
    public static class LogWriter
    {
        #region Variables
        /// <summary>
        /// Only for locking.
        /// </summary>
        private static readonly object lockObj = new object();

        /// <summary>
        /// Log types.
        /// </summary>
        public enum LogType
        {
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// First of file data.
        /// </summary>
        private static byte[] __firstOfFileData = null;
        #endregion

        #region Constructors
        static LogWriter()
        {
            List<byte> fofd = new List<byte>();
            fofd.AddRange(Encoding.UTF8.GetPreamble());
            fofd.AddRange(Encoding.UTF8.GetBytes(@"""TYPE"",""DATETIME"",""TITLE"",""DESCRIPTION"",""PROCESS DATA"""));
            __firstOfFileData = fofd.ToArray();
            fofd.Clear();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a line to log file.
        /// </summary>
        /// <param name="type">Log's type.</param>
        /// <param name="title">Log's title.</param>
        /// <param name="description">Log's description.</param>
        /// <param name="processData">Log's process data. This will be saved as json.</param>
        /// <param name="directory">File path of log.</param>
        public static DateTime AddLine(LogType type, string title, string description, object processData, string directory = "")
        {
            var dt = DateTime.UtcNow;
            lock (lockObj)
            {
                string filePath = Path.Combine(directory, dt.ToString("yyyy-MM-dd") + ".csv");
                if (File.Exists(filePath) == false)
                    File.WriteAllBytes(filePath, __firstOfFileData);

                File.AppendAllText(filePath,
                    Environment.NewLine +
                    $@"""{type}"",""{dt.ToString("yyyy-MM-dd HH:mm:ss.fff")}"",""{stringToCSVCell(title)}"",""{stringToCSVCell(description)}"",""{stringToCSVCell(JsonConvert.SerializeObject(processData))}""",
                    Encoding.UTF8);
            }
            return dt;
        }

        /// <summary>
        /// Clear uncessary character from the text by CSV format.
        /// </summary>
        /// <param name="text">Cell's data.</param>
        /// <returns></returns>
        private static string stringToCSVCell(string text)
        {
            return text.Replace("\"", "\"\"").Replace("\r", "").Replace("\n", "");
        }
        #endregion
    }
}
