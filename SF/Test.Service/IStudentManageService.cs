using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Dependency;
using Test.Models;

namespace Test.Service
{
    public interface IStudentManageService: IScopeDependency
    {
        void AddStu(Students stu);
        void UpdateStu(Students stu);
        void DeltetStu(Students stu);
        List<Students> GetStuList(int classesId,int pageIndex=0,int pageSize=10);
        Students GetStu(int StuId);

        void AddClasses(Classes Cla);
        void UpdateClasses(Classes Cla);
        void DeltetClasses(Classes Cla);
        List<Classes> GetClassesList(int pageIndex = 0, int pageSize = 10);
        Classes GetClasses(int classesId);
    }
}
