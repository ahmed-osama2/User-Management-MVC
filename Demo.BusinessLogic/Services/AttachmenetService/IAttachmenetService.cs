﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachmenetService
{
    public interface IAttachmenetService
    {
        //Upload
        public string? Upload(IFormFile file, string FolderName);
        //Delete
        bool Delete(string filePath);

    }
}
