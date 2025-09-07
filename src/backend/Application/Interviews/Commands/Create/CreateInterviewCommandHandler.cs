using Application.Abstractions.AI;
using Application.Abstractions.FileSystem;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.Create;

internal sealed class CreateInterviewCommandHandler(
    IInterviewRepository interviewRepository, 
    IVacancyRepository vacancyRepository, 
    IInterviewer interviewer,
    IFileReader fileReader) : ICommandHandler<CreateInterviewCommand, Result<Guid>>
{
    public async ValueTask<Result<Guid>> Handle(CreateInterviewCommand command, CancellationToken cancellationToken)
    {
        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(command.VacancyId);

        if (vacancy is null)
        {
            return VacancyErrors.NotFound(command.VacancyId);
        }

        if (vacancy.UserId != command.CurrentUserId)
        {
            return VacancyErrors.WrongAuthor;
        }

        string resumeText = await fileReader.ReadAllTextAsync(command.ResumeFile);
        List<InterviewMessage> initialInterviewMessages = await interviewer.InitializeInterviewAsync(vacancy.Text, vacancy.Language);

        var interview = Interview.Create(command.Title, command.Weights, resumeText, command.MaxMessagesCount, initialInterviewMessages, vacancy);

        await interviewRepository.CreateAsync(interview);

        return interview.Id;
    }
}
