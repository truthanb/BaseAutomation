using System;
using TechTalk.SpecFlow;

/// <summary>
/// This class is mostly to simplify saving key value pairs from one test to another within a feature.
/// one example of usage in a test:
/// FeatureContextService.SaveValue("MyUser", user);
/// var contextUser = FeatureContextService.GetValue<user>("MyUser"); FeatureContextService.ClearValue("MyUser"); </user>
/// </summary>

namespace AutomationFramework.framework
{
    public static class FeatureContextService
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
            if (FeatureContext.Current.ContainsKey(key))
            {
                FeatureContext.Current[key] = value;
            }
            else
            {
                FeatureContext.Current.Add(key, value);
            }
        }

        public static T GetValue<T>()
        {
            var key = typeof(T).FullName;

            return GetValue<T>(key);
        }

        public static T GetValue<T>(string key)
        {
            if (!FeatureContext.Current.ContainsKey(key))
            {
                return default(T);
            }

            return FeatureContext.Current.Get<T>(key);
        }

        public static void ClearValue<T>()
        {
            var key = typeof(T).FullName;
            ClearValue(key);
        }

        public static void ClearValue(string key)
        {
            if (FeatureContext.Current.ContainsKey(key))
            {
                FeatureContext.Current.Remove(key);
            }
        }
    }
}