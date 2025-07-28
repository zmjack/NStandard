namespace NStandard.Web;

/// <summary>
/// Same as System.Web.Mvc.HttpVerbs .
/// <para>Refer: <see href="https://learn.microsoft.com/en-us/dotnet/api/system.web.mvc.httpverbs?view=aspnet-mvc-5.2" /></para>
/// </summary>
[Flags]
public enum HttpVerbs
{
    //Retrieves the information or entity that is identified by the URI of the request.
    Get = 1,

    //Posts a new entity as an addition to a URI.
    Post = 2,

    //Replaces an entity that is identified by a URI.
    Put = 4,

    //Requests that a specified URI be deleted.
    Delete = 8,

    //Retrieves the message headers for the information or entity that is identified by the URI of the request.
    Head = 16,

    //Requests that a set of changes described in the request entity be applied to the resource identified by the Request- URI.
    Patch = 32,

    //Represents a request for information about the communication options available on the request/response chain identified by the Request-URI.
    Options = 64,
}
