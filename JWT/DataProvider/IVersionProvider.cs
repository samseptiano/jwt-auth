using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;


namespace JWT.DataProvider
{
    public interface IVersionProvider
    {
        Task <IEnumerable<Models.Version>> GetVersion();
    }
}
