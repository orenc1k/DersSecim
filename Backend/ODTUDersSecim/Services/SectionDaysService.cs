using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using ODTUDersSecim.Helpers;
using ODTUDersSecim.DTOs;

namespace ODTUDersSecim.Services
{
    public class SectionDaysService
    {

        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public SectionDaysService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<SectionDays>> GetAllSubjectSectionDays()
        {

            var allSectionDays = await odtuDersSecimDbContext.SectionDays.ToListAsync();
            return allSectionDays;
        }

        public async Task<List<SectionDays>> GetSubjectsSectionDays(int sectionCode)
        {
            var subjectSectionDays = await odtuDersSecimDbContext.SectionDays.Where(x => x.SectionId == sectionCode).ToListAsync();
            return subjectSectionDays;
        }

        public async Task<SectionDays?> GetSubjectSectionDay(int sectionDaysId)
        {
            var subjectSectionDays = await odtuDersSecimDbContext.SectionDays.SingleOrDefaultAsync(x => x.SectionDaysId == sectionDaysId);
            return subjectSectionDays;
        }

        public async Task<IslemSonuc<SectionDays>> DeleteSubjectSectionDays(int sectionDaysId)
        {
            try
            {
                var subjectSectionDay = await GetSubjectSectionDay(sectionDaysId);
                if (subjectSectionDay != null)
                {
                    odtuDersSecimDbContext.SectionDays.Remove(subjectSectionDay);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<SectionDays>().Basarili(subjectSectionDay); ;
                }
                return new IslemSonuc<SectionDays>().Basarisiz("Silinecek Section Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<SectionDays>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

        public async Task<IslemSonuc<SectionDaysDTO>> AddSubjectSectionDays(SectionDaysDTO subjectSectionDaysDTO)
        {
            try
            {
                var checkSubject = await SubjectSectionDaysCheckAsync(subjectSectionDaysDTO);
                if (checkSubject)
                {
                    return new IslemSonuc<SectionDaysDTO>().Basarisiz("Section tabloda var");
                }
                var sectionDay = new SectionDays
                {
                    Day1 = subjectSectionDaysDTO.Day1,
                    Day2 = subjectSectionDaysDTO.Day2,
                    Day3 = subjectSectionDaysDTO.Day3,
                    Time1 = subjectSectionDaysDTO.Time1,
                    Time2 = subjectSectionDaysDTO.Time2,
                    Time3 = subjectSectionDaysDTO.Time3,
                    SectionId = subjectSectionDaysDTO.SectionId,
                    SubjectCode = subjectSectionDaysDTO.SubjectCode
                };
                await odtuDersSecimDbContext.SectionDays.AddAsync(sectionDay);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<SectionDaysDTO>().Basarili(subjectSectionDaysDTO);
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

                return new IslemSonuc<SectionDaysDTO>().Basarisiz("Ekleme İşleminde Hata Oluştu!");
            }

        }
        public async Task<IslemSonuc<SectionDays>> UpdateSubjectSectionDays(SectionDays subjectSectionDays)
        {
            try
            {
                var updatedSubjectSectionDays = await GetSubjectSectionDay(subjectSectionDays.SectionDaysId);
                if (updatedSubjectSectionDays != null)
                {
                    updatedSubjectSectionDays.Day1 = subjectSectionDays.Day1;
                    updatedSubjectSectionDays.Day2 = subjectSectionDays.Day2;
                    updatedSubjectSectionDays.Day3 = subjectSectionDays.Day3;
                    updatedSubjectSectionDays.Time1 = subjectSectionDays.Time1;
                    updatedSubjectSectionDays.Time2 = subjectSectionDays.Time2;
                    updatedSubjectSectionDays.Time3 = subjectSectionDays.Time3;
                    updatedSubjectSectionDays.SubjectCode = subjectSectionDays.SubjectCode;
                    updatedSubjectSectionDays.SectionId = subjectSectionDays.SectionId;

                    odtuDersSecimDbContext.SectionDays.Update(updatedSubjectSectionDays);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    var islemSonuc = new IslemSonuc<SectionDays>().Basarili(updatedSubjectSectionDays);
                    return islemSonuc;
                }
                return new IslemSonuc<SectionDays>().Basarisiz("Güncellenecek Ders Tabloda Bulunamadı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<SectionDays>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");

            }

        }
        public async Task<bool> SubjectSectionDaysCheckAsync(SectionDaysDTO subjectSectionDaysDTO)
        {
            var checkSubjectDays = await odtuDersSecimDbContext.SectionDays.Where(x => x.SubjectCode == subjectSectionDaysDTO.SubjectCode).AnyAsync(x =>
            ( x.SectionId == subjectSectionDaysDTO.SectionId &&
              x.Day1 == subjectSectionDaysDTO.Day1 &&
              x.Day2 == subjectSectionDaysDTO.Day2 &&
              x.Day3 == subjectSectionDaysDTO.Day3 &&
              x.Time1 == subjectSectionDaysDTO.Time1 &&
              x.Time2 == subjectSectionDaysDTO.Time2 &&
              x.Time3 == subjectSectionDaysDTO.Time3 
               ));

            return checkSubjectDays;       /// Farklı ders aynı saatte olması sıkıntısı var
        }


    }
}

