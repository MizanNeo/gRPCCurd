using GraphQL.Types;
using GraphQLCore.Mutation;
using GraphQLCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCore.Schemas
{
    public class DepartmentSchema: Schema
    {
        public DepartmentSchema(IServiceProvider serviceProvider):base(serviceProvider) 
        {
            Query=serviceProvider.GetRequiredService<DepartmentQuery>();
            Mutation=serviceProvider.GetRequiredService<DepartmentMutation>();
        }
    }
}
