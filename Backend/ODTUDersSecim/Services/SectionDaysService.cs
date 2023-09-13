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

        public async Task<SectionDaysDTO> GetSubjectSectionDetails(int subjectCode, int sectionId)
        {
            var subjectSectionDetails = await odtuDersSecimDbContext.SectionDays.Where(x => x.SubjectCode == subjectCode && x.SectionId == sectionId).FirstOrDefaultAsync();
            if (subjectSectionDetails == null)
                return new SectionDaysDTO();
            else
            {

                var detail = new SectionDaysDTO()
                {
                    SubjectCode = subjectSectionDetails.SubjectCode,
                    SectionId = subjectSectionDetails.SectionId,
                    Day1 = subjectSectionDetails.Day1,
                    Day2 = subjectSectionDetails.Day2,
                    Day3 = subjectSectionDetails.Day3,
                    Time1 = subjectSectionDetails.Time1,
                    Time2 = subjectSectionDetails.Time2,
                    Time3 = subjectSectionDetails.Time3,
                    InstructorName = subjectSectionDetails.InstructorName,
                    Place = subjectSectionDetails.Place
                    };
                
                return detail;
            }
          
        }
        public bool CheckCourseConflict(SectionDaysDTO first, SectionDaysDTO second)
        {
            bool conflic1 = CheckConflict(first.Day1, first.Time1, second.Day1, second.Time1);
            bool conflic2 = CheckConflict(first.Day1, first.Time1, second.Day2, second.Time2);
            bool conflic3 = CheckConflict(first.Day1, first.Time1, second.Day3, second.Time3);
            bool conflic4 = CheckConflict(first.Day2, first.Time2, second.Day1, second.Time1);
            bool conflic5 = CheckConflict(first.Day2, first.Time2, second.Day2, second.Time2);
            bool conflic6 = CheckConflict(first.Day2, first.Time2, second.Day3, second.Time3);
            bool conflic7 = CheckConflict(first.Day3, first.Time3, second.Day1, second.Time1);
            bool conflic8 = CheckConflict(first.Day3, first.Time3, second.Day2, second.Time2);
            bool conflic9 = CheckConflict(first.Day3, first.Time3, second.Day3, second.Time3);


            return conflic1 || conflic2 || conflic3 || conflic4 || conflic5 || conflic6 || conflic7 || conflic8 || conflic9;
        }

        private bool CheckConflict(string day1, string time1, string day2, string time2)
        {
            return !string.IsNullOrEmpty(day1) && !string.IsNullOrEmpty(time1) &&
                   !string.IsNullOrEmpty(day2) && !string.IsNullOrEmpty(time2) &&
                   day1 == day2 && (IsTimeIntervalWithin(time1, time2) || IsTimeIntervalWithin(time2, time1));
        }
        private bool IsTimeIntervalWithin(string innerTimeInterval, string outerTimeInterval)
        {
            TimeSpan innerStart, innerEnd, outerStart, outerEnd;

            if (TryParseTimeInterval(innerTimeInterval, out innerStart, out innerEnd) &&
                TryParseTimeInterval(outerTimeInterval, out outerStart, out outerEnd))
            {
                return innerStart >= outerStart && innerEnd <= outerEnd;
            }

            return false;
        }

        private bool TryParseTimeInterval(string timeInterval, out TimeSpan startTime, out TimeSpan endTime)
        {
            startTime = TimeSpan.Zero;
            endTime = TimeSpan.Zero;

            string[] parts = timeInterval.Split('-');
            if (parts.Length != 2)
                return false;

            string startTimeWithSeconds = parts[0].Trim() + ":00";
            string endTimeWithSeconds = parts[1].Trim() + ":00";

            if (TimeSpan.TryParseExact(startTimeWithSeconds, "hh\\:mm\\:ss", null, out startTime) &&
                TimeSpan.TryParseExact(endTimeWithSeconds, "hh\\:mm\\:ss", null, out endTime))
            {
                return true;
            }

            return false;
        }

        public async Task<List<List<SectionDaysDTO>>> GetSchedule(List<Subject> listOfSubjects)
        {
            var allSchedules = new List<List<SectionDaysDTO>>();

            var firstSubject = listOfSubjects.FirstOrDefault().SubjectCode;
            int numberOfSubject = 0;

            foreach (var subject in listOfSubjects)
            {
                numberOfSubject++;
                foreach (var sectionId in subject.Sections)
                {
                    var section = await GetSubjectSectionDetails(subject.SubjectCode, sectionId);

                    if (firstSubject == subject.SubjectCode)
                    {
                        var newSchedule = new List<SectionDaysDTO> { section };
                        allSchedules.Add(newSchedule);

                    }
                    else
                    {
                        var indexes = allSchedules
                            .Select((existingSchedule, index) => new { Schedule = existingSchedule, Index = index })
                            .Where(scheduleWithIndex =>
                                !scheduleWithIndex.Schedule.All(existingSection =>
                                    CheckCourseConflict(existingSection,section)
                                )
                            )
                            .Select(scheduleWithIndex => scheduleWithIndex.Index)
                            .ToList();



                        foreach (var index in indexes)
                        {
                            if (numberOfSubject != allSchedules[index].Count)
                            {
                                var newSchedule = new List<SectionDaysDTO>(allSchedules[index])
                            {
                                section
                            };
                                allSchedules.Add(newSchedule);
                            }


                        }
                    }

                }

                for (int i = allSchedules.Count - 1; i >= 0; i--)
                {
                    var schedule = allSchedules[i];

                    if (schedule.Count < numberOfSubject)
                    {
                        allSchedules.RemoveAt(i);
                    }
                }
            }

            return allSchedules;
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
                    SubjectCode = subjectSectionDaysDTO.SubjectCode,
                    InstructorName= subjectSectionDaysDTO.InstructorName,
                    Place = subjectSectionDaysDTO.Place
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
                    updatedSubjectSectionDays.InstructorName = subjectSectionDays.InstructorName;
                    updatedSubjectSectionDays.Place = subjectSectionDays.Place;

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
              x.Time3 == subjectSectionDaysDTO.Time3 &&
              x.InstructorName == subjectSectionDaysDTO.InstructorName &&
              x.Place == subjectSectionDaysDTO.Place 
               ));

            return checkSubjectDays;     
        }


    }
}

