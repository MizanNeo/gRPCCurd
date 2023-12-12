using GraphQL.Types;
using GraphQLParser.AST;
using NeoSOFT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCore.ObjectType
{
    public class Department : ObjectGraphType<DepartMaster>
    {
        public Department() {
            Field(x => x.Id);
            Field(x => x.DepartName);
            Field(x => x.IsActive);
        }
    }
}
