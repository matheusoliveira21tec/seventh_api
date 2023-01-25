using System;
namespace Seventh_api.Services;
public sealed class Singleton
{
    private static volatile Singleton instance;
    private static object syncRoot = new Object();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Singleton();
                }
            }

            return instance;
        }
    }
    /// <summary>
    /// Propriedade para o Status de operação
    /// </summary>
    public string Status { get; set; } = "not running";
}
