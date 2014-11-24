using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.EF.Core.EntityConfigurations;

namespace MFramework.Domain.Services.Repository.EF.Contexts
{
    /// <summary>
    /// Contesto 
    /// </summary>
    public  interface IEFContext
    {
        
        /// <summary>
        /// Seleziona dall'elenco le entita che dovranno essere sottoposte al mapping
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IEnumerable<Type> SelectEntityToMap(IEnumerable<Type> entityList);
      /// <summary>
      /// Seleziona le EntityTypeConfigurationda regsitrare per il processoo di mapping
      /// </summary>
      /// <param name="entityModelConfigurations"></param>
      /// <returns></returns>
        IEnumerable<IEntityModelConfiguration> SelectEntityModelConfiguration(IEnumerable<IEntityModelConfiguration> entityModelConfigurations);
        /// <summary>
        /// Crea l'entitytypeconfiguration di default delle entita
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IEnumerable<IEntityModelConfiguration> CreateDefaultEntityModelConfigurationFor(IEnumerable<Type> entityList);
        /// <summary>
        /// Registra le entityTypeConfiguration per il processo di mapping
        /// </summary>
        /// <param name="entityModelConfigurations"></param>
        void RegisterEntityModelConfigurations(IEnumerable<IEntityModelConfiguration> entityModelConfigurations);

    }
}
}
