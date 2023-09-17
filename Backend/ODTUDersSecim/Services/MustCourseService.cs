using System;
using System.Net;
using ODTUDersSecim.Helpers;
using ODTUDersSecim.DTOs;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;


namespace ODTUDersSecim.Services
{
    public class MustCourseService
    {

        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public MustCourseService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<MustCourseDTO>> GetAllMustCourses()
        {

            var mustCoursesDTO = await odtuDersSecimDbContext.MustCourses
                .Select(x => new MustCourseDTO
                {
                    Semester = x.Semester,
                    DeptCode = x.DeptCode,
                    SubjectCode= x.SubjectCode
                })
                .ToListAsync();
            return mustCoursesDTO;
        }

        public async Task<MustCourses?> GetMustCourse(int mustCourseId)
        {
            var mustCourse = await odtuDersSecimDbContext.MustCourses.SingleOrDefaultAsync(x => x.MustCourseId == mustCourseId);
            return mustCourse;
        }

        public async Task<List<MustCourseDTO>> GetSemesterMustCourses(int semester, int deptCode)
        {
            var mustCourses = await odtuDersSecimDbContext.MustCourses.Where(x => x.DeptCode == deptCode &&
                                                                                  x.Semester == semester).Select(x => new MustCourseDTO
                                                                                  {
                                                                                      Semester = x.Semester,
                                                                                      DeptCode = x.DeptCode,
                                                                                      SubjectCode= x.SubjectCode
                                                                                  }).ToListAsync();

            return mustCourses;
        }

        public async Task<IslemSonuc<MustCourses>> DeleteMustCourse(int mustCourseId)
        {
            try
            {
                var deletedMustCourse = await GetMustCourse(mustCourseId);
                if (deletedMustCourse != null)
                {
                    odtuDersSecimDbContext.MustCourses.Remove(deletedMustCourse);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<MustCourses>().Basarili(deletedMustCourse); ;
                }
                return new IslemSonuc<MustCourses>().Basarisiz("Silinecek Must Course Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<MustCourses>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

        public async Task<IslemSonuc<MustCourseDTO>> AddMustCourse(MustCourseDTO mustCourseDTO)
        {
            try
            {
                var checkMustCourse = await MustCourseCheck(mustCourseDTO.SubjectCode, mustCourseDTO.DeptCode, mustCourseDTO.Semester);
                if (checkMustCourse)
                {
                    return new IslemSonuc<MustCourseDTO>().Basarisiz("Departman tabloda var");
                }

                var addedMustCourse = new MustCourses
                {
                    DeptCode = mustCourseDTO.DeptCode,
                    Semester = mustCourseDTO.Semester,
                    SubjectCode= mustCourseDTO.SubjectCode
                };
                await odtuDersSecimDbContext.MustCourses.AddAsync(addedMustCourse);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<MustCourseDTO>().Basarili(mustCourseDTO);
                return islemSonuc;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<MustCourseDTO>().Basarisiz("Ekleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<IslemSonuc<MustCourses>> UpdateMustCourse(MustCourses mustCourse)
        {
            try
            {
                var updatedMustCourse = await GetMustCourse(mustCourse.MustCourseId);
                if (updatedMustCourse != null)
                {
                    updatedMustCourse.DeptCode = mustCourse.DeptCode;
                    updatedMustCourse.Semester = mustCourse.Semester;
                    updatedMustCourse.Departments = mustCourse.Departments;
                    updatedMustCourse.SubjectCode = mustCourse.SubjectCode;

                    odtuDersSecimDbContext.MustCourses.Update(updatedMustCourse);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    var islemSonuc = new IslemSonuc<MustCourses>().Basarili(updatedMustCourse);
                    return islemSonuc;
                }
                return new IslemSonuc<MustCourses>().Basarisiz("Güncellenecek Departman Tabloda Bulunamadı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<MustCourses>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");

            }

        }

        public async Task<bool> MustCourseCheck(int? subjectCode, int? deptCode, int? semester)
        {
            var checkMustCourse = await odtuDersSecimDbContext.MustCourses.Where(x => x.DeptCode == deptCode &&
                                                                                      x.SubjectCode== subjectCode &&
                                                                                      x.Semester == semester).AnyAsync();

            return checkMustCourse;
        }



    }
}

