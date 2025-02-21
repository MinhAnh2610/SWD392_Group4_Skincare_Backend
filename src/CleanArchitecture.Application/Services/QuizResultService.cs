using CleanArchitecture.Application.DTOs.QuizDto;
using CleanArchitecture.Application.DTOs.RoutineDTO;

namespace CleanArchitecture.Application.Services;

public class QuizResultService : IQuizResultService
{
  private readonly IUnitOfWork _unitOfWork;
  public QuizResultService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task SaveQuizResultAsync(Guid customerId, Guid quizId, QuizSubmitRequest quizSubmission, RoutineResponse quizResponse)
  {
    var skinType = quizResponse.SkinType;
    var options = new List<QuestionOption>();
    var selectedOptionIds = quizSubmission.Answers
    .SelectMany(a => a.SelectedOptionIds)
    .ToList();

    foreach (var optionId in selectedOptionIds)
    {
      var option = await _unitOfWork.QuestionOptions.GetByIdAsync(optionId);
      options.Add(option);
    }

    var quizResult = new QuizResult
    {
      Id = Guid.NewGuid(),
      CustomerId = customerId,
      QuizId = quizId,
      SkinTypeId = skinType!.Id,
      QuizAnswers = quizSubmission.Answers.Select(a => new QuizAnswer
      {
        Id = Guid.NewGuid(),
        QuestionId = a.QuestionId,
        SelectedOptions = string.Join(", ", options
                .Where(o => a.SelectedOptionIds.Contains(o.Id)) 
                .Select(o => o.Content) 
            )!
      }).ToList()
    };

    await _unitOfWork.QuizResults.CreateAsync(quizResult);
    await _unitOfWork.CompleteAsync();
  }
}
