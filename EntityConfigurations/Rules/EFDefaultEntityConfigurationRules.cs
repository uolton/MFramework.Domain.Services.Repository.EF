using System.Data.Entity.ModelConfiguration;
using MFramework.Common.Core.Extensions;

namespace MFramework.Domain.Services.Repository.EF.EntityConfigurations.Rules
{
    /// <summary>
    /// Configurazione di default per le EntityConfigurationRules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFDefaultEntityConfigurationRules<T>:IEntityConfigurationRule<T>
        where T:class
    {

        public virtual string TableName
        {
            get { return string.Empty; }
        }

        public void ToTable(EntityTypeConfiguration<T> cfg)
        {
            (TableName == string.Empty).WhenTrue(() => cfg.ToTable(TableName));

        }
    }
}
