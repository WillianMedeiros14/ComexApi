using System.Collections.Generic;


public class ValidationErrorResponse
{
    public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
}

