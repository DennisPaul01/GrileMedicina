using System;
using System.Threading.Tasks;
using GrileMedicinaDev.Entities;
using GrileMedicinaDev.Models;

namespace GrileMedicinaDev.Services;

public interface IExamRepository
{
    Task<Exam> CreateExamAsync(ExamForCreationDto examDto);
}
