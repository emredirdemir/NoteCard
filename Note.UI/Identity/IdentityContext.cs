using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Identity
{
    public class IdentityContext:IdentityDbContext<User>
    {
        private IConfiguration _configuration;

        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {
        }
    }
}
