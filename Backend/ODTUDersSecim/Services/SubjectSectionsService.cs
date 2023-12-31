﻿using System;
using System.Net;
using ODTUDersSecim.Helpers;
using ODTUDersSecim.DTOs;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;
using System.Globalization; 

namespace ODTUDersSecim.Services
{
    public class SubjectSectionsService 
    {

        private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public SubjectSectionsService(ODTUDersSecimDBContext dBContext)
        {
            this.odtuDersSecimDbContext = dBContext;
        }

        public async Task<List<SubjectSectionsDTO>> GetAllSubjectSections()
        {
            var subjectAllSections = await odtuDersSecimDbContext.SubjectSections
                .Select(x => new SubjectSectionsDTO
                {
                    SectionCode = x.SectionCode,
                    GivenDept = x.GivenDept,
                    StartChar = x.StartChar,
                    EndChar = x.EndChar,
                    MinCumGpa = x.MinCumGpa,
                    MaxCumGpa = x.MaxCumGpa,
                    MinYear = x.MinYear,
                    MaxYear = x.MaxYear,
                    StartGrade = x.StartGrade,
                    EndGrade = x.EndGrade,
                    SubjectCode = x.SubjectCode,
                })
                .ToListAsync();

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

        public bool GradeChecker(string? courseGrade, string? startGrade, string? endGrade)
        {
            string[] gradeArray = { "AA", "BA", "BB", "CB", "CC", "DC", "DD", "FD", "FF", "NA" };

            if (startGrade== "Herkes alabilir")
            {
                return true;
            }
            else if (courseGrade == "Kaldi" && startGrade == "Kaldi")
            {
                return true;
            }

            else if ((courseGrade == "FF" || courseGrade == "FD" || courseGrade == "NA") && startGrade == "Hic almayanlar veya Basarisizlar (FD ve alti)")
            {
                return true;
            }

            else if (courseGrade != null && startGrade != null && endGrade != null)
            {
                int courseGradeIndex = Array.IndexOf(gradeArray, courseGrade);
                int startGradeIndex = Array.IndexOf(gradeArray, startGrade);
                int endGradeIndex = Array.IndexOf(gradeArray, endGrade);

                if (courseGradeIndex >= startGradeIndex && courseGradeIndex <= endGradeIndex)
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsWithinTurkishRange(string surname, string startChar, string endChar)
        {
            return StringComparer.Create(new CultureInfo("tr-TR"), false).Compare(surname, startChar) >= 0 &&
                   StringComparer.Create(new CultureInfo("tr-TR"), false).Compare(surname, endChar) <= 0;
        }

        public async Task<List<SectionDays>> GetSectionDays(int subjectCode, float? cumGPA, string? surname, string? courseGrade)
        {
            try
            {
                var matchingDays=new List<SectionDays>();

                var matchingSections = odtuDersSecimDbContext.SubjectSections
                    .Where(x => x.SubjectCode == subjectCode)
                    .AsEnumerable() 
                    .Where(x =>
                        (cumGPA == null || (cumGPA >= x.MinCumGpa && cumGPA <= x.MaxCumGpa)) &&
                        (surname == null || IsWithinTurkishRange(surname, x.StartChar, x.EndChar)) &&
                        (courseGrade == null || GradeChecker(courseGrade, x.StartGrade, x.EndGrade)))
                    .ToList();


                foreach (var matchingSection in matchingSections)
                {   
                     var matchingDay = await odtuDersSecimDbContext.SectionDays.Where(x => x.SubjectCode == matchingSection.SubjectCode &&
                                                                        x.SectionId == matchingSection.SectionCode).FirstOrDefaultAsync();
                    if (matchingDay != null)
                    {   if (!( matchingDays.Where(x=>x.SectionId == matchingDay.SectionId).Any()))
                                matchingDays.Add(matchingDay);
                    }
                    
                }
                return matchingDays;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error: " + ex.Message);
                return new List<SectionDays>(); // Return an empty list or handle the error accordingly
            }
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

        public async Task<IslemSonuc<SubjectSectionsDTO>> AddSubjectSection(SubjectSectionsDTO subjectSectionDTO)
        {
            try
            {
                var checkSubject = await SubjectSectionCheckAsync(subjectSectionDTO);
                if (checkSubject)
                {
                    return new IslemSonuc<SubjectSectionsDTO>().Basarisiz("Section tabloda var");
                }
                var subjectSection = new SubjectSections
                {
                    SectionCode = subjectSectionDTO.SectionCode,
                    GivenDept = subjectSectionDTO.GivenDept,
                    StartChar = subjectSectionDTO.StartChar,
                    EndChar = subjectSectionDTO.EndChar,
                    MinCumGpa = subjectSectionDTO.MinCumGpa,
                    MaxCumGpa = subjectSectionDTO.MaxCumGpa,
                    MinYear = subjectSectionDTO.MinYear,
                    MaxYear = subjectSectionDTO.MaxYear,
                    StartGrade = subjectSectionDTO.StartGrade,
                    EndGrade = subjectSectionDTO.EndGrade,
                    SubjectCode = subjectSectionDTO.SubjectCode
                };
                await odtuDersSecimDbContext.SubjectSections.AddAsync(subjectSection);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<SubjectSectionsDTO>().Basarili(subjectSectionDTO);
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

                return new IslemSonuc<SubjectSectionsDTO>().Basarisiz("Ekleme İşleminde Hata Oluştu!");
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

        public async Task<bool> SubjectSectionCheckAsync(SubjectSectionsDTO subjectSection)
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

