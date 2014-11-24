using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Fasterflect;
using MFramework.Common.Core.Collections.Extensions;
using MFramework.Common.Specifications;
using MFramework.Common.Core.Types.Extensions;
using MFramework.Domain.DomainEntity;
using MFramework.Domain.Services.Repository.EF.Contexts.Containers;
using MFramework.Domain.Services.Repository.EF.EntityConfigurations;
using MFramework.EF.Core.EntityConfigurations;

namespace MFramework.Domain.Services.Repository.EF.Contexts.Locators
{
    /// <summary>
    /// Locator delle entita : vengono identificate tutte lo classi che ereditano de Entity
    /// </summary>
    public class EFContextConfig : IEFContextConfig
    {
        #region Member class variables

        /// <summary>
        /// Classi base da cui derivano le entita da mappare
        /// </summary>

        /// <summary>
        /// Contesto 
        /// </summary>

        /// <summary>
        /// Entita che non devono essere mappate
        /// </summary>
        private List<Type> _typeToIgnore;

        /// <summary>
        /// Criterio di selezione delle entita che devono essere mappate
        /// </summary>
        private ISpecification<Type> _entitySelectionCriteria;

        /// <summary>
        /// Assembli da elaborare per reperire le entita
        /// </summary>
        private EFContextConfigCompositeContainer _entityContainer;

        private EFContextConfigCompositeContainer _entityModelConfigContainers;

        #endregion

        #region Constructors

        /// <summary>
        /// Inizializza una nuova istanza 
        /// </summary>
        public EFContextConfig() : this(new IEFContextConfigContainer[] {})
        {

        }

        public EFContextConfig(IEnumerable<IEFContextConfigContainer> containers) : this(containers, containers)
        {

        }

        public EFContextConfig(IEnumerable<IEFContextConfigContainer> entityContainers,
            IEnumerable<IEFContextConfigContainer> entityModelConfigurationContainers) : this()
        {
            _typeToIgnore = new List<Type>();
            _entitySelectionCriteria = Specification<Type>.None;
            _entityContainer = new EFContextConfigCompositeContainer(entityContainers);
            _entityModelConfigContainers = new EFContextConfigCompositeContainer(entityModelConfigurationContainers);
        }

        /// <summary>
        /// Inizializza una nuova istanza 
        /// </summary>
        /// <param name="entityAssemblies">Assembly che contengono le entita da mappare</param>

        #endregion

        /// <summary>
        /// Criterio di selezione delle classi che rappresentano le entita
        /// </summary>
        public ISpecification<Type> EntitySelectionCriteria
        {
            get { return _entitySelectionCriteria; }
            set { _entitySelectionCriteria = value; }
        }

        public IEnumerable<Type> EntityToMap
        {
            get { return _entityContainer.EnumerateHaving(_entitySelectionCriteria); }
        }


        public IEnumerable<IEntityModelConfiguration> EntityModelConfigurations { get; private set; }

        public IEFContextConfig Add(IEFContextConfigContainer<IEntityContainerSpecification> container)
        {
            _entityContainer.Add(container);
            return this;
        }

        public IEFContextConfig Add(IEFContextConfigContainer<IEntityModelConfigContainerSpecification> container)
        {
            _entityModelConfigContainers.Add(container);
            return this;
        }



        private IEnumerable<Type> EntityModelConfigurationDefinedTypes(IEnumerable<Type> entityToMap)
        {
            Specification<Type> specOnlyEntityToMap = new Specification<Type>(
                type => entityToMap.Any(t => t == EFEntityModelConfigurationHelper.GetEntityConfiguredBy(type)));

            return _entityModelConfigContainers.EnumerateHaving(specOnlyEntityToMap);
        }

        public IEnumerable<IEntityModelConfiguration> EntityModelConfigurationDefined(IEnumerable<Type> entityToMap)
        {
            return
                EntityModelConfigurationDefinedTypes(entityToMap)
                    .Select(t => t.CreateInstance())
                    .Cast<IEntityModelConfiguration>();

        }

        public IEnumerable<IEntityModelConfiguration> EntityModelConfigurationToCreate(IEFContext ctx,
            IEnumerable<Type> entityToMap)
        {

            return
                ctx.CreateDefaultEntityModelConfigurationFor(
                    entityToMap.Except(EntityModelConfigurationDefinedTypes(entityToMap)));
        }

        public IEnumerable<IEntityModelConfiguration> EntityModelConfigurationToRegister(IEFContext ctx,
            IEnumerable<Type> entityList)
        {
            return EntityModelConfigurationToCreate(ctx, entityList).Union(EntityModelConfigurationDefined(entityList));

        }

        public void Configure(IEFContext ctx)
        {

            ctx.RegisterEntityModelConfigurations(
                ctx.SelectEntityModelConfiguration(EntityModelConfigurationToRegister(ctx,
                    ctx.SelectEntityToMap(EntityToRegister)))
                );


        }
    }
}

        
