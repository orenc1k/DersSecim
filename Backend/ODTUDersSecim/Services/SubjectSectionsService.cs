using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;


namespace ODTUDersSecim.Services
{
    public class SubjectSectionsService 
    {

        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public SubjectSectionsService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<SubjectSections>> GetAllSubjectSections()
        {

            var subjectAllSections = await odtuDersSecimDbContext.SubjectSections.ToListAsync();
            return subjectAllSections;
        }

        public async Task<List<SubjectSections>> GetSubjectSections(int subjectCode)
        {
            var subjectSections = await odtuDersSecimDbContext.SubjectSections.Where(x => x.SubjectCode == subjectCode).ToListAsync();
            return subjectSections;
        }

        public async Task<SubjectSections?> GetSubjectSection(int sectionId)
        {
            var subjectSection = await odtuDersSecimDbContext.SubjectSections.SingleOrDefaultAsync(x => x.SectionId == sectionId);
            return subjectSection;
        }

        public async Task<IslemSonuc<SubjectSections>> DeleteSubjectSection(int sectionId)
        {
            try
            {
                var subjectSection = await GetSubjectSection(sectionId);
                if (subjectSection != null)
                {
                    odtuDersSecimDbContext.SubjectSections.Remove(subjectSection);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<SubjectSections>().Basarili(subjectSection); ;
                }
                return new IslemSonuc<SubjectSections>().Basarisiz("Silinecek Section Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<SubjectSections>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

        public async Task<IslemSonuc<SubjectSections>> AddSubjectSection(SubjectSections subjectSection)
        {
            try
            {
                var checkSubject = await SubjectSectionCheckAsync(subjectSection);
                if (checkSubject)
                {
                    return new IslemSonuc<SubjectSections>().Basarisiz("Section tabloda var");
                }
                await odtuDersSecimDbContext.SubjectSections.AddAsync(subjectSection);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<SubjectSections>().Basarili(subjectSection);
                return islemSonuc;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error while saving changes: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    Console.WriteLine("Inner Exception Stack Trace: " + ex.InnerException.StackTrace);
                }

                return new IslemSonuc<SubjectSections>().Basarisiz("Ekleme İşleminde Hata Oluştu!");
            }

        }

        public async Task<IslemSonuc<SubjectSections>> UpdateSubjectSection(SubjectSections subjectSection)
        {
            try
            {
                var updatedSubjectSection = await GetSubjectSection(subjectSection.SectionId);
                if (updatedSubjectSection != null)
                {
                    updatedSubjectSection.SubjectCode = subjectSection.SubjectCode;
                    updatedSubjectSection.SectionCode = subjectSection.SectionCode;
                    updatedSubjectSection.GivenDept = subjectSection.GivenDept;
                    updatedSubjectSection.StartChar = subjectSection.StartChar;
                    updatedSubjectSection.EndChar = subjectSection.EndChar;
                    updatedSubjectSection.MinCumGpa = subjectSection.MinCumGpa;
                    updatedSubjectSection.MaxCumGpa = subjectSection.MaxCumGpa;
                    updatedSubjectSection.MinYear = subjectSection.MinYear;
                    updatedSubjectSection.MaxYear = subjectSection.MaxYear;
                    updatedSubjectSection.StartGrade = subjectSection.StartGrade;
                    updatedSubjectSection.EndGrade = subjectSection.EndGrade;

                    odtuDersSecimDbContext.SubjectSections.Update(updatedSubjectSection);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    var islemSonuc = new IslemSonuc<SubjectSections>().Basarili(updatedSubjectSection);
                    return islemSonuc;
                }
                return new IslemSonuc<SubjectSections>().Basarisiz("Güncellenecek Ders Tabloda Bulunamadı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<SubjectSections>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<bool> SubjectSectionCheckAsync(SubjectSections subjectSection)
        {
            var checkSubject = await odtuDersSecimDbContext.SubjectSections.Where(x => x.SubjectCode == subjectSection.SubjectCode).AnyAsync(x =>
            (
                x.SectionCode == subjectSection.SectionCode &&
                x.StartChar == subjectSection.StartChar &&
                x.EndChar == subjectSection.EndChar &&
                x.StartGrade == subjectSection.StartGrade &&
                x.EndGrade == subjectSection.EndGrade &&
                x.MinCumGpa == subjectSection.MinCumGpa &&
                x.MaxCumGpa == subjectSection.MaxCumGpa &&
                x.MinYear == subjectSection.MinYear &&
                x.MaxYear == subjectSection.MaxYear &&
                x.GivenDept == subjectSection.GivenDept
            
               ) );

            return checkSubject;
        }



    }
}

