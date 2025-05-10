using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryWebApp.Response
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T User { get; set; }
    }
}