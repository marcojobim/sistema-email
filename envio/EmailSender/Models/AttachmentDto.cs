namespace EmailSender.Models;

public record AttachmentDto(
    string FileName,
    string ContentBase64,
    string ContentType
    );