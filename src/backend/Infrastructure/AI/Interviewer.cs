using Application.Abstractions.AI;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Extensions;
using Microsoft.Extensions.AI;

namespace Infrastructure.AI;

internal sealed class Interviewer(
    IChatClient chatClient,
    ISanitizer sanitizer) : IInterviewer
{
    public async Task<InterviewMessage> GenerateNextQuestionAsync(IEnumerable<InterviewMessage> interviewMessages)
    {
        IEnumerable<ChatMessage> chatConversation = interviewMessages.ToChatConversation();

        var response = await chatClient.GetResponseAsync(chatConversation);

        var interviewMessage = InterviewMessage.Create(response.Text);

        return interviewMessage;
    }

    public async Task<List<InterviewMessage>> InitializeInterviewAsync(string vacancyText, string language)
    {
        string prompt = $"""
            VACANCY TEXT START
            '''
            {sanitizer.Sanitize(vacancyText)}
            '''
            VACANCY TEXT END

            Your name is Alexander, act as a friendly HR specialist conducting an oral interview based on the provided vacancy text.

            **Follow this structure and flow in your conversation**:
            1. Begin with a polite greeting and ask the classic opener: Could you tell me a little about yourself? (mention the position from the vacancy).
            2. After receiving the candidate's answer, proceed to ask one question about their professional experience related to the vacancy.
            3. Then, ask a few technical questions related to the vacancy.

            Ask them ONE at a time (one by one), waiting for the candidate's answer to the first before providing the second.

            Think of it as an oral interview.

            **Important**: The entire interview must be conducted fully in "{sanitizer.Sanitize(language)}" language. Your tone should be professional and friendly.

            The candidate is ready. Begin the interview with your first question.
            """;

        var promptMessage = InterviewMessage.Create("Hello, how can i help you?", prompt);

        var response = await chatClient.GetResponseAsync(prompt);

        var firstMessage = InterviewMessage.Create(response.Text);

        return [promptMessage, firstMessage];
    }
}
