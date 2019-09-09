using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWT.DataProvider
{
    public interface IGeneralDataProvider
    {
        //FileStreamResult GetImageFile(int ID);
        //FileModel GetFile(int ID);
        Task<IEnumerable<FileModel>> GetFile(int ID);
    }
}
