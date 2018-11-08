using System;

namespace SaharaLib
{
    public abstract class BaseSingleton<T> where T : class
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => CreateInstanceOfT());

        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// 
        /// Creates an instance of the derived singleton type using the derived
        /// types default constructor. Use true for the nonpublic parameter so 
        /// Activator.CreateInstance matches with the private singleton constructor
        /// 
        /// </summary>
        /// <returns></returns>
        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
