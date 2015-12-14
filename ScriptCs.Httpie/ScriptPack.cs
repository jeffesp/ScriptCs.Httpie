using ScriptCs.Contracts;

namespace ScriptCs.Httpie
{
    public class ScriptPack : IScriptPack
    {
        public void Initialize(IScriptPackSession session)
        {
        }

        public IScriptPackContext GetContext()
        {
            return new Httpie();
        }

        public void Terminate()
        {
        }
    }
}
