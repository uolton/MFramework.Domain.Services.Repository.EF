using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using MFramework.Common.Specifications;
using MFramework.Domain.Services.Repository.EF.Contexts.Containers;
using MFramework.EF.Core.EntityConfigurations;
using MFramework.Common.Core.Types.Extensions;
using MFramework.Common.Core.Collections.Extensions;
namespace MFramework.Domain.Services.Repository.EF.Contexts
{
 /// <summary>
 /// Identifica negli assembly le classi che costituiscono le entita da mappare
 /// </summary>
    public interface IEFContextConfig
        
    {
        ISpecification<System.Type> EntitySelectionCriteria { get; set; }
        IEnumerable<Type> EntityToMap { get; }
        IEnumerable<IEntityModelConfiguration> EntityModelConfigurations { get; }
        IEFContextConfig Add(IEFContextConfigContainer<IEntityContainerSpecification> container);
        IEFContextConfig Add(IEFContextConfigContainer<IEntityModelConfigContainerSpecification> container);
        /// <summary>
        /// Configura il contesto
        /// </summary>
        /// <param name="ctx"></param>
        void Configure(IEFContext ctx);
        
    }

    public interface IEntityContainerSpecification : ISpecification<Type> { }
    public interface IEntityModelConfigContainerSpecification : ISpecification<Type> { }
    }
    

    

    
