using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;


namespace ODTUDersSecim.Services
{
    public class DepartmentService
    {

        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public DepartmentService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<Departments>> GetDepartmentsAsync()
        {

            var departments = await odtuDersSecimDbContext.Departments.ToListAsync();
            return departments;
        }

        public async Task<Departments?> GetDepartment(int deptCode)
        {
            var department = await odtuDersSecimDbContext.Departments.SingleOrDefaultAsync(x => x.DeptCode == deptCode);
            return department;
        }

        public async Task<IslemSonuc<Departments>> DeleteDepartment(int deptCode)
        {
            try
            {
                var deletedDepartment = await GetDepartment(deptCode);
                if (deletedDepartment != null)
                {
                    odtuDersSecimDbContext.Departments.Remove(deletedDepartment);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<Departments>().Basarili(deletedDepartment); ;
                }
                return new IslemSonuc<Departments>().Basarisiz("Silinecek Departman Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<Departments>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

        public async Task<IslemSonuc<Departments>> AddDepartment(Departments department)
        {
            try
            {
                var addedDepartment = await DepartmentCheckAsync(department.DeptCode);
                if (addedDepartment)
                {
                    return new IslemSonuc<Departments>().Basarisiz("Departman tabloda var");
                }
                await odtuDersSecimDbContext.Departments.AddAsync(department);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<Departments>().Basarili(department);
                return islemSonuc;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<Departments>().Basarisiz("Ekleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<IslemSonuc<Departments>> UpdateDepartment(Departments department)
        {
            try
            {
                var updatedDept = await GetDepartment(department.DeptCode);
                if (updatedDept != null)
                {
                    updatedDept.DeptFullName = department.DeptFullName;
                    updatedDept.DeptShortName = department.DeptShortName;

                    odtuDersSecimDbContext.Departments.Update(updatedDept);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    var islemSonuc = new IslemSonuc<Departments>().Basarili(updatedDept);
                    return islemSonuc;
                }
                return new IslemSonuc<Departments>().Basarisiz("Güncellenecek Departman Tabloda Bulunamadı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<Departments>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<bool> DepartmentCheckAsync(int deptCode)
        {
            var checkDepartment = await odtuDersSecimDbContext.Departments.AnyAsync(q => q.DeptCode == deptCode);

            return checkDepartment;
        }



    }
}

