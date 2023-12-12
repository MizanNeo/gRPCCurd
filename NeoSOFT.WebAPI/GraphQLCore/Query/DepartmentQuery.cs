using GraphQL;
using GraphQL.Types;
using GraphQLCore.ObjectType;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCore.Query
{
    public class DepartmentQuery:ObjectGraphType
    {
        public DepartmentQuery(IDepartmentService departmentService) {
            Field<ListGraphType<Department>>(
               Name="departments",
               resolve: x => departmentService.GetAll()
                );

            Field<Department>(
                Name="department",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => departmentService.GetById(context.GetArgument<int>("id"))
           );
        }
    }
}
