using System;
using System.Net;
using ODTUDersSecim.Helpers;
using Microsoft.EntityFrameworkCore;
using ODTUDersSecim.Models;
using System.Xml.Linq;


namespace ODTUDersSecim.Services
{
	public class CengDersSecimService : IDersSecim<CengSubjects>
	{

		private readonly ODTUDersSecimDBContext odtuDersSecimDbContext;

        public CengDersSecimService(ODTUDersSecimDBContext dBContext)
		{
			this.odtuDersSecimDbContext = dBContext;
		}

		public async Task<List<CengSubjects>> GetSubjects()
		{  
			
				var subjects = await odtuDersSecimDbContext.CengSubjects.ToListAsync();
				return subjects;
		}

		public async Task<CengSubjects?> GetSubject(int subjectCode)
		{
			var subject = await odtuDersSecimDbContext.CengSubjects.SingleOrDefaultAsync(x => x.SubjectCode == subjectCode);
			return subject;
		}

		public async Task<IslemSonuc<CengSubjects>> DeleteSubject(int subjectCode)
		{
			try
			{
                var subject = await GetSubject(subjectCode);
                if (subject != null)
                {
                    odtuDersSecimDbContext.CengSubjects.Remove(subject);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    return new IslemSonuc<CengSubjects>().Basarili(subject); ;
                }
                return new IslemSonuc<CengSubjects>().Basarisiz("Silinecek Ders Tabloda Bulunamadı!");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<CengSubjects>().Basarisiz("Silme İşleminde Hata Oluştu!");

            }
        }

		public async Task<IslemSonuc<CengSubjects>> AddSubject(CengSubjects cengSubject)
		{
			try
			{
                var checkSubject = await SubjectCheckAsync(cengSubject.SubjectCode);
                if(checkSubject)
                {
                    return new IslemSonuc<CengSubjects>().Basarisiz("Ders tabloda var");
                }
                await odtuDersSecimDbContext.CengSubjects.AddAsync(cengSubject);
                await odtuDersSecimDbContext.SaveChangesAsync();
                var islemSonuc = new IslemSonuc<CengSubjects>().Basarili(cengSubject);
                return islemSonuc;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<CengSubjects>().Basarisiz("Ekleme İşleminde Hata Oluştu!");

            }

        }

		public async Task<IslemSonuc<CengSubjects>> UpdateSubject(CengSubjects subject)
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

                    odtuDersSecimDbContext.CengSubjects.Update(updatedSubject);
                    await odtuDersSecimDbContext.SaveChangesAsync();
                    var islemSonuc = new IslemSonuc<CengSubjects>().Basarili(updatedSubject);
                    return islemSonuc;
                }
                return new IslemSonuc<CengSubjects>().Basarisiz("Güncellenecek Ders Tabloda Bulunamadı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new IslemSonuc<CengSubjects>().Basarisiz("Güncelleme İşleminde Hata Oluştu!");

            }

		}

		public async Task<bool> SubjectCheckAsync (int subjectCode)
        {
            var checkSubject = await odtuDersSecimDbContext.CengSubjects.AnyAsync(q => q.SubjectCode == subjectCode);

            return checkSubject;
        }



    }
}

