using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MFramework.Common.Specifications;

namespace MFramework.Domain.Services.Repository.EF.Contexts.Containers
{
    public class AssemblyEFContextConfigContainer<TSpecification> : IEFContextConfigContainer<TSpecification>
        where TSpecification : Specification<Type>,new()
    {
        private Assembly _assembly;    
        public AssemblyEFContextConfigContainer(Assembly a)
        {
            _assembly = a;
        }
        public IEnumerable<Type> Enumerate()
        {
            return Enumerate(new TSpecification());
        }

        public IEnumerable<Type> EnumerateHaving(Specification<Type> criteria)
        {
            return Enumerate(new TSpecification().And(criteria));
        }

        public IEnumerable<Type> Enumerate(ISpecification<Type> criteria)
        {
             return _assembly.GetTypes().Where(criteria.IsSatisfiedBy) ;
        }
        
    }
        public static class AssemblyContainer
        {
            public static IEFContextConfigContainer<TSpecification> For<TSpecification>(Assembly a)
                where TSpecification : Specification<Type>, new()
            {
                return (new AssemblyEFContextConfigContainer<TSpecification>(a));
            }

            public static IEFContextConfigContainer<TSpecification> For<TSpecification>(IEnumerable<Assembly> l)
                where TSpecification : Specification<Type>, new()
            {
                return new CompositeContainer<TSpecification>(l.Select(a => new AssemblyEFContextConfigContainer<TSpecification>(a)));
            }

            public static IEFContextConfigContainer<TSpecification> AsContainerFor<TSpecification>(this Assembly @this)
                where TSpecification : Specification<Type>, new()
            {
                return For<TSpecification>(@this);
            }

        }
}
