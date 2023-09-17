

using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;
using ODTUDersSecim.DTOs;


namespace ODTUDersSecim.Services
{
    public class AvailableCoursesService
    {
        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public AvailableCoursesService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<AvailableCourses>> GetAvailableCourses()
        {

            var availableCourses = await odtuDersSecimDbContext.AvailableCourses.ToListAsync();
            return availableCourses;
        }

        public async Task<AvailableCourses?> GetAvailablecourse(int availableCoursesId)
        {
            var availableCourse = await odtuDersSecimDbContext.AvailableCourses.SingleOrDefaultAsync(x => x.AvailableCoursesId == availableCoursesId);

            return availableCourse;
        }

        public async Task<IslemSonuc<AvailableCoursesDTO>> AddAvailableCourse(AvailableCoursesDTO availableCoursesDTO)
        {

            try
            {
                var checkSubject = await AvailableCourseCheck(availableCoursesDTO.SubjectCode);
                if (checkSubject)
                {
                    await UpdateAvailableCourse(availableCoursesDTO);
                    return new IslemSonuc<AvailableCoursesDTO>().Basarili();
                }
                else
                {
                    var availableCourse = new AvailableCourses()
                    {
                        SubjectCode = availableCoursesDTO.SubjectCode
                    };
                    await odtuDersSecimDbContext.AvailableCourses.AddAsync(availableCourse);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<AvailableCoursesDTO>().Basarili();
                }
            }

            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<AvailableCoursesDTO>().Basarisiz("Ekleme İşleminde Hata Oluştu!");
            }


        }

        public async Task<IslemSonuc<AvailableCourses>> UpdateAvailableCourse(AvailableCoursesDTO availableCoursesDTO)
        {

            try
            {
                var availableCourse = await odtuDersSecimDbContext.AvailableCourses.SingleOrDefaultAsync(x => x.SubjectCode == availableCoursesDTO.SubjectCode);

                if (availableCourse == null) return new IslemSonuc<AvailableCourses>().Basarisiz("Güncellenecek Ders Bulunamadı!");

                availableCourse.SubjectCode = availableCoursesDTO.SubjectCode;

                await odtuDersSecimDbContext.SaveChangesAsync();

                return new IslemSonuc<AvailableCourses>().Basarili(availableCourse);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<AvailableCourses>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");
            }

        }


        public async Task<IslemSonuc<AvailableCourses>> DeleteAvailableCourse(int availableCourseId)
        {
            try
            {
                var deletedAvailableCourse = await GetAvailablecourse(availableCourseId);
                if (deletedAvailableCourse != null)
                {
                    odtuDersSecimDbContext.AvailableCourses.Remove(deletedAvailableCourse);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<AvailableCourses>().Basarili(deletedAvailableCourse); ;
                }
                return new IslemSonuc<AvailableCourses>().Basarisiz("Silinecek Ders Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<AvailableCourses>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

        public async Task<bool> AvailableCourseCheck(int? subjectCode)
        {
            var checkSubject = await odtuDersSecimDbContext.AvailableCourses.AnyAsync(q => q.SubjectCode == subjectCode);

            return checkSubject;
        }


    }


}