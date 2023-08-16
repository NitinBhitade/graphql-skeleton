using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;

namespace Application.Authorization
{
   
    public record AddRoleInput(Optional<string?> Name);
}
