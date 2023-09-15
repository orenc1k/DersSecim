using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;
using ODTUDersSecim.DTOs;


namespace ODTUDersSecim.Services
{
    public class SubjectsService : IDersSecim<Subjects>
    {

        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public SubjectsService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<Subjects>> GetSubjects()
        {

            var subjects = await odtuDersSecimDbContext.Subjects.ToListAsync();
            return subjects;
        }

        public async Task<Subjects?> GetSubject(int subjectCode)
        {
            var subject = await odtuDersSecimDbContext.Subjects.SingleOrDefaultAsync(x => x.SubjectCode == subjectCode);
            return subject;
        }

        public async Task<bool> AddSubjectDeptCode()
        {
            try
            {
                var allSubjects = await GetSubjects();

                foreach(var subject in allSubjects)
                {
                    string deptCodeString = subject.SubjectCode.ToString().Substring(0, 3);
                    int deptCode = int.Parse(deptCodeString);

                    var updateSubject = new Subjects()
                    {
                        SubjectCode = subject.SubjectCode,
                        SubjectName = subject.SubjectName,
                        SubjectCredit = subject.SubjectCredit,
                        EctsCredit = subject.EctsCredit,
                        SubjectLevel = subject.SubjectLevel,
                        SubjectType = subject.SubjectType,
                        DeptCode = deptCode,
                        Departments = null
                    };
                    await UpdateSubject(updateSubject);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;

            }
        }

        public async Task<IslemSonuc<Subjects>> DeleteSubject(int subjectCode)
        {
            try
            {
                var subject = await GetSubject(subjectCode);
                if (subject != null)
                {
                    odtuDersSecimDbContext.Subjects.Remove(subject);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<Subjects>().Basarili(subject); ;
                }
                return new IslemSonuc<Subjects>().Basarisiz("Silinecek Ders Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<Subjects>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

        public async Task<IslemSonuc<Subjects>> AddSubject(Subjects subject)
        {
            try
            {
                var checkSubject = await SubjectCheckAsync(subject.SubjectCode);
                if (checkSubject)
                {
                    await UpdateSubject(subject);
                    return new IslemSonuc<Subjects>().Basarili();
                }
                await odtuDersSecimDbContext.Subjects.AddAsync(subject);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<Subjects>().Basarili(subject);
                return islemSonuc;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<Subjects>().Basarisiz("Ekleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<IslemSonuc<Subjects>> UpdateSubject(Subjects subject)
        {
            try
            {
                var updatedSubject = await GetSubject(subject.SubjectCode);
                if (updatedSubject != null)
                {
                    updatedSubject.SubjectCode = subject.SubjectCode;
                    updatedSubject.SubjectCredit = subject.SubjectCredit;
                    updatedSubject.SubjectLevel = subject.SubjectLevel;
                    updatedSubject.SubjectName = subject.SubjectName;
                    updatedSubject.SubjectType = subject.SubjectType;
                    updatedSubject.DeptCode = subject.DeptCode;
                    updatedSubject.Departments = subject.Departments;

                    odtuDersSecimDbContext.Subjects.Update(updatedSubject);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    var islemSonuc = new IslemSonuc<Subjects>().Basarili(updatedSubject);
                    return islemSonuc;
                }
                return new IslemSonuc<Subjects>().Basarisiz("Güncellenecek Ders Tabloda Bulunamadı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<Subjects>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<bool> SubjectCheckAsync(int subjectCode)
        {
            var checkSubject = await odtuDersSecimDbContext.Subjects.AnyAsync(q => q.SubjectCode == subjectCode);

            return checkSubject;
        }



    }
}

