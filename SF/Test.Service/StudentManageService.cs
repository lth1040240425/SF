using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SF.Data;
using Test.Models;
using SqlSugar;

namespace Test.Service
{
    public class StudentManageService : IStudentManageService
    {
        public IRepository<Classes, int> ClassesRepository { protected get; set; }
        public IRepository<Students, int> StudentsRepository { protected get; set; }
        public void AddClasses(Classes Cla)
        {
            ClassesRepository.Insert(Cla);
        }

        public void AddStu(Students stu)
        {
            StudentsRepository.Insert(stu);
        }

        public void DeltetClasses(Classes Cla)
        {
            ClassesRepository.Delete(Cla);
        }

        public void DeltetStu(Students stu)
        {
            StudentsRepository.Delete(stu);
        }

        public Classes GetClasses(int classesId)
        {
            return ClassesRepository.GetById(classesId);
        }

        public List<Classes> GetClassesList(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex > 0)
            {
                return ClassesRepository.GetPageList(p => true,
                    new PageModel() {PageIndex = pageIndex, PageSize = pageIndex});
            }
            else
            {
                return ClassesRepository.GetList();
            }
        }

        public Students GetStu(int StuId)
        {
            return StudentsRepository.GetById(StuId);
        }

        public List<Students> GetStuList(int classesId, int pageIndex = 0, int pageSize = 10)
        {
            var page = new PageModel()
            {
                PageIndex=pageIndex,
                PageSize=pageSize
            };
            return StudentsRepository.GetPageList(p => p.ClassesId == classesId, page);
        }

        public void UpdateClasses(Classes Cla)
        {
            ClassesRepository.Update(p =>new Classes {Name=p.Name},q=>q.Id==Cla.Id);
        }

        public void UpdateStu(Students stu)
        {
            StudentsRepository.Update(stu);
        }
    }
}
