using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;
using ODTUDersSecim.DTOs;


namespace ODTUDersSecim.Services
{
    public class ElectiveCoursesService
    {
        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public ElectiveCoursesService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }


        public async Task<List<ElectiveCourses>> GetElectiveCourses()
        {

            var electiveCourses = await odtuDersSecimDbContext.ElectiveCourses.ToListAsync();
            return electiveCourses;
        }

        public async Task<ElectiveCourses?> GetElectivecourse(int electiveCoursesId)
        {
            var electiveCourse = await odtuDersSecimDbContext.ElectiveCourses.SingleOrDefaultAsync(x => x.ElectiveCoursesId == electiveCoursesId);

            return electiveCourse;
        }

        public async Task<IslemSonuc<ElectiveCoursesDTO>> AddElectiveCourse(ElectiveCoursesDTO electiveCourseDTO)
        {

            try
            {
                var checkSubject = await ElectiveCourseCheck(electiveCourseDTO.SubjectCode, electiveCourseDTO.DeptCode, electiveCourseDTO.ElectiveType);
                if (checkSubject)
                {
                    await UpdateElectiveCourse(electiveCourseDTO);
                    return new IslemSonuc<ElectiveCoursesDTO>().Basarili();
                }
                else
                {
                    var electiveCourse = new ElectiveCourses()
                    {
                        SubjectCode = electiveCourseDTO.SubjectCode,
                        DeptCode = electiveCourseDTO.DeptCode,
                        ElectiveType = electiveCourseDTO.ElectiveType

                    };
                    await odtuDersSecimDbContext.ElectiveCourses.AddAsync(electiveCourse);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<ElectiveCoursesDTO>().Basarili();
                }
            }

            catch (Exception ex)
            {
                return new IslemSonuc<ElectiveCoursesDTO>().Basarisiz(ex.Message);
            }
        }

        public async Task<IslemSonuc<ElectiveCourses>> UpdateElectiveCourse(ElectiveCoursesDTO electiveCourseDTO)
        {
            try
            {
                var electiveCourse = await odtuDersSecimDbContext.ElectiveCourses.SingleOrDefaultAsync(x => x.ElectiveType == electiveCourseDTO.ElectiveType &&
                                                                                                            x.DeptCode     == electiveCourseDTO.DeptCode &&
                                                                                                            x.SubjectCode == electiveCourseDTO.SubjectCode
                );
                if (electiveCourse == null)
                {
                    return new IslemSonuc<ElectiveCourses>().Basarisiz("Ders Bulunamadı!");
                }
                else
                {
                    electiveCourse.SubjectCode = electiveCourseDTO.SubjectCode;
                    electiveCourse.DeptCode = electiveCourseDTO.DeptCode;
                    electiveCourse.ElectiveType = electiveCourseDTO.ElectiveType;

                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<ElectiveCourses>().Basarili();
                }
            }
            catch (Exception ex)
            {
                return new IslemSonuc<ElectiveCourses>().Basarisiz(ex.Message);
            }
        }

        public async Task<IslemSonuc<ElectiveCourses>> DeleteElectiveCourse(int electiveCoursesId)
        {
            try
            {
                var electiveCourse = await odtuDersSecimDbContext.ElectiveCourses.SingleOrDefaultAsync(x => x.ElectiveCoursesId == electiveCoursesId);
                if (electiveCourse == null)
                {
                    return new IslemSonuc<ElectiveCourses>().Basarisiz("Ders Bulunamadı!");
                }
                else
                {
                    odtuDersSecimDbContext.ElectiveCourses.Remove(electiveCourse);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<ElectiveCourses>().Basarili();
                }
            }
            catch (Exception ex)
            {
                return new IslemSonuc<ElectiveCourses>().Basarisiz(ex.Message);
            }
        }

        public async Task<bool> ElectiveCourseCheck(int? subjectCode, int? deptCode, Electives.ElectiveTypes? electiveType)
        {
            var electiveCourse = await odtuDersSecimDbContext.ElectiveCourses.SingleOrDefaultAsync(x => x.SubjectCode == subjectCode &&
                                                                                                        x.DeptCode == deptCode &&
                                                                                                        x.ElectiveType == electiveType
            );
            if (electiveCourse == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}