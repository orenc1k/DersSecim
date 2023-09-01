using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;


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

        public async Task<IslemSonuc<Subjects>> AddSubject(Subjects cengSubject)
        {
            try
            {
                var checkSubject = await SubjectCheckAsync(cengSubject.SubjectCode);
                if (checkSubject)
                {
                    return new IslemSonuc<Subjects>().Basarisiz("Ders tabloda var");
                }
                await odtuDersSecimDbContext.Subjects.AddAsync(cengSubject);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<Subjects>().Basarili(cengSubject);
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

