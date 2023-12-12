using GraphQL;
using GraphQL.Types;
using GraphQLCore.ObjectType;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Application.Services;
using NeoSOFT.Domain.Models;
using NeoSOFT.Infrastrcture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCore.Mutation
{
    public class DepartmentInput : InputObjectGraphType
    {
        public DepartmentInput()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("departName");
            Field<BooleanGraphType>("isActive");
        }
    }
    public class DepartmentMutation:ObjectGraphType
    {
        public DepartmentMutation(IDepartmentService departmentService)
        {
            Field<Department>("create",
                arguments: new QueryArguments { new QueryArgument<DepartmentInput> { Name="department" } },
                resolve:x=>departmentService.Create(x.GetArgument<DepartMaster>("department"))
                );
        }
    }
}
