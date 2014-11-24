using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Domain.Services.Repository.EF.EntityConfigurations.Rules;

namespace MFramework.Domain.Services.Repository.EF.EntityConfigurations
{
    public class EFDefaultEntityModelConfiguration<TEntity> : EFDefaultEntityModelConfigurationBase<TEntity, EFDefaultEntityConfigurationRules<TEntity>>
        where TEntity : class
    {
    }
}
