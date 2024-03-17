using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Firebase
{
    public interface IFirebaseService
    {
        public Task<string?> UploadFile(IFormFile file);
    }
}
