using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ecommerce.Transversal.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
