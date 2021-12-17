using NStandard;

namespace Dawnx.Compress
{
    public partial class ZipStream
    {
        public class UpdateScope : Scope<UpdateScope>
        {
            public readonly ZipStream Model;

            public UpdateScope(ZipStream model)
            {
                Model = model;
                Model.ZipFile.BeginUpdate();
            }

            public override void Disposing()
            {
                Model.ZipFile.CommitUpdate();
            }
        }
    }
}
