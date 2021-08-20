
namespace FazEngine2D.Extentions
{
    using FazEngine2D.Classes;
    public static class ObjectExt
    {
        public static GameObject ConvertToGameObject(this object obj, string name)
        {
            return new GameObject(name, obj, Core.EngineInstance.Windows[0]);
        }
    }
}
