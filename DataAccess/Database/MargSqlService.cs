using DataAccess.Abstract;
using DataAccess.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public class MargSqlService : AbsSqlService
    {
        public MargSqlService(IOptions<SqlConfig> options) : base(options.Value.MargConnectionString)
        {

        }

      
    }
}
