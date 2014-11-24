using System;
using System.Collections.Generic;
using System.Linq;
using MFramework.Common.Core.Collections.Extensions;
using MFramework.Common.Specifications;

namespace MFramework.Domain.Services.Repository.EF.Contexts.Containers
{
    public class EFContextConfigCompositeContainer : IEFContextConfigContainer
            
    {
        private List<IEFContextConfigContainer> _containers;

        public EFContextConfigCompositeContainer()
            : this(new IEFContextConfigContainer [] { })
        {
        }
        public EFContextConfigCompositeContainer(IEnumerable<IEFContextConfigContainer> containers)
        {
            _containers = new List<IEFContextConfigContainer>(containers);
        }

        public EFContextConfigCompositeContainer Add(IEFContextConfigContainer container)
        {
            _containers.Add(container);
            return this;
        }
        public IEnumerable<Type> Enumerate()
        {

            return _containers.Select(c => c.Enumerate()).Flatten();
        }

        public IEnumerable<Type> EnumerateHaving(ISpecification<Type> criteria)
        {
            return _containers.Select(c => c.EnumerateHaving(criteria)).Flatten(); ;
        }
    }
}
