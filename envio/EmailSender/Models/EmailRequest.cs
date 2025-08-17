using System.Net.Mail;

namespace EmailSender.Models;

public record EmailRequest(
    string MessageId,
    List<string> To,
    string Subject,
    string? Body,
    List<AttachmentDto>? Attachments = null
    );