using System;
using ODTUDersSecim.Models;
using ODTUDersSecim.Helpers;

namespace ODTUDersSecim.Services
{
	public interface IDersSecim<T>
	{
        Task<List<T>> GetSubjects();
        Task<T?> GetSubject(int subjectCode);
        Task<IslemSonuc<T>> DeleteSubject(int subjectCode);
        Task<IslemSonuc<T>> AddSubject(T subject);
        Task<IslemSonuc<T>> UpdateSubject(T subject);
    }
}

