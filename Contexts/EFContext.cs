using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using MFramework.Common.Core.Types.Extensions;
using MFramework.Common.Specifications;
using MFramework.Domain.DomainEntity;
using MFramework.Domain.Services.Repository.EF.EntityConfigurations;
using MFramework.EF.Core;
using MFramework.EF.Core.Builder;
using MFramework.EF.Core.Builder.Rules;
using MFramework.Common.Core.Collections.Extensions;
using MFramework.EF.Core.EntityConfigurations;

namespace MFramework.Domain.Services.Repository.EF.Contexts
{
    /// <summary>
    /// Context base per i repository
    /// </summary>
    public abstract class EFContext : ExtendedDbContext,IEFContext

    {
        
        private  DbModelBuilder _modelBuilder;
        protected EFContext(string connectionString,IEnumerable<Assembly> entityAssemblies ) : base(connectionString)
        {
           
        }

        ///<summary>
        /// Chiamata durante la generazione del modello per la registrazione delle regole
        /// </summary>
        protected abstract void AddBuilderRules();
        
        
        public override int SaveChanges()
        {
            IEnumerable<Type> typeAdded =
                ChangeTracker.Entries().Where(e => e.State == EntityState.Added).Select(e => e.GetType());
            IEnumerable<Tuple<IEnumerable<PropertyInfo>,object>> pl = typeAdded.Where(t => t.GetProperties().Any(p => p.GetCustomAttributes().Any(a => a.GetType() == typeof(DatabaseGeneratedAttribute)))).Select( t=> new Tuple<IEnumerable<PropertyInfo>, object>(new List<PropertyInfo>(t.GetProperties().Where(p => p.GetCustomAttributes().Any(a => a.GetType() == typeof(DatabaseGeneratedAttribute)))),t)); 
            pl.ForEach(t => t.Item1.ForEach(p => p.SetValue(Guid.NewGuid(),t) ));
        }
        private readonly List<IBuilderRule> _rules = new List<IBuilderRule>();
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            IEFContextConfig cfg = GetConfig();
            base.OnModelCreating(modelBuilder);
            
            // Crea le EntityTypeConfig di default per le entita che non ne hanno associata una
            CreateAndRegisterEntityToModel(cfg,modelBuilder);
            // Registra le EntityTypeConfig presenti negli Assembly
            RegisterEntityModelConfiguration(cfg, modelBuilder);
            ProcessRules(modelBuilder);
        }

        
        
        /// <summary>
        /// Add one Rule
        /// </summary>
        /// <param name="convention">Convention to add</param>
        protected void AddRule(IBuilderRule rule)
        {
            _rules.Add(rule);
        }
        /// <summary>
        /// Elabora le BuilderRules registrate 
        /// </summary>
        protected virtual void ProcessRules(DbModelBuilder modelBuilder)
        {
            _rules.ForEach(r => r.Build(modelBuilder));
        }
        /// <summary>
        /// Richiamata dalla class di configurazione per modificare le regole generate di defualt
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        protected virtual List<IBuilderRule> OnGettingStandarRules(List<IBuilderRule> rules)
        {
            return rules;
        }

        /// <summary>
        /// Chiamata prima di generate le EntityConfiguration per eventuali modifiche alla
        /// lista delle entita che devono essere generate
        /// </summary>
        /// <param name="entityTypeList">Entita di cui generare le Entity</param>
        protected virtual IEnumerable<Type> OnGeneratingEntityConfiguration(IEnumerable<Type> entityTypeList)
        {
            return entityTypeList;
        }
        /// <summary>
        /// Viene Richiamta 
        /// </summary>
        /// <param name="entityModelConfigList"></param>
        /// <returns></returns>
        protected virtual IEnumerable<IEntityModelConfiguration> OnGeneratedEntityModelConfiguration(IEnumerable<IEntityModelConfiguration> entityModelConfigList)
        {
            return entityModelConfigList;
        }
        
        
        public virtual IEnumerable<Type> SelectEntityToMap(IEnumerable<Type> entityList)
        {
            return entityList;
        }

        public IEnumerable<IEntityModelConfiguration> SelectEntityModelConfiguration(IEnumerable<IEntityModelConfiguration> entityModelConfigurations)
        {
            return entityModelConfigurations;
        }

        public IEnumerable<IEntityModelConfiguration> CreateDefaultEntityModelConfigurationFor(IEnumerable<Type> entityList)
        {
            return entityList.Select(EFEntityModelConfigurationHelper.GenerateDefault);
        }
        /// <summary>
        /// Registra le EntityTypeConfiguration rilevate dalla configurazione
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        public void RegisterEntityModelConfigurations(IEnumerable<IEntityModelConfiguration> entityModelConfigurations)
        {
            entityModelConfigurations.ForEach( e => e.Register(_modelBuilder));
        }
    }
}
