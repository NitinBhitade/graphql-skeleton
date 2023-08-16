using Domain;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization
{
    public class AddRolePayload : RolePayloadBase
    {
        public AddRolePayload(Role role)
        : base(role)
        {
        }
        public AddRolePayload(UserError error)
      : base(new[] { error })
        {
        }
    }
}
