
namespace FazEngine2D.Extentions
{
    using FazEngine2D.Classes;
    using FazEngine2D.Classes.Addons;
    public static class ObjectExt
    {
        public static GameObject ConvertToGameObject(this object obj, string name)
        {
            return new GameObject(name, obj, Core.EngineInstance.Windows[0]);
        }
        public static void Destroy(this Script script, GameObject gameObject)
        {
            gameObject.Dispose();
        }
    }
}
