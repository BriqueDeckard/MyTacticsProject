namespace Assets.Scripts.src.Common.Contracts
{
    using UnityEngine;

    /// <summary>
    /// The singleton design pattern restricts the instantiation of a class to a single instance.
    /// This is done in order to provide coordinated access to a certain resource, throughout an entire software system.
    /// Through this design pattern, the singleton class ensures that it’s only instantiated once, and can provide easy access to the single instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        /// <summary>
        /// Returns the instance of the singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                instance ??= (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                      " is needed in the scene, but there is none.");
                }
                return instance;
            }
        }
    }
}