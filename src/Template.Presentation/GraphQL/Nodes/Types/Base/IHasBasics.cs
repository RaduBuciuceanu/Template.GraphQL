﻿using System;
using GraphQL.Types;

namespace Template.Presentation.GraphQL.Nodes.Types.Base
{
    public interface IHasBasics
    {
        string Name { get; }

        string Description { get; }

        Type Type { get; }

        object Resolve(ResolveFieldContext context);
    }
}
