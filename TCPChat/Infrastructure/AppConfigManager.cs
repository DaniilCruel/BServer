using System.Configuration;
using System.Linq;

namespace TCPChat.Infrastructure
{

    // Работа с App.config

    public static class AppConfigManager
    {

        // Проверяет наличие параметров в App.config и добавляет при необходимости

        /// <param name="parameters">Параметры, которые должны проверяться</param>
        public static void CreateConfigParameters(params string[] parameters)
        {
            foreach (string str in parameters)
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains(str))
                {
                    Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    currentConfig.AppSettings.Settings.Add(str, "");
                    currentConfig.Save(ConfigurationSaveMode.Full);
                    ConfigurationManager.RefreshSection(str);
                }
            }
            ConfigurationManager.RefreshSection("appSettings");
        }


        // Обновляет значение параметра в App.config

        /// <param name="parameterKey">Название параметра</param>
        /// <param name="parameterValue">Значение параметра</param>
        public static void UpdateConfigParameter(string parameterKey, string parameterValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[parameterKey].Value = parameterValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }


        // Получает значение параметра из App.config по его названию

        /// <param name="parameterKey">Название параметра</param>
        /// <returns>Значение параметра</returns>
        public static string ReadConfigParameter(string parameterKey) =>
            ConfigurationManager.AppSettings[parameterKey];
    }
}
