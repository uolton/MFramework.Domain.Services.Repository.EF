using System;
using System.Collections.Generic;
using System.Reflection;
using Fasterflect;
using MFramework.Common.Core.Types.Extensions;
using MFramework.Common.DesignByContracts;
using MFramework.Domain.DomainEntity;
using MFramework.EF.Core.EntityConfigurations;


namespace MFramework.Domain.Services.Repository.EF.EntityConfigurations
{
    public class EFEntityModelConfiguration<TEntity>:EntityModelConfiguration<TEntity>
        where TEntity:class
        
    {
         
    }

    internal static class EFEntityModelConfigurationHelper
    {
        public static IEntityModelConfiguration GenerateDefault(Type entityType)
        {
            return ((IEntityModelConfiguration )typeof(EFDefaultEntityModelConfiguration<>).CloseGenericWith(entityType).CreateInstance());
        }

        public static Type GetEntityConfiguredBy(Type configurationType)

        {
            Guard.Against<ArgumentException>(configurationType.Implement(typeof(EFEntityModelConfiguration<>)),"Wrong parameter");
            return configurationType.BaseTypeUntil(t => t == typeof (EFEntityModelConfiguration<>)).GetGenericArguments()[0];
        }
    }
}
