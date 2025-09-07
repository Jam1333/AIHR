using Application.Abstractions.AI;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Extensions;
using Microsoft.Extensions.AI;

namespace Infrastructure.AI;

internal sealed class InterviewSummarizer(
    IChatClient chatClient,
    ISanitizer sanitizer) : IInterviewSummarizer
{
    public async Task<string[]> GetCandidateInconsistencies(IEnumerable<InterviewMessage> interviewMessages, string resumeText, string language)
    {
        string prompt = $"""
            RESUME TEXT START
            '''
            {sanitizer.Sanitize(resumeText)}
            '''
            RESUME TEXT END

            Act as a professional HR analyst. Your task is to compare the candidate's interview transcript 
            against their provided resume (CV) to identify and list only factual inconsistencies.

            **Key Instructions:**
            1. **Focus:** Only highlight contradictions between what the candidate *said* in the interview and what is *written* on their resume. Ignore any new information added in the interview that does not directly contradict the resume.
            2. **Basis for Analysis:** Treat the resume as the ground truth. An inconsistency is when a statement in the interview contradicts a claim on the resume.
            3. **Language:** Deliver output in the same language as the interview transcript ({sanitizer.Sanitize(language)}).

            Provide a clean, parsable JSON array of strings.

            **Output Format:**
            [
              "Inconsistency 1 description",
              "Inconsistency 2 description",
              ...
            ]

            **IMPORTANT: PLEASE LIST THE OUTPUT STRING IN THE {sanitizer.Sanitize(language)} LANGUAGE AS THE INTERVIEW TEXT**
            """;

        IEnumerable<ChatMessage> chatConversation = CreateChatConversation(interviewMessages, prompt);

        var inconsistenciesResponse = await chatClient.GetResponseAsync<string[]>(chatConversation);

        return inconsistenciesResponse.Result;
    }

    public async Task<string[]> GetCandidateRedFlags(IEnumerable<InterviewMessage> interviewMessages, string language)
    {
        string prompt = $"""
            Analyze the provided transcript of a job interview and identify any candidate red flags based solely on the content.

            **CRITICAL CONSTRAINTS:**
            1.  **Language Matching:** All red flags must be listed in the same language as the interview transcript ({sanitizer.Sanitize(language)}).
            2.  **Evidence-Based:** Only identify red flags that are directly supported by the text. Avoid assumptions or interpretations not present in the transcript.
            3.  **Output Format:** Provide the response as a valid JSON array of strings.

            **DESIRED OUTPUT FORMAT:**
            [
              "red_flag_1",
              "red_flag_2",
              ...
            ]

            **IMPORTANT: PLEASE LIST THE OUTPUT STRING IN THE {sanitizer.Sanitize(language)} LANGUAGE AS THE INTERVIEW TEXT**
            """;

        IEnumerable<ChatMessage> chatConversation = CreateChatConversation(interviewMessages, prompt);

        var redFlagsResponse = await chatClient.GetResponseAsync<string[]>(chatConversation);

        return redFlagsResponse.Result;
    }

    public async Task<string> GetInterviewConclusion(IEnumerable<InterviewMessage> interviewMessages, string language)
    {
        string prompt = $"""
            Act as a senior career coach. Analyze the following interview transcript and provide one or two concise, actionable pieces of short personalized advice for the candidate (1-2 sentences). Focus on the most critical skill or area for improvement based on their performance.

            **Key Instructions:**
            1.  **Language:** Deliver your advice in the same language as the interview transcript ({sanitizer.Sanitize(language)}).
            2.  **Format:** Be direct, constructive, and focused on development.
            3.  **Content:** Base the advice strictly on the content of the transcript. Avoid generic comments.

            **DESIRED OUTPUT FORMAT:**
            String with advice for candidate

            **IMPORTANT: PLEASE LIST THE OUTPUT STRING IN THE {sanitizer.Sanitize(language)} LANGUAGE AS THE INTERVIEW TEXT**
            """;

        IEnumerable<ChatMessage> chatConversation = CreateChatConversation(interviewMessages, prompt);

        var conclusionResponse = await chatClient.GetResponseAsync(chatConversation);

        return conclusionResponse.Text;
    }

    private static IEnumerable<ChatMessage> CreateChatConversation(IEnumerable<InterviewMessage> interviewMessages, string prompt)
    {
        IEnumerable<ChatMessage> chatConversation = interviewMessages.ToChatConversation();

        return [..chatConversation, new ChatMessage(ChatRole.User, prompt)];
    }
}
