using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;


namespace JWT.DataProvider
{
    //Jika menggunakan Store Procedure harus menggunakan Interface
    public interface IUserListProvider
    {
        Task<IEnumerable<PA_Org>> GetEmpOrg(string empNIK, string tahun);
        Task<IEnumerable<UserListPA>> GetUserList(string empNIK, string tahun);
        Task<IEnumerable<UserList>> GetUserListPJ(string empNIK);
        Task<IEnumerable<UserList>> GetUserListPJOne(string empNIK);

    }
}
