using Domain.Entities;
using Microsoft.Extensions.AI;

namespace Infrastructure.Extensions;

internal static class InterviewMessageExtensions
{
    public static IEnumerable<ChatMessage> ToChatConversation(this IEnumerable<InterviewMessage> interviewMessages)
    {
        return interviewMessages.SelectMany(
            m => new ChatMessage[] 
            { 
                new ChatMessage(ChatRole.Assistant, m.Question), 
                new ChatMessage(ChatRole.User, m.Answer) 
            });
    }
}
