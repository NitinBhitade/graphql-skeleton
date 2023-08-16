using Domain;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization
{
    public class RolePayloadBase :Payload
    {
        protected RolePayloadBase(Role role)
        {
            Role = role;
        }

        protected RolePayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Role? Role { get; }
    }
}
