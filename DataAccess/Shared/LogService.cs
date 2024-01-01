using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Shared
{
    public class LogService
    {
        private readonly string logFilePath;
        string logInfoPath, logErrorPath;

        public LogService()
        {
            logFilePath = "C:\\Reporter_Web_Log".Replace(" ", "");
        }

        void DailyFile()
        {
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            string today = DateTime.Today.ToString("yyyy_MM_dd");
            logInfoPath = $"{logFilePath}\\{today}_Info.txt";
            logErrorPath = $"{logFilePath}\\{today}_Error.txt";
        }

        string EntryInfos()
        {
            return DateTime.Now.ToLocalTime() + " - Method Name : ";
        }

        string GetParameters(params object[] parameters)
        {
            StringBuilder text = new StringBuilder();
            foreach (var item in parameters)
            {
                text.Append(item + " / ");
            }
            return text.ToString();
        }

        /// <summary>
        /// Info Log To File
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns>Return "1" Value</returns>
        public async Task<string> InfoAsync(string methodName, params object[] parameters)
        {
            try
            {
                DailyFile();
                await File.AppendAllTextAsync(logInfoPath, "\n" + EntryInfos() + "(" + methodName + ") " + GetParameters(parameters));
                return "1";
            }
            catch
            {
                return "1";
            }
        }

        /// <summary>
        /// Error Log To File
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns>Return "-1" Value</returns>
        public async Task<string> ErrorAsync(string errorMessage, string methodName, params object[] parameters)
        {
            try
            {
                DailyFile();
                await File.AppendAllTextAsync(logErrorPath, "\n" + EntryInfos() + "(" + methodName + ") " + GetParameters(parameters) + " Error Message: " + errorMessage + "\n");
                return "-1";
            }
            catch
            {
                return "-1";
            }
        }
    }
}
