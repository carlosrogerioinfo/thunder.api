using System.ComponentModel;

namespace Thunder.Project.Domain.Enums
{
    public enum Status
    {
        [Description("Pendente")]
        Pending = 0,

        [Description("Concluído")]
        Done = 1,

        [Description("Excluído")]
        Deleted = 2

    }


}
