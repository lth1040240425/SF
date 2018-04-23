using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SF.Core;
using Test.Models;
using Test.Service;

namespace WebApiTest.Areas.StuManager.Controllers
{
    public class StuManagerController : ApiController
    {
        public IStudentManageService StudentManageService { get; set; }
        public ApiResponse Add(Students stu)
        {
            StudentManageService.AddStu(stu);
            return ApiResponse.CreateSuccess();
        }
    }
}
