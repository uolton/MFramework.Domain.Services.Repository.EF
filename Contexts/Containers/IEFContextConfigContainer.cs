using System;
using System.Collections.Generic;
using MFramework.Common.Specifications;

namespace MFramework.Domain.Services.Repository.EF.Contexts.Containers
{
    public interface IEFContextConfigContainer
    {
        IEnumerable<Type> Enumerate();
        IEnumerable<Type> EnumerateHaving(ISpecification<Type> criteria);
    }

    public interface IEFContextConfigContainer<TSpecification> : IEFContextConfigContainer where TSpecification : ISpecification<Type> { }
}
