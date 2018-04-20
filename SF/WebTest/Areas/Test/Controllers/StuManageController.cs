using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SF.Core;
using Test.Models;
using Test.Service;

namespace WebTest.Areas.Test.Controllers
{
    public class StuManageController : ApiController
    {
        public IStudentManageService StudentManageService;
        public ApiResponse AddStu(Students stu)
        {
            StudentManageService.AddStu(stu);
            return ApiResponse.CreateSuccess();
        }
    }
}
