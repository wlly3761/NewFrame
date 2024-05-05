namespace Core.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class DynamicApiAttribute : System.Attribute
{
    /// <summary>
    ///     服务生命周期
    /// </summary>
    public string ServiceLifeCycle { get; set; }
}