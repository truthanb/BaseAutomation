using System;
using TechTalk.SpecFlow;

namespace AutomationFramework.framework
{
    /// <summary>
    /// This class is mostly to simplify saving key value pairs from one test step to another and even across test step classes when needed.
    /// one example of usage in a test:
    /// ScenarioContextService.SaveValue("MyUser", user);
    /// var contextUser = ScenarioContextService.GetValue<user>("MyUser"); ScenarioContextService.ClearValue("MyUser"); </user>
    /// </summary>
    public static class ScenarioContextService
    {
        public static void SaveValue<T>(T value)
        {
            if (value.Equals(default(T)))
            {
                throw new Exception("Value cannot be default value");
            }

            var key = typeof(T).FullName;
            SaveValue(key, value);
        }

        public static void SaveValue<T>(string key, T value)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                ScenarioContext.Current[key] = value;
            }
            else
            {
                ScenarioContext.Current.Add(key, value);
            }
        }

        public static T GetValue<T>()
        {
            var key = typeof(T).FullName;

            return GetValue<T>(key);
        }

        public static T GetValue<T>(string key)
        {
            if (!ScenarioContext.Current.ContainsKey(key))
            {
                throw new ArgumentException("The key does not exist in the scenario context");
            }

            return ScenarioContext.Current.Get<T>(key);
        }
    }
}