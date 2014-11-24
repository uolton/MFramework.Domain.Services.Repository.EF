using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.EF.Core.EntityConfigurations;

namespace MFramework.Domain.Services.Repository.EF.EntityConfigurations
{

    public abstract class EFDefaultEntityModelConfigurationBase<TEntity, TConfigurationRule> : EFEntityModelConfiguration<TEntity>
        where TEntity : class
        where TConfigurationRule : class,IEntityConfigurationRule<TEntity>, new()
    {
        /// <summary>
        /// Inzializza la nuova istanza 
        /// </summary>
        public EFDefaultEntityModelConfigurationBase()
        {
            IEntityConfigurationRule<TEntity> cfg = new TConfigurationRule();
            cfg.ToTable(this);
        }

        
    }
    
}
